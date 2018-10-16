using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Moving_Object : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(gameObject.transform.position, 1f * Time.deltaTime);
	}
}
