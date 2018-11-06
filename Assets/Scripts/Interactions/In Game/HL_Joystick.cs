using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HL_Joystick : MonoBehaviour
{
    public static HL_Joystick instance;
    public Vector3 m_StartPos;
    [SerializeField]
    private GameObject go_Handle;
    protected bool bl_Aiming;
    protected bool bl_Created;
    protected bool bl_CanMove;
    [SerializeField] protected GameObject go_MovemebtButtons;
    protected GameObject go_HandleInstance;
    protected bool bl_InTrigger;

    private void Start()
    {
        instance = this;
        bl_CanMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (bl_CanMove == true && CharacterController2D.instance.bl_Grounded()== true && bl_InTrigger== false)
        {
            // detect if not on top of UI Element
            bool noUIcontrolsInUse = EventSystem.current.currentSelectedGameObject == null;
            if (noUIcontrolsInUse && HL_In_Game_UI.Paused == false)
            {
                //  on input create handle on input location and child it to canvas(this scripts game object)
                if (Input.GetMouseButtonDown(0) && bl_Created == false)
                {
                    HL_Aim_Rotation.instance.Aim().SetActive(true);
                    go_MovemebtButtons.SetActive(false);
                    HL_PC.instance.SetString("Fire");

                    bl_Aiming = true;
                    m_StartPos = Input.mousePosition;

                    GameObject handle = Instantiate(go_Handle, m_StartPos, go_Handle.transform.rotation);
                    go_HandleInstance = handle;
                    handle.transform.SetParent(gameObject.transform);
                    handle.GetComponent<HL_Joystick_Handle>().m_StartPos = Input.mousePosition;

                    bl_Created = true;
                }
                // on remove of input destoi handle creaated
                if (Input.GetMouseButtonUp(0) && noUIcontrolsInUse)
                {
                    HL_Aim_Rotation.instance.Aim().SetActive(false);


                    if (GameObject.Find("Handle (Clone)") != null)
                    {
                        Destroy(gameObject.transform.Find("Handle (Clone)").gameObject);
                    }
                    go_MovemebtButtons.SetActive(true);
                    HL_PC.instance.SetString("Jump");
                    bl_Aiming = false;
                    bl_Created = false;
                }
            }
        }
    }
    public void SwichCanMove(bool NewCanMove)
    {
        bl_CanMove = NewCanMove;
    }
    public bool CanMove()
    {
        return bl_CanMove;
    }
    public bool Bl_Amingn()
    {
        return bl_Aiming;
    }
    public GameObject Handle()
    {
        return go_HandleInstance;
    }
    public GameObject MovementButtons()
    {
        return go_MovemebtButtons;
    }
    public void SetAiming(bool NewAiming)
    {
        bl_Aiming = NewAiming;
    }
    public void SetCreate(bool NewCreate)
    {
        bl_Created = NewCreate;
    }
    public void SwichInTrigger(bool NewInTrigger)
    {
        bl_InTrigger = NewInTrigger;
    }

}
