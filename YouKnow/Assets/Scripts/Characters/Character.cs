using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public AudioClip CatchAudio;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(MODEL.GAME_STATE == GameStates.Playing)
        {
            if(coll.gameObject.layer == LAYER.MESSAGE)
            {
                MODEL.SOUND_MANAGER.PlayAudio(CatchAudio);
                this.GetTheMessage(coll.gameObject);
            }
        }
    }
    
    public virtual void GetTheMessage(GameObject message)
    {
        Destroy(message);
    }
    
    
}
