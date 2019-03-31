using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_Poles : MonoBehaviour
{
    public MU_ObjectProperties ObjectProps;
    [SerializeField]
    GameObject[] GO_RaycastShooters = null;
    //[SerializeField]
    //private float Fl_Range = 4;
    [SerializeField]
    private float Fl_RepulsionRange = 4;
    [SerializeField]
    private float Fl_MagneticRange = 2;
    [SerializeField]
    private float Fl_MovementSpeed = 0.2f;
    [SerializeField]
    private float Fl_MetalPullSpeed = 0.2f;
    public LayerMask layerrrr;
    private Vector3 mV3_StartingPos;
    public enum PoleType
    {
        Northpole,
        SouthPole
    };
    public PoleType Poletype;
    void Start()
    {
        mV3_StartingPos = transform.root.position;
        ObjectProps = transform.root.GetComponent<MU_ObjectProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        Detection();
        CheckPoleType();
      
    }
    //void Detection()
    //{
    //    if (GO_RaycastShooters != null)
    //    {
    //        for (int i = 0; i < GO_RaycastShooters.Length; i++)
    //        {
    //            //shoot 2 raycast to work for layers and independently of layers with first hit object
    //            RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), Fl_Range, layerrrr);
    //            RaycastHit2D MetalHit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), Fl_Range);

    //            if (hit2D.collider != null)
    //            {
    //                Vector3 Startinpos = transform.parent.transform.position;
    //                MU_Poles Hitpole = hit2D.collider.transform.parent.gameObject.GetComponent<MU_Poles>();
    //                if (Hitpole != null)
    //                {
    //                    GameObject Go_TargetMagnet = Hitpole.transform.parent.gameObject;
    //                    if (transform.parent.GetComponent<MU_ObjectProperties>().MyObjectType == MU_ObjectProperties.ObjectType.Magnet)
    //                    {
    //                        if (Hitpole.Poletype != Poletype)
    //                        {
    //                            Attract(Go_TargetMagnet, Fl_MovementSpeed);
    //                        }
    //                        else
    //                        {
    //                            Repel(Go_TargetMagnet, Fl_MovementSpeed);
    //                        }
    //                    }
    //                }
    //            }
    //            //metals
    //            if (MetalHit2D.collider != null)
    //            {
    //                GameObject HitObject = MetalHit2D.collider.gameObject;
    //                //print(transform.root.name + " " + HitObject.name);
    //                MU_ObjectProperties objprops = HitObject.GetComponent<MU_ObjectProperties>();
    //                //if hit object is a metal
    //                if (objprops != null)
    //                {
    //                    //print(HitObject.name);
    //                    if (objprops.MyObjectType == MU_ObjectProperties.ObjectType.Metal)
    //                    {
    //                        print("Attracting" + HitObject.name + "mynameis" + transform.root.name);
    //                        AttractMetal(HitObject, Fl_MetalPullSpeed);
    //                    }
    //                    //if (transform.parent.GetComponent<MU_ObjectProperties>().MyObjectType == MU_ObjectProperties.ObjectType.FixedMagnet)
    //                    //{
    //                    //    Fixedmagnet(HitObject,0.2f);
    //                    //}
    //                }
    //            }
    //        }
    //    }
    //}
    void Detection()
    {
        if (GO_RaycastShooters != null)
        {
            for (int i = 0; i < GO_RaycastShooters.Length; i++)
            {
                //shoot 2 raycast to work for layers and independently of layers with first hit object
                RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right),ObjectProps.Fl_Range, layerrrr);
                RaycastHit2D MetalHit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), ObjectProps.Fl_Range);

                //fixedmagnet attracting metal
                if (transform.parent.GetComponent<MU_ObjectProperties>().MyObjectType == MU_ObjectProperties.ObjectType.FixedMagnet)
                {
                    RaycastHit2D FixwdHit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), ObjectProps.Fl_Range);
                    if (FixwdHit2D.collider != null)
                    {
                        GameObject HitObject = FixwdHit2D.collider.gameObject;
                        MU_ObjectProperties objprops = HitObject.GetComponent<MU_ObjectProperties>();
                        if (objprops != null)
                        {
                            if (objprops.MyObjectType == MU_ObjectProperties.ObjectType.Metal)
                            {
                                AttractMetal(HitObject, Fl_MetalPullSpeed);
                            }
                            if (objprops.MyObjectType == MU_ObjectProperties.ObjectType.FixedMetal)
                            {
                                return;
                            }
                        }
                    }
                }

                if (hit2D.collider != null && hit2D.collider.transform.parent != null)
                {
                    Vector3 Startinpos = transform.parent.transform.position;
    
                    MU_Poles Hitpole = hit2D.collider.transform.parent.gameObject.GetComponent<MU_Poles>();
                    //magnet
                    if (Hitpole != null)
                    {
                        // if it has poles its either a magnet or a fixed magnet
                        GameObject Go_TargetMagnet = Hitpole.transform.parent.gameObject;
                        //magnet
                        if (transform.parent.GetComponent<MU_ObjectProperties>().MyObjectType == MU_ObjectProperties.ObjectType.Magnet)
                        {
                            if (Hitpole.Poletype != Poletype)
                            {
                                Attract(Go_TargetMagnet, Fl_MovementSpeed);
                            }
                            else
                            {
                                Repel(Go_TargetMagnet, Fl_MovementSpeed);
                            }
                        }
                        //fixed magnet
                        if (transform.parent.GetComponent<MU_ObjectProperties>().MyObjectType == MU_ObjectProperties.ObjectType.FixedMagnet)
                        {
                            transform.parent.transform.position = mV3_StartingPos;
                            if (Hitpole.Poletype != Poletype)
                            {
                                Attract(Go_TargetMagnet, Fl_MovementSpeed);
                            }
                            else
                            {
                                Repel(Go_TargetMagnet, Fl_MovementSpeed);
                            }
                        }
                    }
                    ////metals
                    //if (MetalHit2D.collider != null)
                    //{
                    //    GameObject HitObject = MetalHit2D.collider.gameObject;
                    //    print(transform.root.name + " " + HitObject.name);
                    //    MU_ObjectProperties objprops = HitObject.GetComponent<MU_ObjectProperties>();
                    //    //if hit object is a metal
                    //    if (objprops != null)
                    //    {
                    //        if (objprops.MyObjectType == MU_ObjectProperties.ObjectType.Metal)
                    //        {
                    //            print("Attracting" + HitObject.name + "mynameis" + transform.root.name);
                    //            AttractMetal(HitObject, Fl_MetalPullSpeed);
                    //        }
                    //    }
                    //}
                }
                //metals
                if (MetalHit2D.collider != null)
                {
                    GameObject HitObject = MetalHit2D.collider.gameObject;
                    // print(transform.root.name + " " + HitObject.name);
                    MU_ObjectProperties objprops = HitObject.GetComponent<MU_ObjectProperties>();
                    //if hit object is a metal
                    if (objprops != null)
                    {
                        if (objprops.MyObjectType == MU_ObjectProperties.ObjectType.Metal)
                        {
                            print("Attracting" + HitObject.name + "mynameis" + transform.root.name);
                            AttractMetal(HitObject, Fl_MetalPullSpeed);
                        }
                        if (objprops.MyObjectType == MU_ObjectProperties.ObjectType.FixedMetal)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }








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
            vMagnetoAttract.transform.position = Vector3.MoveTowards(vMagnetoAttract.transform.position, transform.parent.transform.position, vFl_MovementSpeed * Time.deltaTime);
        }
    }
    void AttractMetal(GameObject vGO, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.parent.position, vGO.transform.position) > Fl_MagneticRange)
        {
            vGO.transform.position = Vector3.MoveTowards(vGO.transform.position, transform.position, vFl_MovementSpeed * Time.deltaTime);
        }
    }
    void Repel(GameObject vGO, float vFl_MovementSpeed)
    {
        if (Vector3.Distance(transform.position, vGO.transform.position) < Fl_RepulsionRange) // && Vector3.Distance(transform.position, vGO.transform.position) >= Fl_MagneticRange)
        {
            vGO.transform.position = Vector3.MoveTowards(vGO.transform.position, transform.position, -vFl_MovementSpeed * Time.deltaTime);
        }
    }
    //void Fixedmagnet(GameObject vGo, float vFl_Speed)
    //{
    //    if (GO_RaycastShooters != null)
    //    {
    //        for (int i = 0; i < GO_RaycastShooters.Length; i++)
    //        {
    //            //shoot 2 raycast to work for layers and independently of layers with first hit object
    //            RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), Fl_Range);
    //            GameObject HitObject = hit2D.collider.gameObject;
    //            Attract(HitObject, 0.2f);

    //        }
    //    }
    //}
    void Fixedmagnet(GameObject vGo, float vFl_Speed)
    {
        if (GO_RaycastShooters != null)
        {
            for (int i = 0; i < GO_RaycastShooters.Length; i++)
            {
                //shoot 2 raycast to work for layers and independently of layers with first hit object
                RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), ObjectProps.Fl_Range);
                GameObject HitObject = hit2D.collider.gameObject;
                Attract(HitObject, 0.2f);

            }
        }
    }

}