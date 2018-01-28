using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winPrompt : MonoBehaviour
{


    private GameObject obj;

    private int count = 2;



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

        switch (count)
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
        count++;
    }

}




