using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInitializer : MonoBehaviour {

    void Awake()
    {
        LoadResources.SetParents();
    }
    void Start ()
	{
	    LoadResources.LoadUIResource("MainMenu");	    
	    LoadResources.LoadGameResource("background");
	}
}
