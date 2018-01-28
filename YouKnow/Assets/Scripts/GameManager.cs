using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float timeToEnd = 60;
    private float timeLeft;
    public int currentLevel;
	
	void Awake() 
	{
		MODEL.GAME_MANAGER = this;	    
	}
	
	void Start()
	{
	   StartGame();
	}
	
	private void StartGame()
	{
        progressVisited = 0;
		MODEL.GAME_STATE = GameStates.Playing;
        MODEL.SOUND_MANAGER.PlayAudio(Music);

        Invoke("YouLose", timeToEnd);
        
        timeLeft = timeToEnd;
        InvokeRepeating("UpdateTimeLeft", 0, 1);
        UpdateTargetLeft();
	}
	
	public void YouLose()
	{
        CancelInvoke("YouLose");
	    CancelInvoke("UpdateTimeLeft");
	    
        MODEL.SOUND_MANAGER.StopAllAudio();
        MODEL.SOUND_MANAGER.PlayAudio(GameOverSound);
        MODEL.GAME_STATE = GameStates.Lose;
	    Debug.Log("Lose");
	}
	
	public void StepToWin()
	{
	    Debug.Log("StepToWin");
	    progressVisited++;
	    UpdateTargetLeft();
	    if(progressVisited == goalVisited)
	    {
	        Win();	       
	    }
	}
	
	public void Win()
	{
	    CancelInvoke("YouLose");
	    CancelInvoke("UpdateTimeLeft");
	    MODEL.GAME_STATE = GameStates.Win;
	    Debug.Log("Win");
	    
	}
	
	public Text txtCurrentLevel;
	public Text txtTargetLeft;
	public Text txtTimeLeft;
	
	public void SetCurrentLevel()
	{
	    txtCurrentLevel.text = "Current Level: " + currentLevel;
	}
	
	public void UpdateTargetLeft()
	{
	    txtTargetLeft.text = "Children Left: "  + (goalVisited - progressVisited).ToString();
	}
	
	public void UpdateTimeLeft()
	{
	   timeLeft--;
	   txtTimeLeft.text = "TimeLeft: " + timeLeft;
	}
}
