using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Aim_Rotation : MonoBehaviour {
    public static HL_Aim_Rotation instance;

    public float fl_X_Value;
    public float fl_Y_Value;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(fl_X_Value, fl_Y_Value,0));
    }
}
