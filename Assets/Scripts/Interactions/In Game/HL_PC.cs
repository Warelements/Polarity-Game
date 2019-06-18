using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HL_PC : MonoBehaviour
{
    public static HL_PC instance;

    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    protected Vector3 moveDirection = Vector3.zero;
    [SerializeField] public CharacterController2D controller;
    protected float HorizontalMove;
    [SerializeField] protected int Horizontal;
    public bool jump;
    [SerializeField] protected GameObject go_JumpButton;
    [SerializeField] private string ButtonState = "Jump";
    // variables for Text Boxes when interacting
    protected GameObject CurentTextBox;
    GameObject gun;
    [SerializeField] private bool Bl_OnRailing;
    private MU_Movewithmagnet Railing;

    // Use this for initialization
    void Start()
    {
        instance = this;
        controller = GetComponent<CharacterController2D>();
    }

    //these are the input for the buttons set trough the event system
    public void Left()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (HL_Joystick.instance.Bl_Aiming() == false)
                Horizontal = -1;
            gameObject.GetComponent<MU_Animations>().Ref_Speed(Horizontal);
        }
    }
    public void Right()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (HL_Joystick.instance.Bl_Aiming() == false)
                Horizontal = 1;
            gameObject.GetComponent<MU_Animations>().Ref_Speed(Horizontal);
        }
    }
    public void Stationary()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (HL_Joystick.instance.Bl_Aiming() == false)
                Horizontal = 0;
            gameObject.GetComponent<MU_Animations>().Ref_Speed(Horizontal);
        }
    }
    public void Jump()
    {
        if (HL_Joystick.instance.CanMove())
        {
            if (ButtonState == "Jump")
            {
                if (HL_Joystick.instance.Bl_Aiming() == false)
                    jump = true;
            }
            else if (ButtonState == "Fire")
            {
                Destroy(HL_Joystick.instance.Handle());
                HL_Aim_Rotation.instance.Fire();
                HL_Joystick.instance.MovementButtons().SetActive(true);
                HL_Joystick.instance.SetAiming(false);
                HL_Joystick.instance.SetCreate(false);
                HL_Aim_Rotation.instance.ResetAimLines();
                HL_Aim_Rotation.instance.Aim().SetActive(false);
                ButtonState = "Jump";
                HL_Joystick.instance.SwichCanMove(false);
            }
            else if (ButtonState == "Interact")
            {
                CurentTextBox.GetComponent<HL_Text_Trigger>().TriggerInteractableBox();
            }
        }
    }

    //--------------
    // Update is called once per frame
    void Update()
    {

        HorizontalMove = Horizontal * m_MaxSpeed;
        SetText();
    }
    void FixedUpdate()
    {
        // CheckifOnRailing();

        if (GetComponent<MU_RailingMechanics>() != null)
        {
            if (GetComponent<MU_RailingMechanics>().AttachedtoRailing)
            {
                print("base speed" + HorizontalMove);
                //if i am moving right AND railing is moving right
                if (HorizontalMove > 0 && GetComponent<MU_RailingMechanics>().RailingMovementdirection.x > 0)
                {
                    controller.Move(((HorizontalMove + (GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x)))*Time.fixedDeltaTime, false, jump);

                    print(" new speed =" + HorizontalMove + (GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x));
                }
                //if i am moving right AND railing is moving left OPPOSITE
                if (HorizontalMove > 0 && GetComponent<MU_RailingMechanics>().RailingMovementdirection.x < 0)
                {
                    controller.Move(((HorizontalMove + (GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x))/10)*Time.fixedDeltaTime, false, jump);

                    print(" new speed R+L =" + HorizontalMove + (GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x));
                }
                //if i am moving left AND railing is moving left
                if (HorizontalMove < 0 && GetComponent<MU_RailingMechanics>().RailingMovementdirection.x < 0)
                {
                    controller.Move(((HorizontalMove + -(GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x)))*Time.fixedDeltaTime, false, jump);

                    print(" new speed left =" + HorizontalMove + -(GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x));
                }
                //if i am moving left AND railing is moving right OPPOSITE
                if (HorizontalMove < 0 && GetComponent<MU_RailingMechanics>().RailingMovementdirection.x > 0)
                {
                    controller.Move(((HorizontalMove + -(GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x)) / 10) * Time.fixedDeltaTime, false, jump); //(((HorizontalMove + -(GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x))/10)*Time.fixedDeltaTime), false, jump);

                    print(" new speed L+R =" + HorizontalMove + -(GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x));
                }
                //print(" new speed ="+HorizontalMove + (GetComponent<MU_RailingMechanics>().railingspeed * GetComponent<MU_RailingMechanics>().RailingMovementdirection.x));
            }
        }


        //if (Bl_OnRailing == true)
        //{
        //    print(HorizontalMove + (Railing.Speed * Railing.MovementDirection.x));
        //    print("YOLO 1");
        //    controller.Move((HorizontalMove + (Railing.Speed*Railing.MovementDirection.x)) * Time.fixedDeltaTime, false, jump);
        //    print("YOLO 122");
        //} 


        else
        {
            controller.Move(HorizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }
    // this sets the text to the jump button depending on the state;
    void SetText()
    {
        if (ButtonState == "Jump")
        {
            go_JumpButton.GetComponentInChildren<Text>().text = "Jump";
        }
        if (ButtonState == "Fire")
        {
            go_JumpButton.GetComponentInChildren<Text>().text = "Fire";
        }
        if (ButtonState == "Interact")
        {
            go_JumpButton.GetComponentInChildren<Text>().text = "Interact";
        }
    }
    public void SetString(string NewState)
    {
        ButtonState = NewState;
    }
    public void SetTextBox(GameObject NewCurentTextBox)
    {
        CurentTextBox = NewCurentTextBox;
    }
    void CheckifOnRailing()
    {
        if (GetComponent<MU_RailingMechanics>() != null)
        {
            Bl_OnRailing = GetComponent<MU_RailingMechanics>().AttachedtoRailing;
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // if its on a railing
    //    if (collision.gameObject.GetComponent<MU_Movewithmagnet>()!=null)
    //    {
    //        print("Hit railing");
    //        Railing = collision.gameObject.GetComponent<MU_Movewithmagnet>();
    //    }
    //}
}
