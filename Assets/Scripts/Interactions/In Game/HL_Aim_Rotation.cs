using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Aim_Rotation : MonoBehaviour {
    public static HL_Aim_Rotation instance;

    public float fl_X_Value;
    public float fl_Y_Value;

    protected GameObject aim;
    protected GameObject Particles;
    // Use this for initialization
    void Start () {
        instance = this;
        aim = gameObject.transform.Find("Aim").gameObject;
        Particles = gameObject.transform.Find("Particle holder").gameObject;
        Particles.SetActive(false);
        aim.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        Color color = Color.red;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(fl_X_Value, fl_Y_Value,0));
        
      //  Debug.DrawLine(transform.position, aim.transform.forward, color);

        if (RayHit().collider != null)
        {
            print(RayHit().collider.name);
        }
    }
    // triggered in PC script
    public void Fire()
    {
        Particles.SetActive(true);
        Invoke("ResetAim", 1.5f);
    }
    //------------------------------
    void ResetAim()
    {
        Particles.SetActive(false);
        HL_Joystick.instance.SwichCanMove(true);
    }
    private RaycastHit2D RayHit()
    {
        Vector2 posiotion = new Vector3(transform.position.x + 1, transform.position.y);
        return Physics2D.Raycast(posiotion, new Vector2(fl_X_Value,fl_Y_Value), 200000);

    }
public GameObject Aim()
    {
        return aim;
    }
}
