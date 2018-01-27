using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private Action<GamemangerMessages> _notifyGameManager;
    
    public virtual void GetTheMessage()
    {
        //Notify GameManager send type
    }    
    
    private void Initialize(Action<GamemangerMessages> notifyGameManager)
    {
        _notifyGameManager = notifyGameManager;
    }
    
   
    
}
