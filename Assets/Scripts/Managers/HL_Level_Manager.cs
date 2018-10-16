using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Level_Manager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // when pc finishes level it communicates with the main manager and triggers save calculation 
    public void LevelComplete()
    {
        HL_MainManager.instance.TriggerSave();
    }
}
