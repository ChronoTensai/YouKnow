using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallIndicator : MonoBehaviour {

    public GameObject Warning;
    private Transform Teacher;

    private bool ShowingWarning = false;

    void Start()
    {
        Teacher = GameObject.FindGameObjectWithTag("Teacher").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 teacherFront = Teacher.TransformDirection(Vector2.up);
        Vector2 direction = this.transform.position - Teacher.position;
        if (Vector2.Dot(teacherFront, direction) > 0)
        {
            if (!ShowingWarning)
            {
                Warning.SetActive(true);
                ShowingWarning = true;
            }
        }
        else if (ShowingWarning)
        {
            Warning.SetActive(false);
            ShowingWarning = false;
        }
    }
}
