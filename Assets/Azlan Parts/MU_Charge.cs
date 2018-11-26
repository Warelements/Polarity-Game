using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Charge : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<HL_PC>()!=null)
        {
            Destroy(col.gameObject);
        }
    }
}
