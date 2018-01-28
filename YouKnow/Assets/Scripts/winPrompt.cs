using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winPrompt : MonoBehaviour
{


    public GameObject obj;
   
    public void Update()
    {

        if (MODEL.GAME_STATE == GameStates.Win)
        {
            obj.SetActive(true);
        }
    }



    public void LoadLevel()
    {
        switch (MODEL.GAME_MANAGER.currentLevel)
        {
            case 1:
                SceneManager.LoadScene(SCENE.LEVEL_TWO);
                break;
            case 2:
                SceneManager.LoadScene(SCENE.LEVEL_THREE);
                break;
            case 3:
                SceneManager.LoadScene(SCENE.LEVEL_FOUR);
                break;
            case 4:
                SceneManager.LoadScene(SCENE.MAIN_MENU);
                break;
        }

    }

}




