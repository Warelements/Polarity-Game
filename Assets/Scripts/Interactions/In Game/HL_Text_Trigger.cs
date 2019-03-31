using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Text_Trigger : MonoBehaviour {

    protected GameObject MyText;
    protected GameObject PC;
    public bool Bl_Enabled;
	// Use this for initialization
	void Start () {
        MyText = gameObject.transform.Find("Text Holder").gameObject;
        MyText.SetActive(false);
        Bl_Enabled = false;
        PC = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && gameObject.tag != "Interactable")
        {
            HL_Joystick.instance.SwichInTrigger(true);
            MyText.SetActive(true);
            Bl_Enabled = true;

        }
        if (collision.tag == "Player" && gameObject.tag == "Interactable")
        {
            print("i have collided");
            Bl_Enabled = true;
            HL_Joystick.instance.SwichInTrigger(true);
            HL_PC.instance.SetString("Interact");
            HL_PC.instance.SetTextBox(gameObject);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            HL_Joystick.instance.SwichInTrigger(false);
            Bl_Enabled = false;
            MyText.SetActive(false);
            
            HL_PC.instance.SetTextBox(null);
            HL_PC.instance.SetString("Jump");
        }
    }
    public void TriggerInteractableBox()
    {
        MyText.SetActive(true);
        Bl_Enabled = true;
    }
}
