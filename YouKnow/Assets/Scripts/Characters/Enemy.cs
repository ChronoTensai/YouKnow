using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public override void GetTheMessage(GameObject message)
    {
        MODEL.GAME_MANAGER.YouLose();
        base.GetTheMessage(message);
    }
    
}
