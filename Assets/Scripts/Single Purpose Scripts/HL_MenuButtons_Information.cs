using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HL_MenuButtons_Information : MonoBehaviour {

    protected bool bl_LevelOpen; // a bool that is activated based on save information
    protected int int_MyLevel;   // information of level this button holds

    public Sprite[] ButtonStates; // used to signify changes in button states for player visibility

    protected Button myButton;
    protected Image myImage;
	// Use this for initialization
	void Start () {
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        myButton.enabled = false;
        myImage.sprite = ButtonStates[0];

	}
	
	// Update is called once per frame
	void Update () {
        ButtonStateChange();
    }
    // change state of buttons dependent of available levels
    protected void ButtonStateChange()
    {
        if(bl_LevelOpen == true)
        {
            myButton.enabled = true;
            myImage.sprite = ButtonStates[1];
        }
    }
    // triggered by button press to send mesage to the manager with the level information
    public void ButtonClicked()
    {
        HL_MainManager.instance.SerCurent_Level(int_MyLevel);
        HL_MainManager.instance.SceenManagement(int_MyLevel);
    }
    //-----------------

    // external references for protection
    public int Level_ID()
    {
        return int_MyLevel;
    }
    public void AsingLevelID(int AsignedID)
    {
        int_MyLevel = AsignedID;
    }
    public void SetBool(bool SetState)
    {
        bl_LevelOpen = SetState;
    }
}
