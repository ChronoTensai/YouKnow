using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgressorClass : MonoBehaviour {

    public GameObject misslePrefab;
    public Transform marker;
    GameObject missleInstance;
    bool missleSpawned;
    Vector3 targetPoint;
    void Update () {
        //movements of the pointer (marker) through the game scene
        Vector3 mouse_position = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse_position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        marker.position = hit.point;

        //rocket launching toward the pointer(marker)
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit) &&  hit.normal == new Vector3(0, 1, 0))
                LaunchAMissle(hit.point + new Vector3(0, 1f, 0));
        }
        //request of moving to the marker coordinate +(0,10f,0)
        if (Input.GetMouseButtonDown(1))
        {
           
            if (Physics.Raycast(ray, out hit) && hit.normal == new Vector3(0,1,0))
            {
                gameObject.GetComponent<Pursuer>().CancelMovement();
                gameObject.GetComponent<Pursuer>().CancelWaySearch();
                gameObject.GetComponent<Pursuer>().MoveTo(hit.point + new Vector3(0, 10f, 0));
            }
        }
    }

    void LaunchAMissle(Vector3 pos)
    {
        missleInstance = Instantiate(misslePrefab,transform.position,Quaternion.identity);
        missleSpawned = true;
        targetPoint = pos;
    }

    //calling the search for the path is performed in LateUpdate because “Pursuer” instance creation 
    //must be followed by the body of “Awake” performing for the core classes initialization.
    private void LateUpdate()
    {
        if (missleSpawned)
        {
            missleInstance.GetComponent<Pursuer>().MoveTo(targetPoint);
            missleSpawned = false;
        }
    }
}
