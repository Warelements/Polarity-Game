using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HL_Drag_TestSCript : MonoBehaviour {

    public float OffsetX;
    Vector2 Star_Pos;

    public float BaseSpeed; 
    // Use this for initialization
	void Start () {
        Star_Pos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

        
        if (transform.localPosition.x > Star_Pos.x)
        {
            transform.localPosition = Star_Pos;
        }
        if (transform.localPosition.x < -655)
        {
            transform.localPosition = Star_Pos - new Vector2 (655,0);
        }
    }
    public void StartDrag()
    {
        OffsetX = transform.position.x - Input.mousePosition.x;

    }
    public void OnDrag()
    {
          transform.position = new Vector3(OffsetX + Input.mousePosition.x, transform.position.y);
    }
}
