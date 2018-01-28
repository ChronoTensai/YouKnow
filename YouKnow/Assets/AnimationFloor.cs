using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFloor : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer _sprite;
	void Start () {
        _sprite = this.gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating("AnimateThis", 0.5f, 0.5f);
	}
	
	// Update is called once per frame
	void AnimateThis ()
    {

        if (DoFlip())
        { 
            _sprite.flipX = !_sprite.flipX;
            if (DoFlip())
            {
                _sprite.flipY = !_sprite.flipY;
            }
        }
        else
        {
            _sprite.flipY = !_sprite.flipY;
        }
            

    }

    bool DoFlip()
    {
        int random = Mathf.RoundToInt(Random.Range(0, 2));
        random = random == 2 ? 1 : random;

        return random == 1;
    }
}
