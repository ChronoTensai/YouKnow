using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Unused class
public class CircularRotation : MonoBehaviour {

    float alpha = 0;
	float radius = 24;
	void FixedUpdate () {
        alpha += 0.5f;
        float rad_alpha = (alpha * (float)Mathf.PI) / 180.0F;
        float x = radius * (float)Mathf.Cos(rad_alpha);
        float z = radius * (float)Mathf.Sin(rad_alpha);

        transform.position = new Vector3(x, transform.position.y, z);
        if (alpha == 360) alpha = 0;

        transform.rotation = Quaternion.LookRotation(Vector3.zero - transform.position);
    }
}
