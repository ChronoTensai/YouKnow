using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class losePrompt : MonoBehaviour {


	public GameObject obj;
	bool showing = false;

	public void Update()
	{
        if (!showing && MODEL.GAME_STATE == GameStates.Lose)
        {
            obj.SetActive(true);
        }
        else if(showing && Input.GetMouseButtonDown(0))
        {
            LoadLevel();
        }
	}



	public void LoadLevel()
	{
		Debug.Log (MODEL.GAME_STATE);
		obj.SetActive (false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void QuitToMainMenu()
	{
		Debug.Log (MODEL.GAME_STATE);
		obj.SetActive (false);
		SceneManager.LoadScene(SCENE.MAIN_MENU);
	}
}
