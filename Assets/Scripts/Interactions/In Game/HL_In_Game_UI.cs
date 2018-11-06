using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HL_In_Game_UI : MonoBehaviour {
    protected bool bl_OpenMenu;
    public static bool Paused;
    [SerializeField] protected GameObject go_MenuUI;
    [SerializeField] protected GameObject JumpButton;
    [SerializeField] protected GameObject MoveButtons;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // reduce time to 0 across all things that use time. 
        // anything that does not use time has to reaaly on the pause bool
		if(Paused == true)
        {
            Time.timeScale = 0f;
        }
        else if(Paused == false)
        {
            Time.timeScale = 1f;
        }

	}
    // open and close UI menu for the level
    public void Open_Close_Menu()
    {
        bl_OpenMenu = !bl_OpenMenu;
        if (bl_OpenMenu == true)
        {
            go_MenuUI.SetActive(true);
            JumpButton.SetActive(false);
            MoveButtons.SetActive(false);
            Paused = true;
            Time.timeScale = 0f;
        }
        else if(bl_OpenMenu == false)
        {
            go_MenuUI.SetActive(false);
            JumpButton.SetActive(true);
            MoveButtons.SetActive(true);
            Paused = false;
            Time.timeScale = 1;
        }
    }
    // reset the curent level that is beeing played
    public void ResetLevel()
    {
        Paused = false;
        SceneManager.LoadScene("Level " + HL_MainManager.instance.Curent_Level_Ref());
       
    }
    //-------------
    
    // load main menu during game  
    public void BackTo_Menu()
    {
        // the reference has to change if the name is different
        Paused = false;
        SceneManager.LoadScene("Main Menu");

    }
    //--------

    // quit game on button click
    public void Quit_Level()
    {
        Application.Quit();
    }
    //----------
}
