using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class losePrompt : MonoBehaviour {


	private GameObject obj;

	public void Start()
	{
		obj = GameObject.FindGameObjectWithTag("losePrompt");
		obj.SetActive (false);
	}


	public void Update()
	{

		if (MODEL.GAME_STATE == GameStates.Lose) {
			obj.SetActive (true);
		}
	}



	public void LoadLevel()
	{
		Debug.Log (MODEL.GAME_STATE);
		SceneManager.LoadScene(SCENE.LEVEL_ONE);
	}

	public void QuitToMainMenu()
	{
		Debug.Log (MODEL.GAME_STATE);
		SceneManager.LoadScene(SCENE.MAIN_MENU);
	}
}
