using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HL_Joystick : MonoBehaviour
{
    public Vector3 m_StartPos;
    [SerializeField]
    private GameObject go_Handle;


    // Update is called once per frame
    void Update()
    {
        // detect if not on top of UI Element
        bool noUIcontrolsInUse = EventSystem.current.currentSelectedGameObject == null;
        if (noUIcontrolsInUse && HL_In_Game_UI.Paused == false)
        {
            //  on input create handle on input location and child it to canvas(this scripts game object)
            if (Input.GetMouseButtonDown(0))
            {
                m_StartPos = Input.mousePosition;
                GameObject handle = Instantiate(go_Handle, m_StartPos, go_Handle.transform.rotation);
                handle.transform.SetParent(gameObject.transform);
                handle.GetComponent<HL_Joystick_Handle>().m_StartPos = Input.mousePosition;
            }
            // on remove of input destoi handle creaated
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(gameObject.transform.Find("Handle (Clone)").gameObject);

            }
        }
    }
}
