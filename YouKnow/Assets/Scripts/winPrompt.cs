using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPrompt : MonoBehaviour
{


    private GameObject obj;
<<<<<<< HEAD
    private int count = 2;
=======

>>>>>>> cf05a8a5144d8526f3471370ed23e7ea2c648da4
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
<<<<<<< HEAD
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
=======
        SceneManager.LoadScene(SCENE.LEVEL_TWO);
    }

}

>>>>>>> cf05a8a5144d8526f3471370ed23e7ea2c648da4
