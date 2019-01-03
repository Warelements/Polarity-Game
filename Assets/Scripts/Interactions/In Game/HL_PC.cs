using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HL_PC : MonoBehaviour {
    public static HL_PC instance;

    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    protected Vector3 moveDirection = Vector3.zero;
    [SerializeField] public CharacterController2D controller;
    protected float HorisontalMove;
    [SerializeField] protected int Horisontal;
    public bool jump;
    [SerializeField] protected GameObject go_JumpButton;
    [SerializeField] private string ButonState = "Jump";

    // variables for Text Boxes when interacting
    protected GameObject CurentTextBox;

    // Use this for initialization
    void Start() {
        instance = this;
        controller = GetComponent<CharacterController2D>();
    }

    //these are the input for the buttons set trough the event system
    public void Left()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (HL_Joystick.instance.Bl_Amingn() == false)
                Horisontal = -1;
        }
    }
    public void Right()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (HL_Joystick.instance.Bl_Amingn() == false)
                Horisontal = 1;
        }
    }
    public void Stationary()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (HL_Joystick.instance.Bl_Amingn() == false)
                Horisontal = 0;
        }
    }
    public void Jump()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (ButonState == "Jump")
            {
                if (HL_Joystick.instance.Bl_Amingn() == false)
                    jump = true;
            }
            else if (ButonState == "Fire")
            {
                Destroy(HL_Joystick.instance.Handle());
                HL_Aim_Rotation.instance.Fire();
                HL_Joystick.instance.MovementButtons().SetActive(true);
                HL_Joystick.instance.SetAiming(false);
                HL_Joystick.instance.SetCreate(false);
                HL_Aim_Rotation.instance.Aim().SetActive(false);
                ButonState = "Jump";
                HL_Joystick.instance.SwichCanMove(false);
            }
            else if (ButonState == "Interact")
            {
                CurentTextBox.GetComponent<HL_Text_Trigger>().TriggerInteractableBox();
            }
        }
    }
    //--------------

    // Update is called once per frame
    void Update() {
        HorisontalMove = Horisontal * m_MaxSpeed;
        SetText();
    }
    void FixedUpdate()
    {
        controller.Move(HorisontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    // this sets the text to the jump button depending on the state;
    void SetText()
    {
        if (ButonState == "Jump")
        {
            go_JumpButton.GetComponentInChildren<Text>().text = "Jump";
        }
        if (ButonState == "Fire")
        {
            go_JumpButton.GetComponentInChildren<Text>().text = "Fire";
        }
        if (ButonState == "Interact")
        {
            go_JumpButton.GetComponentInChildren<Text>().text = "Interact";
        }
    }

    public void SetString(string NewState)
    {
        ButonState = NewState;
    }
    public void SetTextBox(GameObject NewCurentTextBox)
        {
        CurentTextBox = NewCurentTextBox;
        }
}
