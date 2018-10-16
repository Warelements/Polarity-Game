using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Joystick_Handle : MonoBehaviour
{
    public Vector3 m_StartPos;
    public float movementRange;
    bool m_UseX;
    bool m_UseY;

    public float fl_X_Axis;
    public float fl_Y_Axis;
    // Use this for initialization, 
    void Start()
    {
        m_UseX = true;
        m_UseY = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = Vector3.zero;
        //
        if (m_UseX)
        {
            int delta = (int)(Input.mousePosition.x - m_StartPos.x);
            newPos.x = delta;
        }
        if (m_UseY)
        {
            int delta = (int)(Input.mousePosition.y - m_StartPos.y);
            newPos.y = delta;
        }
        transform.position = Vector3.ClampMagnitude(new Vector3(newPos.x, newPos.y, newPos.z), movementRange) + m_StartPos;
        UpdateVirtualAxes(transform.position);
    }

    // this transmits the data to the aim ray/tool w use with the coordinates of the jystick
    void UpdateVirtualAxes(Vector3 value)
    {
        var delta = m_StartPos - value;
        delta.y = -delta.y;
        delta /= movementRange;
        if (m_UseX)
        {
            fl_X_Axis = -delta.x;
            HL_Aim_Rotation.instance.fl_X_Value = fl_X_Axis;
        }

        if (m_UseY)
        {
            fl_Y_Axis = delta.y;
            HL_Aim_Rotation.instance.fl_Y_Value = fl_Y_Axis;
        }
    }
}