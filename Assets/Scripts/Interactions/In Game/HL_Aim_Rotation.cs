using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Aim_Rotation : MonoBehaviour {
    public static HL_Aim_Rotation instance;

    public float fl_X_Value;
    public float fl_Y_Value;
    [SerializeField] LayerMask Layermask;

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
        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(fl_X_Value, fl_Y_Value,0));
    }
    // triggered in PC script
    public void Fire()
    {
        Particles.SetActive(true);
        // 
        if (RayHit().collider != null)
        {
            var TargetScript = RayHit().collider.GetComponent<MU_ObjectProperties>();
            if(TargetScript != null)
            {
                Invoke("DelayedMessageTransmision", 1.5f);
            }
        }

        Invoke("ResetAim", 1.5f);
    }
    void DelayedMessageTransmision()
    {

        var TargetScript = RayHit().collider.GetComponent<MU_ObjectProperties>();
        TargetScript.Bl_CanDecreaseTimer = true;
        // transmit the mesage to do the changes
        if (TargetScript.Bl_CanDecreaseTimer)
        {
            if (TargetScript.MyObjectType == MU_ObjectProperties.ObjectType.Magnet || TargetScript.MyObjectType == MU_ObjectProperties.ObjectType.FixedMagnet)
            {
                ReversePoles(RayHit().collider.gameObject);
            }
            if (TargetScript.MyObjectType == MU_ObjectProperties.ObjectType.Metal)
            {
                ConvertToMagnet(RayHit().collider.gameObject);
            }
            if (TargetScript.MyObjectType == MU_ObjectProperties.ObjectType.FixedMetal)
            {
                print("Yolo");
                ConvertToFixedMagnet(RayHit().collider.gameObject);
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
    void ConvertToFixedMagnet(GameObject vGO)
    {
        MU_ObjectProperties HitProperties = vGO.GetComponent<MU_ObjectProperties>();
        if (HitProperties != null)
        {
            HitProperties.MyObjectType = MU_ObjectProperties.ObjectType.FixedMagnet;
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

    //------------------------------
    void ResetAim()
    {
        Particles.SetActive(false);
        HL_Joystick.instance.SwichCanMove(true);
    }
    private RaycastHit2D RayHit()
    {
       
        Vector2 posiotion = new Vector3(transform.position.x, transform.position.y);
        return Physics2D.Raycast(posiotion, new Vector2(fl_X_Value,fl_Y_Value), 2, Layermask);

    }
public GameObject Aim()
    {
        return aim;
    }
}
