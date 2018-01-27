using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message : MonoBehaviour {

    // Use this for initialization
    public AudioClip PopMessage;
	public float Velocity = 3;
	private Vector2 _velocity;
	
	void Start () {
		Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = Input.mousePosition - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        _velocity = Vector3.right * Velocity;
        MODEL.SOUND_MANAGER.PlayAudio(PopMessage);
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.Translate(_velocity * Time.deltaTime);
	    
	    
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
	    if(coll.gameObject.layer == LAYER.BLACKBOARD_WALL)
	    {
	        MODEL.GAME_MANAGER.YouLose();
	    }
	    else if(coll.gameObject.layer == LAYER.WALL)
	    {
	        Debug.Log(_velocity);
	        //_velocity = Vector2.(_velocity, coll.contacts[0].normal);
	       // float angle = Vector2.Angle(coll.contacts[0].normal, this.transform.right);
	        this.transform.right = Vector2.Reflect(this.transform.position, coll.contacts[0].normal);
	        //Debug.Log("angle: " +angle);
            //Debug.Break();

	    }
	}
}
