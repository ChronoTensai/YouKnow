using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Message : MonoBehaviour
{
    const float startVel = 0.1f;
    float actualVel;
    
    public bool isHide = false;
    
    void Start()
    {
        actualVel = startVel;
    }
    
   
    
    void Update()
    {
        Debug.Log(this.transform.forward * Time.time * actualVel);
        this.transform.Translate(this.transform.right * Time.time * actualVel);
    }
    
    
    
    void OnCollisionEnter2D (Collision2D collider)
    {
        if(collider.gameObject.layer == LAYER.WALL)
        {
            //bounce 
            actualVel = 40 * actualVel / 100;
        }        
        else if(collider.gameObject.layer == LAYER.ENEMY || 
                collider.gameObject.layer == LAYER.FRIEND || 
                collider.gameObject.layer == LAYER.NEUTRAL)
        {
            collider.gameObject.GetComponent<Character>().GetTheMessage();
        }
    }

}

