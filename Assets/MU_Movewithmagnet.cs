using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Movewithmagnet : MonoBehaviour
{

    public Vector2 MovementDirection;
    public float Speed;
    MU_RailingMechanics RM;
   public HL_ObjectProperties props;
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
        //if (Go.GetComponent<HL_ObjectProperties>()!=null)
        //{
        //    props = Go.GetComponent<HL_ObjectProperties>();
        //    props.enabled = false;
        //    //if (Go.GetComponent<MU_RailingMechanics>() == null)
        //    //{
        //    //    MU_RailingMechanics RMa = Go.AddComponent<MU_RailingMechanics>();
        //    //    RMa.AttachedtoRailing = true;
        //    //    RMa.railingspeed = Speed;
        //    //    RMa.RailingMovementdirection = MovementDirection;
        //    //}
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        //if (collision.gameObject.GetComponent<HL_ObjectProperties>()!=null)
        //{
        //    collision.gameObject.GetComponent<HL_ObjectProperties>().Interactable = false;
        //    collision.gameObject.transform.parent = this.gameObject.transform.parent.transform;
        //    Go.transform.Translate(MovementDirection * Speed);
        //}
        if (Go.tag=="Player")
        {
          RM.AttachedtoRailing = true;
          Go.transform.Translate(MovementDirection * Speed*Time.deltaTime);
        }
        else
        {
            Go.transform.Translate(MovementDirection * Speed * Time.deltaTime);
            //MU_RailingMechanics rma = Go.GetComponent<MU_RailingMechanics>();
            //if(rma!=null)
            //{
            //    Go.transform.Translate()
            //}
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject Go = collision.gameObject;
        //if (collision.gameObject.GetComponent<HL_ObjectProperties>() != null)
        //{
        //    collision.gameObject.GetComponent<HL_ObjectProperties>().Interactable = true;
        //    collision.gameObject.transform.parent = null;       
        //}
        if(props!=null)
        {
            props.enabled = true;
            props.ActivatePoles();
           
        }
        if (Go.tag == "Player")
        {
            RM.AttachedtoRailing = false;
            collision.gameObject.transform.parent = null;
           
            Destroy(RM);
        }
    }
}
