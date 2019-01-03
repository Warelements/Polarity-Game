using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Animations : MonoBehaviour
{
    public Animation[] AnimStates;
    public int In_CurrentAnimationState;
    [SerializeField]
    private float fl_speed = 0;
    [SerializeField]
    private bool Jumping;
    [SerializeField]
    private GameObject Go_PCMesh;

    [SerializeField]
    private Animator Anim_controller;
    private void Start()
    {
        Anim_controller = Go_PCMesh.GetComponent<Animator>();
    }
    void Update()
    {
        Jumping = GetComponent<HL_PC>().jump;
        ChangeCurrentAnimState();
        ChangeAnimations();
    }
    void ChangeAnimations()
    {
        switch (In_CurrentAnimationState)
        {
            case 0: Anim_controller.SetInteger("CurrentAnim", 0); break;
            case 1: Anim_controller.SetInteger("CurrentAnim", 1); break;
            case 2: Anim_controller.SetInteger("CurrentAnim", 2); break;
            case 3: Anim_controller.SetInteger("CurrentAnim", 3); break;
        }
    }
    void ChangeCurrentAnimState()
    {

        if (fl_speed > 0 && !Jumping)
        {
            In_CurrentAnimationState = 1;
        }
        if (Jumping)
        {
            In_CurrentAnimationState = 2;
        }
        if (fl_speed == 0 && !Jumping)
        {
            In_CurrentAnimationState = 0;
        }
    }
}
