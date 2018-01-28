using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

    void Start()
    {
        Screen.SetResolution(800, 600, false);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(SCENE.LEVEL_ONE);
    }
    
    public void ApplicationQuit()
    {
        Application.Quit();
    }
}
