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
    public int point = 0;
    public float speed = 10f;
    private float closeEnouth = 0.5f;



    void Start () {
	    Invoke("LookScholars", Random.Range(minTimeToTurn,maxTimeToTurn));
        this.transform.SetPositionAndRotation(StartNode.transform.position,Quaternion.LookRotation(StartNode.transform.position- this.transform.position, transform.TransformDirection(Vector3.forward))); //No se como hacer que mire en el sentido que se va a dirigir
        
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
    void LookWallkDirection()
    {

    }

    void Move()
    {
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, FollowNode[point].transform.position, (Time.deltaTime/10) * speed);
        if (Vector3.Distance(this.transform.position, FollowNode[point].transform.position) < closeEnouth)
        {
            if (point + 1 < FollowNode.Length)
                point++;
            else
                point = 0;

        }
    }
}
