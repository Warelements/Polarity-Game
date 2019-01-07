using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Trigger : MonoBehaviour
{
    public bool bl_Colliding;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bl_Colliding = true;
        //print("A");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        bl_Colliding = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("E");
        bl_Colliding = false;
    }
}
