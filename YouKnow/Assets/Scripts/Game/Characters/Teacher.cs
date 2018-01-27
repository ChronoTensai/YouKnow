using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : Character {

	// Use this for initialization
	enum TeacherStates
	{
	   None,
	   Idle,
	   Walking,
	   Spot
	}
	
	TeacherStates _state;
	Vector2 MinMaxTimeToChange;
	
	public void SetNodes(Transform[] transformNode, Vector2 minMaxTimeToChange)
	{
	   MinMaxTimeToChange = minMaxTimeToChange;
	}
	
	void Start () 
	{
		
	}
	
	public void StartGame()
	{
	   _state = TeacherStates.Idle;
	}
	
	// Update is called once per frame
	void Update () 
	{
	    switch(_state)
	    {
	        case TeacherStates.Idle:
	            Idle();
	            break;
	        case TeacherStates.Walking:
	            Walking();
	            break;	       
	    }
	}
	
	private void StartIdle()
	{
	    Invoke("EndIdle", Random.Range(MinMaxTimeToChange.x, MinMaxTimeToChange.y));
	}
	
	private void Idle()
	{
	    Watch();
	}
	
	private void EndIdle()
	{
	   //GettheMostClose nodes
	}
	
	private void Walking()
	{
        //LookAt
        //WalkTo
        Watch();
	}
	
	private void Watch()
	{
	    //CheckPaperInAir
	    //CheckPaperInFront
	}
	
	private void Spot()
	{
	   //EndOfGame
	}
	
}
