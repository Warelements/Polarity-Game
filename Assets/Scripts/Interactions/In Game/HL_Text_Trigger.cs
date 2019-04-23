using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Text_Trigger : MonoBehaviour
{

    protected GameObject MyText;
    protected GameObject PC;
    public bool Bl_Enabled;
    public bool Bl_Connectedtoobject;
    public GameObject[] Connectedobjects;
    // Use this for initialization
    void Start()
    {
        MyText = gameObject.transform.Find("Text Holder").gameObject;
        MyText.SetActive(false);
        Bl_Enabled = false;
        PC = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CheckConnection();
    }
    //checks if i am connected to an object
    void CheckConnection()
    {
        if (Connectedobjects.Length>0)
        {
            Bl_Connectedtoobject = true;
        }
        else
        {
            Bl_Connectedtoobject = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //tutorial
        if (collision.tag == "Player" && gameObject.tag != "Interactable")
        {
            HL_Joystick.instance.SwichInTrigger(true);
            MyText.SetActive(true);
            Bl_Enabled = true;
        }
        //interactable
        if (collision.tag == "Player" && gameObject.tag == "Interactable")
        {
            //--------------------azlan
            //if (!Bl_Connectedtoobject)
            //{
                print("i have collided");
                Bl_Enabled = true;
                HL_Joystick.instance.SwichInTrigger(true);
                HL_PC.instance.SetString("Interact");
                HL_PC.instance.SetTextBox(gameObject);
           // }
            //else if (Bl_Connectedtoobject)
            //{
            //    foreach (GameObject go in Connectedobjects)
            //    {
            //        go.GetComponent<MU_LinktoInteractable>().Bl_Activated = true;
            //    }
            //}
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (!Bl_Connectedtoobject)
            //{
                HL_Joystick.instance.SwichInTrigger(false);
                Bl_Enabled = false;
                MyText.SetActive(false);

                HL_PC.instance.SetTextBox(null);
                HL_PC.instance.SetString("Jump"); 
           // }
            //else if (Bl_Connectedtoobject)
            //{
            //    foreach (GameObject go in Connectedobjects)
            //    {
            //        go.GetComponent<MU_LinktoInteractable>().Bl_Activated = false;
            //    }
            //}
        }
    }
    public void TriggerInteractableBox()
    {

        if (!Bl_Connectedtoobject)
        {
            MyText.SetActive(true);
            Bl_Enabled = true; 
        }
        else if (Bl_Connectedtoobject)
        {
            foreach (GameObject go in Connectedobjects)
            {
                go.GetComponent<MU_LinktoInteractable>().Bl_Activated = !go.GetComponent<MU_LinktoInteractable>().Bl_Activated;
            }
        }
    }
}
