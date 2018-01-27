using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_task : MonoBehaviour {
    public GameObject plane;
    public Transform target;
    Pursuer pursuerInstance;
    private void Start()
    {
        pursuerInstance = plane.GetComponent<Pursuer>();
        pursuerInstance.MoveTo(target.position);
    }
    // Update is called once per frame
    void Update()
    {

        //new target assignment in case the pursuer is waiting for a command
        if (pursuerInstance.GetStatus() == "Void WaitingForRequest()")
        {
            //Indentation from the edges of the search area
            RaycastHit hit;
            float xmin = pursuerInstance.xMin + pursuerInstance.spaceFragmentation;
            float xmax = pursuerInstance.xMax - pursuerInstance.spaceFragmentation;
            float zmin = pursuerInstance.zMin + pursuerInstance.spaceFragmentation;
            float zmax = pursuerInstance.zMax - pursuerInstance.spaceFragmentation;
            Physics.Raycast(new Vector3(Random.Range(xmin,xmax), 200, Random.Range(zmin,zmax)), Vector3.down, out hit);
            Vector3 targetPos = hit.point + hit.normal * 10 + Vector3.up * 10;

            if (hit.point.y < 2 && Vector3.Distance(targetPos, plane.transform.position) > 25)
            {
                pursuerInstance.MoveTo(targetPos);
                target.position = targetPos;
            }

        }
    }
}
