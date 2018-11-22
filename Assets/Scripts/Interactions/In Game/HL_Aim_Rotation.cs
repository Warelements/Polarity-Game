using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Aim_Rotation : MonoBehaviour
{
    public static HL_Aim_Rotation instance;

    public float fl_X_Value;
    public float fl_Y_Value;
    [SerializeField] LayerMask Layermask;

    protected GameObject aim;
    protected GameObject Particles;


    #region Azlans Stuff added for range display
    [SerializeField] private GameObject GO_PreviousHitTarget;
    [SerializeField] private GameObject GO_CurrentHitTarget;
    [SerializeField] private Material LineMat;
    #endregion
    // Use this for initialization
    void Start()
    {
        instance = this;
        aim = gameObject.transform.Find("Aim").gameObject;
        Particles = gameObject.transform.Find("Particle holder").gameObject;
        Particles.SetActive(false);
        aim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(fl_X_Value, fl_Y_Value, 0));
        DisplayRange();
    }
    // triggered in PC script
    public void Fire()
    {
        Particles.SetActive(true);
        // 
        if (RayHit().collider != null)
        {
            var TargetScript = RayHit().collider.GetComponent<MU_ObjectProperties>();
            if (TargetScript != null)
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
        return Physics2D.Raycast(posiotion, new Vector2(fl_X_Value, fl_Y_Value), 2, Layermask);
    }
    //gets raycast return and displays the range as a circle for the hit Interactable object
    void DisplayRange()
    {
        if (RayHit().collider != null)//if youve ht something
        {
            if (RayHit().collider.GetComponent<MU_ObjectProperties>() != null)// only works on interactable objects i.e magnets,metals,fixed metals andfixed magnets
            {
                if (GO_CurrentHitTarget==null)//if current target is null, assign the hit object as the current target
                {
                    GO_CurrentHitTarget = RayHit().collider.gameObject;
                }
                if(GO_CurrentHitTarget!=null)//if the current target is not null
                {
                    if(RayHit().collider.gameObject!=GO_CurrentHitTarget)//if what ive hit is not the previous current target
                    {
                        DeactivateCircle(GO_CurrentHitTarget);//deactive the current targets circle
                        GO_CurrentHitTarget = null;//set the current target to null
                    }
                }
                if (GO_CurrentHitTarget.GetComponent<MU_Circle>() == null)//if the current target circle is deactivated
                {
                    ActivateCircle(GO_CurrentHitTarget);//activate it
                }
            }
        }
        else// if youre not hitting anything
        {
            MU_ObjectProperties[] InteractableObjects = GameObject.FindObjectsOfType<MU_ObjectProperties>();// rfind all interactable objects
            foreach(MU_ObjectProperties obj in InteractableObjects)
            {
                if(obj.gameObject.GetComponent<MU_Circle>()!=null)//if the circle is active on any of them
                {
                    DeactivateCircle(obj.gameObject);//deactivate the circle on said object.
                }
            }
                DeactivateCircle(GO_PreviousHitTarget);
                DeactivateCircle(GO_CurrentHitTarget);
                DeactivateCircle(RayHit().collider.gameObject);
                GO_CurrentHitTarget = null;
                GO_PreviousHitTarget = null;     
        }
    }
    void ActivateCircle(GameObject vGO)//sets circle to active
    {
        MU_Circle Circle = vGO.AddComponent<MU_Circle>();//adds circle script
        Circle.lineRenderer.material = LineMat;// adds material to circle
    }
    void DeactivateCircle(GameObject vGO)//deactivates circle
    {
        Destroy(vGO.GetComponent<MU_Circle>());//removes circle component
        Destroy(vGO.GetComponent<LineRenderer>());//removes line component
    }
    public GameObject Aim()
    {
        return aim;
    }
}
