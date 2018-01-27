using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : Enemy {

	bool looking = false;

    // Use this for initialization
	public float minTimeToTurn = 4;
	public float maxTimeToTurn = 9;
	
	public Node StartNode;
    public Node[] FollowNode;
    private Node ToFollow;
    private Node BeforeFollow;
    private bool desicion = false;
    //private int point = 0;
    public float speed = 20f;
    private float closeEnough = 0f;



    void Start () {
	    Invoke("LookScholars", Random.Range(minTimeToTurn,maxTimeToTurn));
        this.transform.SetPositionAndRotation(StartNode.transform.position,Quaternion.LookRotation(StartNode.transform.position- this.transform.position, transform.TransformDirection(Vector3.forward))); //No se como hacer que mire en el sentido que se va a dirigir
        ToFollow = FollowNode[0];

    }
	
	void LookScholars()
	{
	   looking = true;

       
	   Vector3 newRotation = Vector3.zero;
	   newRotation.z = 180;
	   this.transform.eulerAngles = newRotation;
	       Invoke("LookBlackboard", Random.Range(1,3));
	   
	       
	}
	
	void Update()
	{
	    if(looking)
	    {
	       GameObject go = GameObject.FindGameObjectWithTag("Message");
	        if(go != null)
    	   {
	            Destroy(go);
	            MODEL.GAME_MANAGER.YouLose();
    	       CancelInvoke("LookBlackboard");
    	   }
	    }

        if (!looking)
        {
            Move();
        }

    }

    void LookBlackboard()
    {
        looking = false;
        this.transform.eulerAngles = Vector3.zero;

        Invoke("LookScholars", Random.Range(minTimeToTurn, maxTimeToTurn));


    }


    void Move()
    {
        Quaternion rotation = Quaternion.LookRotation(ToFollow.transform.position - this.transform.position, transform.TransformDirection(Vector3.forward));
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        this.transform.position = Vector3.MoveTowards(this.transform.position, ToFollow.transform.position, (Time.deltaTime/10) * speed);
        if (Vector3.Distance(this.transform.position, ToFollow.transform.position) <= closeEnough)
        {
          //  if (!BeforeFollow)
          //      BeforeFollow.LastVisited = false;
            ToFollow.LastVisited = true;
          //  BeforeFollow = ToFollow;

            while (!desicion)
            {
                switch (Random.Range(1, 5))
                {
                    case 1:
                        if (ToFollow.Top && !ToFollow.Top.LastVisited)
                        {
                            ToFollow.LastVisited = false;
                            ToFollow = ToFollow.Top;
                            desicion = true;
                        }
                        break;
                    case 2:
                        if (ToFollow.Right && !ToFollow.Right.LastVisited)
                        {
                            ToFollow.LastVisited = false;
                            ToFollow = ToFollow.Right;
                            desicion = true;
                        }
                        break;
                    case 3:
                        if (ToFollow.Bot && !ToFollow.Bot.LastVisited)
                        {
                            ToFollow.LastVisited = false;
                            ToFollow = ToFollow.Bot;
                            desicion = true;
                        }
                        break;
                    case 4:
                        if (ToFollow.Left && !ToFollow.Left.LastVisited)
                        {
                            ToFollow.LastVisited = false;
                            ToFollow = ToFollow.Left;
                            desicion = true;
                        }
                        break;
                }
                
            }
            desicion = false;

        }
    }
}
