using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interfaceController : MonoBehaviour
{
    Pursuer pursuerInstance;
    public GameObject pathfinder;
    void Start()
    {
        pursuerInstance = pathfinder.GetComponent<Pursuer>();
    }
    //calling the user defined methods and changing the class fields for changing of the search parameters and pursuer movement :

    //path optimization on/off
    public void switchOptimization()
    {
        pursuerInstance.trajectoryOptimization = !pursuerInstance.trajectoryOptimization;
    }

    //smoothing on/off
    public void switchSmoothing()
    {
        pursuerInstance.trajectorySmoothing = !pursuerInstance.trajectorySmoothing;
    }
    //pursuer speed assignment 
    public void changeSpeed(float sp)
    {
        pursuerInstance.SetSpeed(sp);
    }

    //upper constraint (yMax) assignment
    public void changeSearchAreaY(float ymax)
    {
        pursuerInstance.yMax = ymax;
        pursuerInstance.RedefineConstraints(pursuerInstance.xMin, pursuerInstance.xMax, pursuerInstance.yMin, ymax, pursuerInstance.zMin, pursuerInstance.zMax, pursuerInstance.spaceFragmentation, 0.7d);
    }
}
