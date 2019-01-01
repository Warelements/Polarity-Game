using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_AnimationEvents : MonoBehaviour
{
    Animator An_Animator;
    private void Start()
    {
        An_Animator = GetComponent<Animator>();
    }
    void switchtoidle()
    {
        if (An_Animator.GetInteger("CurrentState") == 2)
        {
            print("YOOlo");
            An_Animator.SetInteger("CurrentState", 0);
        }
    }
}
