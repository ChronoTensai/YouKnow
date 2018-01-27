using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.PathFinder3D;

public class Pursuer : MonoBehaviour
{
    [Header("Game zone constraints:")]
    public float spaceFragmentation;
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    [Header("Path search settings:")]
    public bool automaticSearchStop;
    public float stopTimer;
    public bool trajectoryOptimization;
    public bool trajectorySmoothing;
    [Header("Movement settings:")]
    public float speed;
    public bool moveVectorOrientation;
    public float turnSpeed;
    [Header("Debug settings:")]
    public bool traceWay;
    public float lineWidth;

    //Inside variables
    SpaceConstraints constraints;
    SpaceGraph spaceGraph;
    List<Vector3> way;
    List<float> LengthOfTheChords;
    float totalLength;
    float CurrOffset;
    int prePointI;
    delegate void conditions();
    conditions curCondition;
    Coroutine AStarCoroutine;
    Vector3 finalPoint;
    void Start()
    {
        //Core classes initiation (learn more about system calls in “Additional options” of “User’s guide.pdf” document
        ClearResorces();
        constraints = new SpaceConstraints(xMin, xMax, yMin, yMax, zMin, zMax);
        spaceGraph = new SpaceGraph(constraints, spaceFragmentation, 0.7d, this);
    }
    void Awake()
    {
        //Core classes initiation (learn more about system calls in “Additional options” of “User’s guide.pdf” document
        ClearResorces();
        constraints = new SpaceConstraints(xMin, xMax, yMin, yMax, zMin, zMax);
        spaceGraph = new SpaceGraph(constraints, spaceFragmentation, 0.7d, this);
    }
    void FixedUpdate()
    {
        //Executing the method currently appropriated to the delegate 
        curCondition();
    }

    //User defined calls (learn more about all of them in “Additional options” of “User’s guide.pdf” document)
    //>>>>
    public void ClearResorces()
    {
        if (way != null)
            way.Clear();
        if (LengthOfTheChords != null)
            LengthOfTheChords.Clear();
        totalLength = 0;
        CurrOffset = 0;
        prePointI = 0;
        curCondition = WaitingForRequest;
    }
    public bool RedefineConstraints(float XMin, float XMax, float YMin, float YMax, float ZMin, float ZMax, float SideLength, double heur_fact)
    {
        if (XMin < XMax && YMin < YMax && ZMin < ZMax && SideLength > 0 && heur_fact >= 0.5f && heur_fact <= 1)
        {
            constraints = new SpaceConstraints(XMin, XMax, YMin, YMax, ZMin, ZMax);
            spaceGraph = new SpaceGraph(constraints, SideLength, heur_fact, this);
            return true;
        }
        else return false;
    }
    public bool MoveTo(Vector3 target)
    {
        if (curCondition == WaitingForRequest)
        {
            ClearResorces();
            spaceGraph.SetGoal(target);
            spaceGraph.SetStart(transform.position);
            finalPoint = target;
            curCondition = WaitingForAWay;
            AStarCoroutine = spaceGraph.GetWay(ProcessWay);
            if (automaticSearchStop) StartCoroutine(KillSearch(stopTimer));
            return true;
        }
        else return false;
    }
    public bool MoveTo(Transform target)
    {
        if (curCondition == WaitingForRequest)
        {
            ClearResorces();
            spaceGraph.SetGoal(target.position);
            spaceGraph.SetStart(transform.position);
            finalPoint = target.position;
            curCondition = WaitingForAWay;
            AStarCoroutine = spaceGraph.GetWay(ProcessWay);
            if (automaticSearchStop) StartCoroutine(KillSearch(stopTimer));
            return true;
        }
        else return false;
    }
    public bool InterruptMovement()
    {
        if (curCondition == Movement)
        {
            curCondition = WaitingForTheContinuation;
            return true;
        }
        else return false;
    }
    public bool ResumeMovement()
    {
        if (curCondition == WaitingForTheContinuation)
        {
            curCondition = Movement;
            return true;
        }
        return false;
    }
    public bool CancelMovement()
    {
        if (curCondition == Movement || curCondition == WaitingForTheContinuation)
        {
            ClearResorces();
            curCondition = WaitingForRequest;
            return true;
        }
        return false;
    }
    public bool CancelWaySearch()
    {
        if (curCondition == WaitingForAWay)
        {
            if (AStarCoroutine != null)
            {
                StopCoroutine(AStarCoroutine);
                gameObject.SendMessage("WaySearchingIsCanceled");
            }
            else
                Debug.Log("Coroutine is not found");
            curCondition = WaitingForRequest;
            return true;
        }
        else return false;
    }
    public string GetStatus()
    {
        return curCondition.Method.ToString();
    }
    public void PrintStatus()
    {
        Debug.Log(curCondition.Method.ToString());
    }
    public List<Vector3> GetWay()
    {
        if (curCondition != WaitingForRequest && curCondition != WaitingForAWay)
            return way;
        else return null;
    }
    public float GetLength()
    {
        if (curCondition == Movement || curCondition == WaitingForTheContinuation)
            return totalLength;
        else return -1;
    }
    public float GetProgress()
    {
        if (curCondition == Movement || curCondition == WaitingForTheContinuation) return CurrOffset / totalLength;
        else return -1;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public bool SetSpeed(float sp)
    {
        if (sp > 0)
        {
            speed = sp;
            return true;
        }
        else return false;
    }
    //<<<<

    //Here all required trajectory transformations occur
    //before the game object starts the movement toward the target
    void ProcessWay(List<Vector3> InputWay)
    {
        //Add precise start and end coordinates to the found trajectory
        InputWay[InputWay.Count - 1] = finalPoint;
        InputWay.Insert(0, transform.position);

        //call optimization functions and smooth trajectory
        way = PathOptimization(InputWay);
        way = PathSmoothing(InputWay);

        //Record lengths of all chords of the trajectory in the list
        LengthOfTheChords = new List<float>(way.Count - 1);
        for (int i = 1; i <= way.Count - 1; i++)
        {
            LengthOfTheChords.Add(Vector3.Magnitude(way[i - 1] - way[i]));
            totalLength += LengthOfTheChords[i - 1];
        }

        //Trajectory drawing
        trace_way(way);

        //Assigning the method to the delegate
        curCondition = Movement;
    }

    // Trajectory smoothing when trajectorySmoothing value is true
    List<Vector3> PathSmoothing(List<Vector3> inp_way)
    {
        if (trajectorySmoothing)
        {
            //Interpolation with the Katmull-Rom spline
            CatmullRom spline = new CatmullRom(inp_way, spaceFragmentation);
            return spline.GetSpline();

            //An alternative variant of interpolation using the Bézier spline
            //            BezierInterpolation Bezier_spline = new BezierInterpolation(inp_way,spaceFragmentation);
            //            Bezier_spline.calc_curves();
            //            return Bezier_spline.BuildSpline();
        }
        else return inp_way;
    }

    //Trajectory optimization when trajectoryOptimization value is true
    List<Vector3> PathOptimization(List<Vector3> inp_way)
    {
        if (trajectoryOptimization)
        {
            for (int i = 0; i <= inp_way.Count - 2; i++)
            {
                for (int j = i + 1; j <= inp_way.Count - 1; j++)
                {
                    RaycastHit hit;
                    if (Physics.SphereCast(inp_way[i], spaceFragmentation * 0.5F, inp_way[j] - inp_way[i], out hit, Vector3.Magnitude(inp_way[j] - inp_way[i])) 
                        || Physics.Raycast(inp_way[i], inp_way[j] - inp_way[i],Vector3.Magnitude(inp_way[j] - inp_way[i]))
                        || Physics.SphereCast(inp_way[j], spaceFragmentation * 0.5F, inp_way[i] - inp_way[j], out hit, Vector3.Magnitude(inp_way[j] - inp_way[i]))
                        || Physics.Raycast(inp_way[j], inp_way[i] - inp_way[j], Vector3.Magnitude(inp_way[j] - inp_way[i])))
                    {
                        if (j - i > 2)
                        {
                            inp_way.RemoveRange(i + 1, j - i - 1);
                            return PathOptimization(inp_way);
                        }
                        else break;
                    }
                    if (j == inp_way.Count - 1 && j - i > 2)
                    {
                        inp_way.RemoveRange(i + 1, j - i - 1);
                        return inp_way;
                    }
                }
            }
            return inp_way;
        }
        else return inp_way;
    }

    //Trajectory visualization when traceway variable is true
    void trace_way(List<Vector3> way)
    {
        if (traceWay)
        {
            GameObject lineContainer = new GameObject();
            lineContainer.name = "spline";
            LineRenderer line = lineContainer.AddComponent<LineRenderer>();
            line.loop = false;

            line.positionCount = way.Count;
            line.SetPositions(way.ToArray());
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            Destroy(lineContainer, 60);
        }
        return;
    }


    //Procedures that can be assigned to “curCondition” delegate
    //Each procedure matches a state of FSM
    //Learn more about FSM in “Additional opportunities” of “User’s guide.pdf” document

    //>>>>
    void Movement()
    {
        // calling the procedure of moving along the path
        Move();
    }
    void WaitingForRequest()
    {
        //Place here your instructions
        return;
    }
    void WaitingForAWay()
    {
        if (AStarCoroutine == null && curCondition == WaitingForAWay) CancelWaySearch();
        //Place here your instructions
        return;
    }
    void WaitingForTheContinuation()
    {
        //Place here your instructions
        return;
    }
    //<<<<

    //Assignment a required turn in space to the game object
    void FollowTheDirection(Vector3 pre_position)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position - pre_position), turnSpeed);
    }

    //Procedure of game object’s moving along the path
    void Move()
    {
        Vector3 prePosition = Vector3.zero;
        if (prePointI == way.Count - 1)
        {
            gameObject.SendMessage("PursuitIsFinished");
            curCondition = WaitingForRequest;
            return;
        }
        if (speed >= totalLength)
        {
            curCondition = WaitingForRequest;
            prePosition = transform.position;
            transform.position = way[way.Count - 1];
            gameObject.SendMessage("PursuitIsFinished");
            if (moveVectorOrientation) FollowTheDirection(prePosition);
            return;
        }
        if (Mathf.Abs(totalLength - CurrOffset) <= speed)
        {
            prePosition = transform.position;
            transform.position = way[way.Count - 1];
            prePointI++;
            CurrOffset += totalLength - CurrOffset;
            curCondition = WaitingForRequest;
            gameObject.SendMessage("PursuitIsFinished");
            if (moveVectorOrientation) FollowTheDirection(prePosition);
            return;
        }
        if (Vector3.Distance(transform.position, way[prePointI + 1]) > speed)
        {
            prePosition = transform.position;
            transform.position += speed * (way[prePointI + 1] - transform.position).normalized;
            CurrOffset += speed;
            if (moveVectorOrientation) FollowTheDirection(prePosition);
            return;
        }
        if (Vector3.Distance(transform.position, way[prePointI + 1]) == speed)
        {
            prePosition = transform.position;
            transform.position = way[prePointI + 1];
            CurrOffset += speed;
            prePointI++;
            if (moveVectorOrientation) FollowTheDirection(prePosition);
            return;
        }
        if (Vector3.Distance(transform.position, way[prePointI + 1]) < speed)
        {
            float offset = 0;
            Vector3 cur_pos = transform.position;
            while ((speed - offset) > Vector3.Distance(way[prePointI + 1], cur_pos))
            {
                offset += Vector3.Distance(way[prePointI + 1], cur_pos);
                prePointI++;
                cur_pos = way[prePointI];
                if (prePointI + 1 == way.Count)
                {
                    prePosition = transform.position;
                    transform.position = cur_pos;
                    if (moveVectorOrientation) FollowTheDirection(prePosition);
                    return;
                }
            }
            cur_pos += (speed - offset) * (way[prePointI + 1] - way[prePointI]).normalized;
            prePosition = transform.position;
            transform.position = cur_pos;
            CurrOffset += speed;
            if (moveVectorOrientation) FollowTheDirection(prePosition);
            return;
        }
    }

    //Coroutine that stops the path searching couroutine through delay after start if automaticSearchStop is true 
    IEnumerator KillSearch(float delay)
    {
        Coroutine curActualCoroutine = AStarCoroutine;
        yield return new WaitForSeconds(delay);
        if(AStarCoroutine==curActualCoroutine)
        CancelWaySearch();
    }

    //procedure-receiver of the message about moving along the path ending
    void PursuitIsFinished()
    {

    }

    //procedure-receiver of the message about interruption the search for the path
    void WaySearchingIsCanceled()
    {
        Debug.Log("Way searching has been canceled");
    }
}
