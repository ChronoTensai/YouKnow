using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPrompt : MonoBehaviour
{


    private GameObject obj;

    public void Start()
    {
        obj = GameObject.FindGameObjectWithTag("winPrompt");
        obj.SetActive(false);
    }


    public void Update()
    {

        if (MODEL.GAME_STATE == GameStates.Win)
        {
            obj.SetActive(true);
        }
    }



    public void LoadLevel()
    {
        Debug.Log(MODEL.GAME_STATE);
        obj.SetActive(false);
        SceneManager.LoadScene(SCENE.LEVEL_TWO);
    }

}

