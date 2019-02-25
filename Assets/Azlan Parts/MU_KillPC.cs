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
            Destroy(HL_Joystick.instance.Handle());
            gameObject.transform.Find("Rotation Center").gameObject.SetActive(false);
            print("Crushed");
            Invoke("TriggerGameOver", 1.5f);
        }
    }
    public void TheslaKill()
    {
        Destroy(HL_Joystick.instance.Handle());
        gameObject.transform.Find("Rotation Center").gameObject.SetActive(false);
        print("Zapped Kill");
        Invoke("TriggerGameOver", 1.5f);
    }
    void TriggerGameOver()
    {
        GameObject.Find("Level Canvas").GetComponent<HL_In_Game_UI>().Game_Over();
    }
}
