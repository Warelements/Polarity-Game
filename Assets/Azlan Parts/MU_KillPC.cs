using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MU_KillPC : MonoBehaviour
{
    public Collider2D upcol;
    public Collider2D Downcol;
    public Collider2D Rightcol;
    public Collider2D Leftcol;
    [SerializeField]
    private bool Bl_Crushed;
    private float Fl_DeathAnimTime=1;
    public Collider2D[] Cl_Colliders;
    public HL_Aim_Rotation aim;
    int[] numbers=new int[10];

    //int[] array1; 
    //int[] array2 = new int[] { 1, 2, 3 };
    private void Start()
    {
        Cl_Colliders = new Collider2D[] { upcol, Downcol, Leftcol, Rightcol };
    }
    // Update is called once per frame
    void Update()
    {
        Kill();
        if (Input.GetKeyDown(KeyCode.A))
        {
            aim.Fire();  
        }
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
        if(Bl_Crushed == true)
        {
            Kill();
        }
        // array1 = new int[] { 1, 2, 3 };
        //for (int i = 0; i < Cl_Colliders.Length; i++)
        //{
        //    //    if(Gettrigger(Cl_Colliders[i]).bl_Colliding==true&&Gettrigger(Cl_Colliders[i]).Cl_AlternateCollider).bl_Colliding==true)
        //    //{

        //    //}
        //    if (Gettrigger(Cl_Colliders[i]).bl_Colliding==true && Gettrigger(Gettrigger(Cl_Colliders[i]).Cl_AlternateCollider).bl_Colliding==true)
        //    {
        //        Bl_Crushed = true;
        //        Kill();
        //        break;
        //    }
        //}      
        //if (Gettrigger(upcol).bl_Colliding==true&&Gettrigger(Downcol)==true)
        //{
        //        Bl_Crushed = true; 
        //}
        //if (Gettrigger(Rightcol).bl_Colliding == true && Gettrigger(Leftcol) == true)
        //{
        //        Bl_Crushed = true;           
        //}
        //else
        //{
        //    Bl_Crushed = false;
        //}
    }
    MU_Trigger Gettrigger(Collider2D col)
    {
        return (col.GetComponent<MU_Trigger>());
    }
    public bool Crushed()
    {
        
            return Bl_Crushed;
        
    }
    public void ChangeCrushed(bool NewCrushed)
    {
        Bl_Crushed = NewCrushed;
    }
   public void Kill()
    {
        if(Bl_Crushed)
        {
            print("Crushed");
            Invoke("TriggerGameOver", 2);
        }
    }
    void TriggerGameOver()
    {
        GameObject.Find("Level Canvas").GetComponent<HL_In_Game_UI>().Game_Over();
    }
}
