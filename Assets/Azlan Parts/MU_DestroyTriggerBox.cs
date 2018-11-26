using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_DestroyTriggerBox : MonoBehaviour
{
    [SerializeField]
    private GameObject GO_Target;
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.GetComponent<HL_PC>()!=null)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<HL_PC>() != null)
        {
            Destroy(GO_Target);
        }
    }
}
