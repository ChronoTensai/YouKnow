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

    public AudioClip GameOverSound;
    public AudioClip Music;

    // Use this for initialization
	private const int goalVisited = 3;
	private int progressVisited;
    private float timeToEnd = 15;
	
	void Start () 
	{
		MODEL.GAME_MANAGER = this;

	    StartGame();
	}
	
	private void StartGame()
	{
        progressVisited = 0;
		MODEL.GAME_STATE = GameStates.Playing;
        MODEL.SOUND_MANAGER.PlayAudio(GameOverSound);

        Invoke("YouLose", timeToEnd);
	}
	
	public void YouLose()
	{
        CancelInvoke("YouLose");
        MODEL.SOUND_MANAGER.StopAllAudio();
        MODEL.SOUND_MANAGER.PlayAudio(GameOverSound);
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
