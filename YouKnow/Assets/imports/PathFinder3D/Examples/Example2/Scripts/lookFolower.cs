using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookFolower : MonoBehaviour {

    public Transform target;
	

	void Update () {
        //a turn to the point between Vector3.zero and target coordinate
        transform.rotation = Quaternion.LookRotation(Vector3.zero + 0.5f*(target.position-Vector3.zero) - transform.position);

    }
}
