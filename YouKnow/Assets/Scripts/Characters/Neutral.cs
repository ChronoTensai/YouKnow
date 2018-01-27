using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral : Character {

    public bool HasTheBall;
    public GameObject Message;
    protected bool visited;
    
	
	// Update is called once per frame
	void Update () 
	{
	     
	    if(HasTheBall)
	    {
	        //Shoot
	        if(Input.GetMouseButtonUp(0))
    	    {
	            Shoot();
    	    }
	    }
	}
	
	void Shoot()
	{
	    HasTheBall = false;
        Instantiate(Message, this.transform.position, Quaternion.identity);
	}
	
    public override void GetTheMessage(GameObject message)
    {
        if(!visited)
        {
            visited = true;
            HasTheBall = true;
            base.GetTheMessage(message);
        }
    }  
}
