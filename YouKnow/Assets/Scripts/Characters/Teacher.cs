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
	
	void Start () {
	    Invoke("LookScholars", Random.Range(minTimeToTurn,maxTimeToTurn));
        this.transform.SetPositionAndRotation(StartNode.transform.position,Quaternion.LookRotation(StartNode.transform.position));
        
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
	}
	
	void LookBlackboard()
	{
	   looking = false;
	    this.transform.eulerAngles = Vector3.zero;
	   
	   Invoke("LookScholars", Random.Range(minTimeToTurn,maxTimeToTurn));
	
	}


}
