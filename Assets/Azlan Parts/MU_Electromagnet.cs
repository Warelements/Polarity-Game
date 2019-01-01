using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Electromagnet : MonoBehaviour
{
    [SerializeField]
    GameObject [] Go_Target;
    public bool Bl_ON;
    [SerializeField]
    Animator An_Animator;
    private void Start()
    {
       // An_Animator = Go_Bridge.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        ChangeTargetState();
        TriggerBridge();
    }
    void ChangeTargetState()
    {
        if(Bl_ON)
        {
          //  vGo_Target.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
           // vGo_Target.SetActive(false);
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    void TriggerBridge()
    {

        foreach (GameObject go in Go_Target)
        {
            if (go != null)
            {
                if (Bl_ON)
                {
                    go.GetComponent<MU_GeneratorVariables>().bl_Generator_On = true;
                }
                if (!Bl_ON)
                {
                    go.GetComponent<MU_GeneratorVariables>().bl_Generator_On = false;
                }
            }
        }
    }
}
// sc1 generator is on
// sc2 if i recive that im on. stay on, otherwhise turn off
// sc 3 any other script will draw from sc2 . sc3 is also atach to same object as sc2