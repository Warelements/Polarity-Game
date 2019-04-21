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
    [Header("Range display properties")]
    [SerializeField] private GameObject GO_PreviousHitTarget;
    [SerializeField] private GameObject GO_CurrentHitTarget;
    [SerializeField] private Material LineMat;
    [SerializeField] private float Fl_Range;
    #endregion
    [SerializeField] private LineRenderer Ln_render;
    [SerializeField] private GameObject go_DirectionAim;
    // Use this for initialization
    void Start()
    {
        instance = this;
        aim = gameObject.transform.Find("Aim").gameObject;
        Particles = gameObject.transform.Find("Particle holder").gameObject;
        Particles.SetActive(false);
        aim.SetActive(false);
        ResetAimLines();
    }

    // Update is called once per frame
    void Update()
    {
        //rotates the knobe holding the aim
        transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(fl_X_Value, fl_Y_Value, 0));

        //----
        if (HL_Joystick.instance.SharedCreated())
        {
            DrawAimlines();
        }

        DisplayRange();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Fire();
        }
    }
    // triggered in PC script

    public void DrawAimlines()
    {
        //extra linerenderer parts from azlan
        if (Ln_render != null)
        {
            if (RayHit().collider != null)
            {
                Ln_render.SetPosition(0, transform.position);
                Ln_render.SetPosition(1, RayHit().point);
                //   Ln_render.material.SetTextureScale("dotted line", new Vector2((Ln_render.GetPosition(1).x - Ln_render.GetPosition(0).x)/1, (Ln_render.GetPosition(1).y - Ln_render.GetPosition(0).y)/1));
                float distance = Vector2.Distance(gameObject.transform.position, RayHit().point);
                Ln_render.material.mainTextureScale = new Vector2(distance * 10, 1);
            }
            else
            {
                print("no colider");
                Ln_render.SetPosition(0, transform.position);
                Ln_render.SetPosition(1, go_DirectionAim.transform.position);
                //   Ln_render.material.SetTextureScale("dotted line", new Vector2((Ln_render.GetPosition(1).x - Ln_render.GetPosition(0).x) / 1, (Ln_render.GetPosition(1).y - Ln_render.GetPosition(0).y) / 1));
            }

        }
    }
    public void ResetAimLines()
    {
        if (Ln_render != null)
        {
            Ln_render.SetPosition(0, transform.position);
            Ln_render.SetPosition(1, transform.position);
        }
    }
    public void Fire()
    {
        Particles.SetActive(true);
        // 
        if (RayHit().collider != null)
        {
            var TargetScript = RayHit().collider.GetComponent<HL_ObjectProperties>();
            if (TargetScript != null)
            {
                Invoke("DelayedMessageTransmision", 1.5f);
            }
            if (RayHit().collider.GetComponent<MU_Electromagnet>() != null)
            {
                MU_Electromagnet vGO_Generator = RayHit().collider.GetComponent<MU_Electromagnet>();
                
                if (!vGO_Generator.Bl_ON)
                {
                    vGO_Generator.Bl_ON = true;        
                }
               else if (vGO_Generator.Bl_ON)
                {
                    vGO_Generator.Bl_ON = false;
                }
            }
        }
        Invoke("ResetAim", 1.5f);
    }
    void DelayedMessageTransmision()
    {
        var TargetScript = RayHit().collider.GetComponent<HL_ObjectProperties>();

        //TargetScript.Bl_CanDecreaseTimer = true;

        // transmit the mesage to do the changes
        if (TargetScript != null)
        {
            if (TargetScript.MyObjectType == HL_ObjectProperties.ObjectType.Magnet || TargetScript.MyObjectType == HL_ObjectProperties.ObjectType.FixedMagnet)
            {
                ReversePoles(RayHit().collider.gameObject);
            }
            if (TargetScript.MyObjectType == HL_ObjectProperties.ObjectType.Metal)
            {

                if (!TargetScript.unchangeable)
                {
                    ConvertToMagnet(RayHit().collider.gameObject);
                    TargetScript.Bl_CanDecreaseTimer = true; 
                }
            }
            if (TargetScript.MyObjectType == HL_ObjectProperties.ObjectType.FixedMetal)
            {
                TargetScript.Bl_CanDecreaseTimer = true;
                ConvertToFixedMagnet(RayHit().collider.gameObject);
            }
           
        }
    }
    void ConvertToMagnet(GameObject vGO)
    {
        HL_ObjectProperties HitProperties = vGO.GetComponent<HL_ObjectProperties>();
        if (HitProperties != null)
        {
            HitProperties.MyObjectType = HL_ObjectProperties.ObjectType.Magnet;
        }
    }
    void ConvertToFixedMagnet(GameObject vGO)
    {
        HL_ObjectProperties HitProperties = vGO.GetComponent<HL_ObjectProperties>();
        if (HitProperties != null)
        {
            HitProperties.MyObjectType = HL_ObjectProperties.ObjectType.FixedMagnet;
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
            if (go.GetComponent<HL_Poles>() != null)
            {
                if (go.GetComponent<HL_Poles>().Poletype != HL_Poles.PoleType.Northpole)
                {
                    go.GetComponent<HL_Poles>().Poletype = HL_Poles.PoleType.Northpole;
                }
                else /*if (go.GetComponent<MU_Poles>().Poletype == MU_Poles.PoleType.SouthPole)*/
                {
                    go.GetComponent<HL_Poles>().Poletype = HL_Poles.PoleType.SouthPole;
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
        if (RayHit().collider != null && HL_Joystick.instance.Bl_Amingn() == true)//if youve ht something
        {
            if (RayHit().collider.GetComponent<HL_ObjectProperties>() != null)// only works on interactable objects i.e magnets,metals,fixed metals andfixed magnets
            {
                if (GO_CurrentHitTarget == null)//if current target is null, assign the hit object as the current target
                {
                    GO_CurrentHitTarget = RayHit().collider.gameObject;
                }
                if (GO_CurrentHitTarget != null)//if the current target is not null
                {
                    if (RayHit().collider.gameObject != GO_CurrentHitTarget)//if what ive hit is not the previous current target
                    {
                        DeactivateCircle(GO_CurrentHitTarget);//deactive the current targets circle
                        GO_CurrentHitTarget = null;//set the current target to null
                        // andy
                        GO_CurrentHitTarget = RayHit().collider.gameObject;

                    }
                }
                if (GO_CurrentHitTarget.GetComponent<MU_Circle>() == null)//if the current target circle is deactivated
                {
                    ActivateCircle(GO_CurrentHitTarget);//activate it
                }
            }
            else
            {
                FindAllActiveAims();
            }
        }
        else // if youre not hitting anything
        {
            FindAllActiveAims();
        }
    }
    public void FindAllActiveAims()
    {
        HL_ObjectProperties[] InteractableObjects = GameObject.FindObjectsOfType<HL_ObjectProperties>();// rfind all interactable objects
        foreach (HL_ObjectProperties obj in InteractableObjects)
        {
            if (obj.gameObject.GetComponent<MU_Circle>() != null)//if the circle is active on any of them
            {
                DeactivateCircle(obj.gameObject);//deactivate the circle on said object.
            }
        }
        //DeactivateCircle(GO_PreviousHitTarget);
        DeactivateCircle(GO_CurrentHitTarget);
        // DeactivateCircle(RayHit().collider.gameObject);
        GO_CurrentHitTarget = null;
        GO_PreviousHitTarget = null;

    }
    void ActivateCircle(GameObject vGO)//sets circle to active
    {
        MU_Circle Circle = vGO.AddComponent<MU_Circle>();//adds circle script
        Circle.lineRenderer.material = LineMat;// adds material to circle
    }
    void DeactivateCircle(GameObject vGO)//deactivates circle
    {
        if (vGO != null)
        {
            Destroy(vGO.GetComponent<MU_Circle>());//removes circle component
            Destroy(vGO.GetComponent<LineRenderer>());//removes line component
        }
    }
    public GameObject Aim()
    {
        return aim;
    }
}
