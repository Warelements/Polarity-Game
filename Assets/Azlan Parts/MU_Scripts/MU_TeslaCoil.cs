using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_TeslaCoil : MonoBehaviour
{
    public GameObject GO_RightPillar;
    public GameObject GO_LeftPillar;
    public GameObject GO_Charge;
    public GameObject GO_Target;
    [SerializeField]
    private float Fl_Speed=0.4f;
    private float Fl_MaximumDistance;
    // Use this for initialization
    void Start()
    {
        GO_Target = GO_RightPillar;
    }

    // Update is called once per frame
    void Update()
    {
        Reversedirection();
        MoveCharge();
    }
    void Reversedirection()
    {
        if (Vector2.Distance(GO_Charge.transform.position,GO_RightPillar.transform.position)<=0)
        {
            GO_Target = GO_LeftPillar;
        }
        if (Vector2.Distance(GO_Charge.transform.position, GO_LeftPillar.transform.position) <= 0)
        {
            GO_Target = GO_RightPillar;
        }
    }
    void MoveCharge()
    {
        GO_Charge.transform.position = Vector2.MoveTowards(GO_Charge.transform.position, GO_Target.transform.position, Fl_Speed * Time.deltaTime);
    }
}
