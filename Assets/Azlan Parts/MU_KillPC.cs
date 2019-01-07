using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_KillPC : MonoBehaviour
{
    public Collider2D upcol;
    public Collider2D Downcol;
    public Collider2D Rightcol;
    public Collider2D Leftcol;
    [SerializeField]
    private bool Bl_Crushed;
    private float Fl_DeathAnimTime=1;

    // Update is called once per frame
    void Update()
    {
        CheckForCrush();
        //Crush();
    }
    void Crush()
    {
        if(Bl_Crushed)
        {
            Destroy(gameObject, Fl_DeathAnimTime);
        }
    }
    void CheckForCrush()
    {
     if((Gettrigger(upcol).bl_Colliding==true&&Gettrigger(Downcol)==true)|| (Gettrigger(Rightcol).bl_Colliding == true && Gettrigger(Leftcol) == true))
        {
            Bl_Crushed = true;
        }
     else
        {
            Bl_Crushed = false;
        }
    }
    MU_Trigger Gettrigger(Collider2D col)
    {
        return (col.GetComponent<MU_Trigger>());
    }
    public bool Crushed()
    {
        {
            return Bl_Crushed;
        }
    }
}
