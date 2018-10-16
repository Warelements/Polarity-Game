using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HL_Test_Levels : MonoBehaviour {
    public Texture btnTexture;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	

	}
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 200, 30), HL_MainManager.instance.Curent_Level_Ref().ToString()))
            Debug.Log("Clicked the button with text");

        if (GUI.Button(new Rect(10, 270, 600, 150), Application.persistentDataPath))
            Debug.Log(Application.persistentDataPath);

        if (GUI.Button(new Rect(10, 170, 50, 30), "Load Next Level"))
        {
           
            HL_MainManager.instance.SceenManagement(HL_MainManager.instance.Curent_Level_Ref() + 1);
            HL_MainManager.instance.SerCurent_Level(HL_MainManager.instance.Curent_Level_Ref() + 1);
            HL_MainManager.instance.TriggerSave();
        }
    }
}
