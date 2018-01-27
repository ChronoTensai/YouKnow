using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousPursuer : MonoBehaviour {
    public Transform target;
    Vector3 targetOldPos;
    Pursuer pursuerInstance;

    private void Start()
    {
        targetOldPos = Vector3.zero;
        pursuerInstance = gameObject.GetComponent<Pursuer>();
    }

    void Update () {
        //moving to the target in case it changes its coordinate
        if (target.position!=targetOldPos)
        {
            //pursuer states check, request of moving to the target, previous coordinate assignment

            if (pursuerInstance.GetStatus() == "Void Movement()")
            {
                pursuerInstance.CancelMovement();
                pursuerInstance.MoveTo(target);
                targetOldPos = target.position;
                return;
            }
            if (pursuerInstance.GetStatus() == "Void WaitingForAWay()")
            {
                pursuerInstance.CancelWaySearch();
                pursuerInstance.MoveTo(target);
                targetOldPos = target.position;
                return;
            }
            if (pursuerInstance.GetStatus() == "Void WaitingForRequest()")
            {
                pursuerInstance.MoveTo(target);
                targetOldPos = target.position;
                return;
            }
            
        }
	}
}
