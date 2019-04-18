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
    public enum PoleType
    {
        Northpole,
        SouthPole
    };
    public PoleType Poletype;
    void Start()
    {
        My_ObjectProps = transform.root.GetComponent<HL_ObjectProperties>();
        GetComponent<SpriteRenderer>().color = Color.white;
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
                // shoot raycast from each shooter
                //shoot 2 raycast to work for layers and independently of layers with first hit object
                RaycastHit2D hit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), My_ObjectProps.Fl_Range, layerrrr);
                RaycastHit2D MetalHit2D = Physics2D.Raycast(GO_RaycastShooters[i].transform.position, GO_RaycastShooters[i].transform.TransformDirection(Vector3.right), My_ObjectProps.Fl_Range);
                HL_ObjectProperties.ObjectType CurrentType = My_ObjectProps.MyObjectType;
                //--------------------

                switch (CurrentType)
                {
                    //trigger fixed maggnets
                    case HL_ObjectProperties.ObjectType.FixedMagnet:
                        if (hit2D.collider != null && hit2D.collider.transform.parent != null)// if ive hit something
                        {
                            Vector3 Startinpos = transform.parent.transform.position;// startpos=mypos
                            HL_Poles Hitpole = hit2D.collider.transform.parent.gameObject.GetComponent<HL_Poles>();// hitpole=pole i just hit
                            if (Hitpole != null)// if i hit a pole
                            {
                                GameObject Go_TargetMagnet = Hitpole.transform.parent.gameObject;//
                                FixedMagnets(Hitpole, Go_TargetMagnet, GO_RaycastShooters[i].gameObject);
                            }
                        }
                        break;
                        //trigger metals
                    case HL_ObjectProperties.ObjectType.Metal:
                        if (hit2D.collider != null && hit2D.collider.transform.parent != null)
                        {
                            GameObject HitObject = hit2D.collider.gameObject;
                            HL_ObjectProperties objprops = HitObject.transform.root.transform.GetComponent<HL_ObjectProperties>();
                            Metals(objprops, GO_RaycastShooters[i].gameObject);
                        }
                        break;
                    // trigger magnet function
                    case HL_ObjectProperties.ObjectType.Magnet:
                        if (hit2D.collider != null && hit2D.collider.transform.parent != null)
                        {
                            Vector3 Startinpos = transform.parent.transform.position;
                            HL_Poles Hitpole = hit2D.collider.transform.parent.gameObject.GetComponent<HL_Poles>();
                            if (Hitpole != null)
                            {
                                GameObject Go_TargetMagnet = Hitpole.transform.parent.gameObject;
                                Magnet(Hitpole, Go_TargetMagnet, GO_RaycastShooters[i].gameObject);
                            }
                        }
                        break;
                    //trigger nothing
                    case HL_ObjectProperties.ObjectType.FixedMetal:
                        break;
                }
            }
        }
    }
    // main magnet function 
    // trigger by case swich
    void Magnet(HL_Poles Hitpole, GameObject Go_TargetMagnet, GameObject Raycast)
    {
        //atract and repel magnets
        if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal
            && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.Metal)
        {
            //atracting
            if (Hitpole.Poletype != Poletype)
            {
                // cannot atract fixed magnets
                if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet)
                {

                    if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= My_ObjectProps.Fl_Range+0.1f
                    && Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange * 1.8f))
                    {
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = false;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = true;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = Raycast.name; // transmit the name of the shooter
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
                    }
                }
            }
            //repeling
            else
            {
                if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= My_ObjectProps.Fl_Range)
                {
                    if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet)
                    {
                        //print("Repel Magnet 2" + gameObject.transform.root.transform.name);
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = false;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = true;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = Raycast.name; // transmit the name of the shooter
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
                    }
                }
            }
        }
        //logic for metals while beeing a magnet
        else if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType == HL_ObjectProperties.ObjectType.Metal)
        {
            if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= My_ObjectProps.Fl_Range+0.1
                   && Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange * 1.8f))
            {
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = false;
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = true;
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = Raycast.name; // transmit the name of the shooter
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
            }
        }
   }
    //---------------
    // main fixed magnet function 
    // triggered by case swich
    void FixedMagnets(HL_Poles Hitpole, GameObject Go_TargetMagnet, GameObject Raycast)
    {
        // this manages atraction and repeling of magnets but not metals
        if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal
            && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.Metal)
        {

            //atracting
            if (Hitpole.Poletype != Poletype)
            {
                if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet)
                {
                    if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= My_ObjectProps.Fl_Range+0.1f
                    && Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange))
                    {
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = false;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = true;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = Raycast.name; // transmit the name of the shooter
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
                    }
                }
            }
            else if (Hitpole.Poletype == Poletype)
            {
                if (Vector3.Distance(transform.root.transform.position, Go_TargetMagnet.transform.position) <= My_ObjectProps.Fl_Range)
                {
                    if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMagnet
                        && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal)
                    {
                       // print("repeling magnet while beeing fixed magnet."+ gameObject.transform.root.name);
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = false;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = true;
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = Raycast.name; // transmit the name of the shooter
                        Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
                    }
                  
                }
            }
        }

        // atracting of metals but not fixed metals
        else if (Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType != HL_ObjectProperties.ObjectType.FixedMetal && Go_TargetMagnet.GetComponent<HL_ObjectProperties>().MyObjectType == HL_ObjectProperties.ObjectType.Metal)
        {
            if (Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) <= Go_TargetMagnet.GetComponent<HL_ObjectProperties>().Fl_Range +0.1f
                   && Vector3.Distance(transform.position, Go_TargetMagnet.transform.position) >= (My_ObjectProps.Fl_MinimumMagneticRange * 1.8f))
            {
                //print("atracting a metal");
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_Repeling = false;
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().bl_atracting = true;
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().st_Direction = Raycast.name; // transmit the name of the shooter
                Go_TargetMagnet.GetComponent<HL_ObjectProperties>().go_MyTarget = transform.root.gameObject;
            }

        }
    }
    //-------------------
    // 
    void Metals(HL_ObjectProperties objprops, GameObject Raycast)
    {
        if (objprops != null && My_ObjectProps.MyObjectType != HL_ObjectProperties.ObjectType.Metal)
        {
            if (objprops.MyObjectType == HL_ObjectProperties.ObjectType.Metal)
            {
                objprops.bl_Repeling = false;
                objprops.bl_atracting = true;
                objprops.st_Direction = Raycast.name; // transmit the name of the shooter
                objprops.go_MyTarget = transform.root.gameObject;
            }
            if (objprops.MyObjectType == HL_ObjectProperties.ObjectType.FixedMetal)
            {
                return;
            }
        }
    }
    //-------------------
    //
    void FixedMetals()
    {
        //there shoud be nothing here to do
    }
    //---------------------
    // render tole collour for distinction 
    void CheckPoleType()
    {
        if (Poletype == PoleType.Northpole)
        {
            GetComponent<SpriteRenderer>().sprite =transform.parent.GetComponent<HL_ObjectProperties>().Sp_NorthPole;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite= transform.parent.GetComponent<HL_ObjectProperties>().Sp_SouthPole;
        }
    }
    //----------------------
}
