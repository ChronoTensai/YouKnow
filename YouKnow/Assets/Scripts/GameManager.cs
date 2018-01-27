using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
    {
        Playing,
        Lose,
        Win
    }

public class GameManager : MonoBehaviour {

    // Use this for initialization
	private const int goalVisited = 3;
	private int progressVisited;
	
	void Start () 
	{
		MODEL.GAME_MANAGER = this;
	    StartGame();
	}
	
	private void StartGame()
	{
        progressVisited = 0;
		MODEL.GAME_STATE = GameStates.Playing;
	}
	
	public void YouLose()
	{
	    MODEL.GAME_STATE = GameStates.Lose;
	    Debug.Log("Lose");
	}
	
	public void StepToWin()
	{
	    Debug.Log("StepToWin");
	    progressVisited++;
	    if(progressVisited == goalVisited)
	    {
	        Win();	       
	    }
	}
	
	public void Win()
	{
	    MODEL.GAME_STATE = GameStates.Win;
	    Debug.Log("Win");
	    
	}
}
