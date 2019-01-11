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
        if (collision.transform.tag != "Player" && collision.transform.tag != "Interactable" && collision.transform.tag != "Tutorial")
        {
            bl_Colliding = true;
            print(collision.transform.name);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Interactable" && collision.tag != "Tutorial")
        {
            bl_Colliding = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("E");
        bl_Colliding = false;
    }
}
