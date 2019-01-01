using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MU_GeneratorVariables))]
public class MU_Bridge : MonoBehaviour
{
    MU_GeneratorVariables sc_Generator;
    // Use this for initialization
    void Start()
    {
        sc_Generator = GetComponent<MU_GeneratorVariables>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sc_Generator.bl_Generator_On)
        {
            Active();
        }
        else if (sc_Generator.bl_Generator_On == false)
        {
            Inactive();
        }

    }
    void Active()
    {
        print("acrive");
        
    }
    void Inactive()
    {
        print("Inactive");

    }
}
