using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Electromagnet : MonoBehaviour
{
    [SerializeField]
    GameObject[] Go_Target;
    public bool Bl_ON;
    [SerializeField]
    private float Fl_Timer;
    [SerializeField]
    private float Fl_ResetTimerValue;
    [SerializeField]
    Animator An_Animator;
    private void Start()
    {
        //Fl_ResetTimerValue = Fl_Timer;
        foreach (GameObject go in Go_Target)
        {
            if (go != null)
            {
                go.GetComponent<MU_GeneratorVariables>().Generator = this;
            }
        }
        // An_Animator = Go_Bridge.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        TriggerONandOFF();
        ChangeTargetState();
        TriggerTarget();
    }

    void ChangeTargetState()
    {
        if (Bl_ON)
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
    void TriggerTarget()
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
    void TriggerONandOFF()
    {
        if (Bl_ON)
        {
            if (Fl_Timer < Fl_ResetTimerValue)
            {
                Fl_Timer += Time.deltaTime;
            }
            else
            {
                Fl_Timer = 0;
                Bl_ON = false;
            }
        }
    }
}
// sc1 generator is on
// sc2 if i recive that im on. stay on, otherwhise turn off
// sc 3 any other script will draw from sc2 . sc3 is also atach to same object as sc2