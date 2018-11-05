using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Magnet : MonoBehaviour
{
    [SerializeField]
    private float Fl_MagneticRange;
    [SerializeField]
    private float Fl_RepulsionRange;
    private void Update()
    {
        
    }

    void Attract(GameObject vMagnetoAttract, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.position, vMagnetoAttract.transform.position) > Fl_MagneticRange)
        {
            vMagnetoAttract.transform.position = Vector3.MoveTowards(vMagnetoAttract.transform.position, transform.position, vFl_MovementSpeed * Time.deltaTime);
        }
    }
    void Repel(GameObject vGO, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.position, vGO.transform.position) < Fl_RepulsionRange && Vector3.Distance(transform.position, vGO.transform.position) > Fl_MagneticRange)
        {
            vGO.transform.position = Vector3.MoveTowards(vGO.transform.position, transform.position, -vFl_MovementSpeed * Time.deltaTime);
        }
    }
}
