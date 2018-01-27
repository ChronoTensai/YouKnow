using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		LoadResources.SetParents();
	}
	
	private GameManager _gameManger;
	
	void Start()
	{
	    LoadResources.LoadGameResource("Classroom");
	    _gameManger = new GameManager();
	}
	
	
}
