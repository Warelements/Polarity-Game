using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_Poles : MonoBehaviour
{

    public HL_ObjectProperties My_ObjectProps;

    [SerializeField] GameObject[] GO_RaycastShooters = null;
    [SerializeField] private float Fl_RepulsionRange = 4;
    [SerializeField] private float Fl_MagneticRange = 2;
    [SerializeField] private float Fl_MovementSpeed = 0.2f;
    [SerializeField] private float Fl_MetalPullSpeed = 0.2f;

    public LayerMask layerrrr;
    private Vector3 mV3_StartingPos;

    bool test;
    public enum PoleType
    {
        Northpole,
        SouthPole
    };
    public PoleType Poletype;
    void Start()
    {
        mV3_StartingPos = transform.root.position;
        My_ObjectProps = transform.root.GetComponent<HL_ObjectProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        Detection();  // this triggers raycast for each shooter and check for a hit
        CheckPoleType();

    }
    //---------------    
    void Detection()
    {
        if (GO_RaycastShooters != null)
        {
            for (int i = 0; i < GO_RaycastShooters.Length; i++)
            {
                //shoot 2 raycast to work for layers and independently of layers with first hit object
                RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), My_ObjectProps.Fl_Range, layerrrr);
                RaycastHit2D MetalHit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), My_ObjectProps.Fl_Range);

                //fixedmagnet attracting metal
                if (My_ObjectProps.MyObjectType == HL_ObjectProperties.ObjectType.FixedMagnet)
                {
                    RaycastHit2D FixwdHit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), My_ObjectProps.Fl_Range);
                    if (FixwdHit2D.collider != null)
                    {
                        GameObject HitObject = FixwdHit2D.collider.gameObject;
                        HL_ObjectProperties objprops = HitObject.GetComponent<HL_ObjectProperties>();
                        if (objprops != null)
                        {

                            // if the metal is not fixed then atract it otherwhise do nothing
                            if (objprops.MyObjectType == HL_ObjectProperties.ObjectType.Metal)
                            {
                                if (Vector3.Distance(transform.position, objprops.gameObject.transform.position) <= objprops.gameObject.GetComponent<HL_ObjectProperties>().Fl_Range
                                 && Vector3.Distance(transform.position, objprops.gameObject.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange*1.8f))
                                {
                                    objprops.bl_Repeling = false;
                                    objprops.bl_atracting = true;
                                    objprops.st_Direction = GO_RaycastShooters[i].name; // transmit the name of the shooter
                                    objprops.go_MyTarget = transform.root.gameObject;
                                  
                                    
                                }
                            }
                            else if (objprops.MyObjectType == HL_ObjectProperties.ObjectType.FixedMetal)
                            {
                                return;
                            }
                        }
                    }
                }

                if (hit2D.collider != null && hit2D.collider.transform.parent != null)
                {
                    Vector3 Startinpos = transform.parent.transform.position;
                    HL_Poles Hitpole = hit2D.collider.transform.parent.gameObject.GetComponent<HL_Poles>();
                   
                    
                    //magnet
                    if (Hitpole != null)
                    {
                        // if it has poles its either a magnet or a fixed magnet
                        GameObject Go_TargetMagnet = Hitpole.transform.parent.gameObject;
                        //magnet
                        if (My_ObjectProps.MyObjectType == HL_ObjectProperties.ObjectType.Magnet)
                        {
                            if (Hitpole.Poletype != Poletype )
                            {
                                if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.Metal)
                                {
                                    if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= Go_TargetMagnet.GetComponent<HL_ObjectProperties>().Fl_Range
                                    && Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange * 1.8f))
                                    {
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = false;
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = true;
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = GO_RaycastShooters[i].name; // transmit the name of the shooter
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;

                                    }
                                }
                            }
                            else
                            {
                                if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= Go_TargetMagnet.GetComponent<HL_ObjectProperties>().Fl_Range)
                                {
                                    if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.Metal)
                                    {
                                        print("Repel Magnet" + gameObject.transform.root.transform.name);
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = false;
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = true;
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = GO_RaycastShooters[i].name; // transmit the name of the shooter
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
                                    }
                                }
                            }
                        }
                        //fixed magnet
                        if (My_ObjectProps.MyObjectType == HL_ObjectProperties.ObjectType.FixedMagnet)
                        {

                            if (Hitpole.Poletype != Poletype)
                            {
                                if (Hitpole.Poletype != Poletype)
                                {
                                    if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.Metal)

                                    {
                                        if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= Go_TargetMagnet.GetComponent<HL_ObjectProperties>().Fl_Range
                                        && Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange * 1.8f))
                                        {
                                            Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = false;
                                            Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = true;
                                            Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = GO_RaycastShooters[i].name; // transmit the name of the shooter
                                            Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;

                                        }
                                    }
                                }

                            }
                            else if (Hitpole.Poletype == Poletype)
                            {
                                if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= Go_TargetMagnet.GetComponent<HL_ObjectProperties>().Fl_Range)
                                {
                                    if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal)
                                    {
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = false;
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = true;
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = GO_RaycastShooters[i].name; // transmit the name of the shooter
                                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
                                    }
                                }
                            }
                        }
                    }

                }
                //metals
                if (hit2D.collider != null  && hit2D.collider.transform.parent != null)
                {
                    GameObject HitObject = hit2D.collider.gameObject;
                    HL_ObjectProperties objprops = HitObject.transform.root.transform.GetComponent<HL_ObjectProperties>();
                    //if hit object is a metal
                    if (objprops != null && My_ObjectProps.MyObjectType != HL_ObjectProperties.ObjectType.Metal)
                    {
                        if (objprops.MyObjectType == HL_ObjectProperties.ObjectType.Metal)
                        {
                            print("Atract Metal 2" + gameObject.transform.root.transform.name);
                            objprops.bl_Repeling = false;
                            objprops.bl_atracting = true;
                            objprops.st_Direction = GO_RaycastShooters[i].name; // transmit the name of the shooter
                            objprops.go_MyTarget = transform.root.gameObject;
                        }
                        if (objprops.MyObjectType == HL_ObjectProperties.ObjectType.FixedMetal)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
    //---------------
    void CheckPoleType()
    {
        if (Poletype == PoleType.Northpole)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    void Attract(GameObject vMagnetoAttract, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.position, vMagnetoAttract.transform.position) > Fl_MagneticRange)
        {
            //    vMagnetoAttract.transform.position = Vector3.MoveTowards(vMagnetoAttract.transform.position, transform.parent.transform.position, vFl_MovementSpeed * Time.deltaTime);
        }
    }
    void AttractMetal(GameObject vGO, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.parent.position, vGO.transform.position) > Fl_MagneticRange)
        {
            // vGO.transform.position = Vector3.MoveTowards(vGO.transform.position, transform.position, vFl_MovementSpeed * Time.deltaTime);

        }
    }
    void Repel(GameObject vGO, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.position, vGO.transform.position) < Fl_RepulsionRange) // && Vector3.Distance(transform.position, vGO.transform.position) >= Fl_MagneticRange)
        {
            //  vGO.transform.position = Vector3.MoveTowards(vGO.transform.position, transform.position, -vFl_MovementSpeed * Time.deltaTime);
        }
    }

    void Fixedmagnet(GameObject vGo, float vFl_Speed)
    {
        if (GO_RaycastShooters != null)
        {
            for (int i = 0; i < GO_RaycastShooters.Length; i++)
            {
                //shoot 2 raycast to work for layers and independently of layers with first hit object
                RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), My_ObjectProps.Fl_Range);
                GameObject HitObject = hit2D.collider.gameObject;
                Attract(HitObject, 0.2f);

            }
        }
    }

    //-----------------



}