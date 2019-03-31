using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_CollisionManager : MonoBehaviour
{
    public List<string> RH_HIttracker = new List<string>();
    public List<RaycastHit2D> RH_ConnectingRaycasts = new List<RaycastHit2D>();
    public Collider2D[] colliders = new Collider2D[1];
    public int numberofcolliders;
    public float collisionrange;

    public bool collideFromLeft;
    public bool collideFromTop;
    public bool collideFromRight;
    public bool collideFromBottom;

    public Rect Box;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + (Vector3)Box.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector2.zero, Box.size);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        colliders[0] = GetComponent<Collider2D>();
        RaycastCollisions();
        numberofcolliders = RH_ConnectingRaycasts.Count;
    }
    void RaycastCollisions()
    {
        RaycastHit2D hitleft = Physics2D.Raycast(new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y),Vector2.left, 0.1f);//shoot a ray from your left center edge
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y),Vector2.right, collisionrange);//shoot a ray from your Right center edge
        RaycastHit2D hitUp = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2),Vector2.up, collisionrange);//shoot a ray from your UP center edge
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2),Vector2.down, 0.1f);//shoot a ray from your Down center edge

        Debug.DrawLine(new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y), Vector2.left, Color.green);
        // Debug.DrawLine(transform.position.x,)
        RaycastHit2D upbox = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + 0.1765523f), new Vector2(2, 0.1f),0f, Vector2.up);
        if(upbox.collider!=null)
        {
            print(upbox.collider.name+" UP");
            
        }




            //{
            //RaycastHit2D HitLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.1865523f * 2, transform.position.y), Vector2.left, 0.5f);
            //RaycastHit2D HitRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.1765523f * 2, transform.position.y), Vector2.right, 0.5f);
            //RaycastHit2D HitTop = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.1765523f), Vector2.up, 1f);
            //RaycastHit2D HitDown = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.1765523f), Vector2.down, 0.5f);

        ADDandRemoveraycast(hitleft);
        ADDandRemoveraycast(hitRight);
        ADDandRemoveraycast(hitUp);
        ADDandRemoveraycast(hitDown);
       // Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 2, 0), Vector3.down*collisionrange, Color.black);

        //if (hitDown.collider != null)
        //{
        //    if (!RH_ConnectingRaycasts.Contains(hitDown))
        //    {
        //        RH_ConnectingRaycasts.Add(hitDown);
        //        print("G");
        //    }
        //    else
        //    {
        //        print("V");
        //        return;
        //    }
        //}
        //if (hitDown.collider == null)
        //{
        //    if (RH_ConnectingRaycasts.Contains(hitDown))
        //    {
        //        RH_ConnectingRaycasts.Remove(hitDown);
        //        print("E");
        //    }
        //}



        if (hitleft.collider != null)
        {
            if (!RH_HIttracker.Contains("Left"))
            {
                RH_HIttracker.Add("Left");
            }
        }
        if (hitleft.collider == null)
        {
            if (RH_HIttracker.Contains("Left"))
            {
                RH_HIttracker.Remove("Left");
            }
        }
        if (hitRight.collider != null)
        {
            if (!RH_HIttracker.Contains("Right"))
            {
                RH_HIttracker.Add("Right");
            }
        }
        if (hitRight.collider == null)
        {
            if (RH_HIttracker.Contains("Right"))
            {
                RH_HIttracker.Remove("Right");
            }
        }
        if (hitUp.collider != null)
        {
            if (!RH_HIttracker.Contains("UP"))
            {
                RH_HIttracker.Add("UP");
            }
        }
        if (hitUp.collider == null)
        {
            if (RH_HIttracker.Contains("UP"))
            {
                RH_HIttracker.Remove("UP");
            }
        }
        if (hitDown.collider != null)
        {
            if (!RH_HIttracker.Contains("Down"))
            {
                RH_HIttracker.Add("Down");
            }
        }
        if (hitDown.collider == null)
        {
            if (RH_HIttracker.Contains("Down"))
            {
                RH_HIttracker.Remove("Down");
            }
        }
    }
    void ADDandRemoveraycast(RaycastHit2D vHit)
    {
        if (vHit.collider != null)
        {
            if (!RH_ConnectingRaycasts.Contains(vHit))
            {
                RH_ConnectingRaycasts.Add(vHit);
                //print("G");
            }
        }
        if (vHit.collider == null)
        {
            if (RH_ConnectingRaycasts.Contains(vHit))
            {
                RH_ConnectingRaycasts.Remove(vHit);
                //print("E");
            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    ContactPoint2D[] contacts = collision.contacts;
    //    if(GetComponent<Collider2D>().GetContacts(contacts)!=0)
    //    {

    //    }
    //    foreach(ContactPoint2D C in contacts)
    //    {

    //    }
    //    //collision.collider.get
    //   // ContactPoint2D[] mycontacts = GetComponent<Collider2D>().GetContacts(colliders);
    //    //foreach(ContactPoint2D C in GetComponent<Collider2D>().GetContacts())
    //    //{
    //    //    print(C.collider.name+ C.normal);
    //    //    switch ((int)C.normal.x)
    //    //    {
    //    //        case -1: print(C.collider.name + "hit object from Left"); break;
    //    //        case 1: print(C.collider.name + "hit object from Right"); break;
    //    //    }
    //    //    switch ((int)C.normal.y)
    //    //    {
    //    //        case -1:print(C.collider.name+"hit object from Bottom")  ;break;
    //    //        case  1:print(C.collider.name+"hit object from Top"); break;
    //    //    }

    //    //}
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.otherCollider;
        int RectWidth = (int)GetComponent<Collider2D>().bounds.size.x;
        int RectHeight = (int)GetComponent<Collider2D>().bounds.size.y;

        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = collider.bounds.center;
        print(collider.name);
        if (collision.contacts[0].point.y < center.y && collision.contacts[0].point.x < center.x + GetComponent<Collider2D>().bounds.size.x / 2 && collision.contacts[0].point.x > center.x - GetComponent<Collider2D>().bounds.size.x / 2)
        {
            collideFromBottom = true;
            print("hitbelow");
        }
        if (collision.contacts[0].point.y > center.y && collision.contacts[0].point.x < center.x + GetComponent<Collider2D>().bounds.size.x / 2 && collision.contacts[0].point.x > center.x - GetComponent<Collider2D>().bounds.size.x / 2)
        {
            collideFromTop = true;
            print("hitAbove");
        }
        if (collision.contacts[0].point.x > center.x &&
           collision.contacts[0].point.y < center.y + GetComponent<Collider2D>().bounds.size.y / 2 && collision.contacts[0].point.y > center.y - GetComponent<Collider2D>().bounds.size.y / 2)
        {
            collideFromRight = true;
        }
        if (collision.contacts[0].point.x < center.x &&
         collision.contacts[0].point.y < center.y + GetComponent<Collider2D>().bounds.size.y / 2 && collision.contacts[0].point.y > center.y - GetComponent<Collider2D>().bounds.size.y / 2)
        {
            collideFromLeft = true;
        }
    }
}
