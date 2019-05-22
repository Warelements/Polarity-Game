using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Movewithmagnet : MonoBehaviour
{

    public Vector2 MovementDirection;
    public float Speed;
    MU_RailingMechanics RM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        if (Go.tag == "Player")
        {

            if (Go.GetComponent<MU_RailingMechanics>()==null)
            {
                RM = Go.AddComponent<MU_RailingMechanics>();
                RM.AttachedtoRailing = true;
                RM.railingspeed = Speed;
                RM.RailingMovementdirection = MovementDirection;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        if (collision.gameObject.tag == "cube")
        {        
            collision.gameObject.transform.parent = this.gameObject.transform.parent.transform;
            Go.transform.Translate(MovementDirection * Speed);
        }
        if (Go.tag=="Player")
        {
          RM.AttachedtoRailing = true;
          Go.transform.Translate(MovementDirection * Speed*Time.deltaTime);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        if (collision.gameObject.tag == "cube")
        {
            collision.gameObject.transform.parent = null;       
        }
        if (Go.tag == "Player")
        {
            RM.AttachedtoRailing = false;
            collision.gameObject.transform.parent = null;
           
            Destroy(RM);
        }
    }
}
