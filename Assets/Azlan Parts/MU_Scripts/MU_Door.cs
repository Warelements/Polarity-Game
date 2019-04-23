using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Door : MonoBehaviour {

    private MU_LinktoInteractable LS_LinkedScript;
    private void Start()
    {
        LS_LinkedScript = GetComponent<MU_LinktoInteractable>();
    }
    private void Update()
    {
        OpenCloseDoor();
    }
    void OpenCloseDoor()
    {
        if (LS_LinkedScript.Bl_Activated)
        {
            Open();
        }
        else if (!LS_LinkedScript.Bl_Activated)
        {
            Close();
        }
    }
    void Open()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void Close()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
