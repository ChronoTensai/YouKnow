using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winPrompt : MonoBehaviour
{


    public GameObject obj;
    bool showing = false;
    public int currentLevel;

    public void Update()
    {

        if (!showing && MODEL.GAME_STATE == GameStates.Win)
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
        Debug.Log(MODEL.GAME_STATE);
        obj.SetActive(false);
        currentLevel++;

        switch (currentLevel)
        {
            case 2:
                SceneManager.LoadScene(SCENE.LEVEL_TWO);
                break;
            case 3:
                SceneManager.LoadScene(SCENE.LEVEL_THREE);
                break;
            case 4:
                SceneManager.LoadScene(SCENE.LEVEL_FOUR);
                break;
        }

    }

}




