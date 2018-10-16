using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HL_UI_Manager : MonoBehaviour {

    public GameObject go_LevelPannel;
    public GameObject go_MainButtons;
    public GameObject go_ButtonHolder;
    protected GameObject[] ButtonList;

    // variables for moing level selection

    public bool bl_PannelOpen;
    public Vector2 v2_ClickLocation;
    public int int_MaxRight_Value;
    public int int_MaxLeft_Value;
    public int Curent_value;

    public float fl_Pannel_Move_Speed;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i <= go_ButtonHolder.transform.childCount - 1; i++)
        {
            go_ButtonHolder.transform.GetChild(i).GetComponent<HL_MenuButtons_Information>().AsingLevelID(i + 1);
        }
        ButtonList = GameObject.FindGameObjectsWithTag("MainButtons");
        SetButtonsAvailablility();

        go_LevelPannel.SetActive(false);
    }
    private void Update()
    {
        DragPannel();
    }
    // quits aplication all together
    public void Quit()
    {
        Application.Quit();
    }
    //------

    //opens pannel for level selection
    public void LevelSelect()
    {
        go_MainButtons.SetActive(false);
        go_LevelPannel.SetActive(true);

    }
    //---------

    // back button from level selection
    public void Back()
    {
        go_MainButtons.SetActive(true);
        go_LevelPannel.SetActive(false);

    }
    //------
    void SetButtonsAvailablility()
    {
        foreach (GameObject go in ButtonList)
        {
            if (go.GetComponent<HL_MenuButtons_Information>().Level_ID() <= HL_SaveLoad.instance.LevelTotal())
            {
                print(go.name);
                go.GetComponent<HL_MenuButtons_Information>().SetBool(true);

            }
        }
    }
    // loads the next level that has not been played
    public void ContinueFromLastLevel()
    {
        HL_MainManager.instance.SerCurent_Level(HL_SaveLoad.instance.LevelTotal());
        HL_MainManager.instance.SceenManagement(HL_SaveLoad.instance.LevelTotal());

    }
    //----------------
    void DragPannel()
    {
        //go_LevelPannel.GetComponent<RectTransform>().position.x = 2

        if (Input.GetMouseButtonDown(0))

        {
            v2_ClickLocation = Input.mousePosition;
        }
        if (Input.mousePosition.x < v2_ClickLocation.x && Input.GetButton("Fire1"))
        {
           // Curent_value++;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        Curent_value--;
        if (eventData.dragging)
        {
            print("Dragging");
        };

    }
}
