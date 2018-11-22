using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_TestCircle : MonoBehaviour
{

    //unused test script for circle
    [SerializeField]
    private Material LineMat;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            ShowRange();
        }
    }
    void ShowRange()
    {
        if (GetComponent<MU_Circle>() == null)
        {
            MU_Circle Circle = gameObject.AddComponent<MU_Circle>();
            Circle.lineRenderer.material = LineMat;
        }
        else//make seperate method for raycasts
        {
            Destroy(GetComponent<MU_Circle>());
            Destroy(GetComponent<LineRenderer>());
        }
        //use prviously hit object and new hit object to enable disable circle
    }
}
