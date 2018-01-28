using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OntriggerEnterSound : MonoBehaviour {


    public GameObject cuackPlant;
    private SpriteRenderer originalPlant;

    void Awake()
    {
        originalPlant = this.transform.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        
        cuackPlant.SetActive(true);
        originalPlant.enabled = false;
        Destroy(this);
    }
}
