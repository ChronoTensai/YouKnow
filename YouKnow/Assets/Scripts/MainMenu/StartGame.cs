using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public void LoadGame()
    {
        SceneManager.LoadScene(SCENE.GAME);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
