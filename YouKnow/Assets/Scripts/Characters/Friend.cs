using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friend : Neutral {

    void Start()
    {
        if (HasTheBall)
        { 
            visited = true;
            ShowIndicator();
        }
    }
    
    public override void GetTheMessage(GameObject message)
    {
        if(!visited)
        {
            Destroy(message);
            MODEL.GAME_MANAGER.StepToWin();
            visited = true;
            ShowIndicator();

            if (MODEL.GAME_STATE != GameStates.Win)
            {
                HasTheBall = true;
                
            }
        }
    }

}
