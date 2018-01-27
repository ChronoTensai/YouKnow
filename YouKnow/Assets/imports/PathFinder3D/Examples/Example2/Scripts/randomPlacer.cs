using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPlacer : MonoBehaviour {

    public Transform rocket;
	// Update is called once per frame
	void Update () {
        //target moves to the opposite side of the scene in case the pursuer is close enough
        if (Vector3.Distance(rocket.position, transform.position) < 7)
        {
            if(transform.position.x == -200) {
                transform.position = new Vector3(200, Random.Range(-50, 50), Random.Range(-50, 50));
            }else
            {
                transform.position = new Vector3(-200, Random.Range(-50, 50), Random.Range(-50, 50));
            }
        }
	}
}
