using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Raycast_Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {

       if (collision.gameObject != gameObject.transform.root.gameObject)
        {
           // print(collision.gameObject.name + "  i collided with that" );

            transform.root.gameObject.transform.GetComponent<HL_ObjectProperties>().str_recivedColision = gameObject.name.ToString();
        }
    }
}
