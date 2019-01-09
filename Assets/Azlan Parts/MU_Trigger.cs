using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Trigger : MonoBehaviour
{
    public bool bl_Colliding;
    public Collider2D Cl_AlternateCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(gameObject.name);
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
