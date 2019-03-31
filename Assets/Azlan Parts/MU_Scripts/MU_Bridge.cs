using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MU_GeneratorVariables))]
public class MU_Bridge : MonoBehaviour
{
    MU_GeneratorVariables sc_Generator;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        sc_Generator = GetComponent<MU_GeneratorVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sc_Generator.bl_Generator_On)
        {
            Active();
        }
       if (sc_Generator.bl_Generator_On == false)
        {
            Inactive();
        }
    }
    void Active()
    {
        print("acrive");
       animator.SetInteger("CurrentState", 1);
    }
    void Inactive()
    {
        print("Inactive");
        if (animator.GetInteger("CurrentState") == 1)
        {
            animator.SetInteger("CurrentState", 2);
        }
    }
}
