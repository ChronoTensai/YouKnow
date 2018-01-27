using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleClass : MonoBehaviour
{
    //Interception of a message sent from current “Pursuer” instance about ending of moving along the path
    public void PursuitIsFinished()
    {
        gameObject.GetComponent<Light>().intensity = 50;
        Destroy(gameObject, 0.2f);
    }
    //Interception of a message sent from current “Pursuer” instance about interruption of the search for the path
    void WaySearchingIsCanceled() {
        Destroy(gameObject);

    }
}
