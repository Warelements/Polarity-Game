using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_LinktoInteractable : MonoBehaviour
{
    public bool Bl_Activated;
    public HL_Text_Trigger interactable;



    public void DrawLinetoTarget()
    {
        if (GetComponent<LineRenderer>() == null)
        {
            LineRenderer Ln_Renderer = gameObject.AddComponent<LineRenderer>();
            Ln_Renderer.SetPosition(0, transform.position);
            Ln_Renderer.SetPosition(1, interactable.transform.position);
            Ln_Renderer.startWidth = 0.01f;
            Ln_Renderer.endWidth = 0.01f;
        }
    }

}
