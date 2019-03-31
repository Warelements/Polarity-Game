using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Shoot(gameObject);
        }
    }
    //test out for shooting later, should work
    void Shoot(GameObject Vgo_Shooter)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(Vgo_Shooter.transform.position, Vgo_Shooter.transform.right, 5f/* range for shooting with ray*/);
        // if you hit something
        if (hit2D.collider != null)
        {
            MU_ObjectProperties HitProperties = hit2D.collider.gameObject.GetComponent<MU_ObjectProperties>();
            if (HitProperties.MyObjectType == MU_ObjectProperties.ObjectType.Magnet)
            {
                print(hit2D.collider.transform.root + "afhafsaf");
                ReversePoles(hit2D.collider.gameObject);
            }
            if (HitProperties.MyObjectType == MU_ObjectProperties.ObjectType.Metal)
            {
                ConvertToMagnet(hit2D.collider.gameObject);
                //hit2D = Physics2D.Raycast(Vgo_Shooter.transform.position, Vgo_Shooter.transform.right, 5f);
            }
        }
    }
    void ConvertToMagnet(GameObject vGO)
    {
        MU_ObjectProperties HitProperties = vGO.GetComponent<MU_ObjectProperties>();
        if (HitProperties != null)
        {
            HitProperties.MyObjectType = MU_ObjectProperties.ObjectType.Magnet;
        }
    }
    void ReversePoles(GameObject vGO)
    {
        GameObject[] children = new GameObject[vGO.transform.childCount];
        //add children to the array
        for (int i = 0; i < vGO.transform.childCount; i++)
        {
            children[i] = vGO.transform.GetChild(i).gameObject;
        }
        foreach (GameObject go in children)
        {
            // if it has a pole script attached
            if (go.GetComponent<MU_Poles>() != null)
            {
                if (go.GetComponent<MU_Poles>().Poletype != MU_Poles.PoleType.Northpole)
                {
                    go.GetComponent<MU_Poles>().Poletype = MU_Poles.PoleType.Northpole;
                }
               else /*if (go.GetComponent<MU_Poles>().Poletype == MU_Poles.PoleType.SouthPole)*/
                {
                    go.GetComponent<MU_Poles>().Poletype = MU_Poles.PoleType.SouthPole;
                }
            }
        }
    }
}
