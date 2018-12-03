using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HL_ObjectProperties : MonoBehaviour {
    [SerializeField]
    private Transform[] Children;
    private SpriteRenderer SR_spriterenderer;

    public Vector3 direction;
    public float Fl_Range = 4;
    public float Fl_MinimumMagneticRange;
    public float fl_MagnetSpeed;
    public GameObject go_MyTarget;
    public enum ObjectType
    {
        Magnet,
        Metal,
        FixedMagnet,
        FixedMetal,
    }

    [SerializeField]
    public ObjectType MyObjectType;
 

    //timer for metal
    public float Fl_ReverseTimer = 5f;
    public float Fl_Starttime = 5f;
    public bool Bl_CanDecreaseTimer;
    //----------
    public string st_Direction;
    public Vector2 v2_MoveDirection;

    public bool bl_atracting;
    public bool bl_Repeling;

    // Update is called once per frame
    private void Start()
    {
        SR_spriterenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //---
        AtractOrReppel();
        SetDirection();
        MoveToTarget();
        //
        if (Bl_CanDecreaseTimer)
        {
            if (Fl_ReverseTimer > 0)
            {
                Fl_ReverseTimer -= Time.deltaTime;
            }
            else
            {
                Fl_ReverseTimer = Fl_Starttime;
                Bl_CanDecreaseTimer = false;
                //MyObjectType = StartingObjecttype;
            }
        }

        Children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Children[i] = transform.GetChild(i);
        }
        DefineObjectTypes();
    }
    void DefineObjectTypes()
    {
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.Magnet)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Magnets();
        }
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.Metal)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            Metal();
        }
        #region ADDtoAndy
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.FixedMagnet)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            FixedMagnet();
        }
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.FixedMetal)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            FixedMetal();
        }
        #endregion




    }
    void Magnets()
    {
        SR_spriterenderer.enabled = false;
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].gameObject.SetActive(true);
        }
    }
    void FixedMagnet()
    {
        SR_spriterenderer.enabled = false;
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].gameObject.SetActive(true);
        }
    }
    void Metal()
    {
        SR_spriterenderer.enabled = true;
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].gameObject.SetActive(false);
        }
    }
    void FixedMetal()
    {
        SR_spriterenderer.enabled = true;
        for (int i = 0; i < Children.Length; i++)
        {
            Children[i].gameObject.SetActive(false);
        }
    }
    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR
        direction = Vector3.Normalize(direction);
        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, direction, Fl_Range);
#endif
    }

    void AtractOrReppel()
    {
       if(bl_atracting == true)
        {
            bl_Repeling = false;
        }
       else if(bl_Repeling == true)
        {
            bl_atracting = false;
        }
}
    void SetDirection()
    {
        if (go_MyTarget != null)
        {
            if (Vector3.Distance(transform.position, go_MyTarget.transform.position) <= go_MyTarget.GetComponent<HL_ObjectProperties>().Fl_Range
                && Vector3.Distance(transform.position, go_MyTarget.transform.position) >= Fl_MinimumMagneticRange)
            {
                if (st_Direction == "Left")
                {
                    v2_MoveDirection = Vector2.right;
                }
                else if (st_Direction == "Right")
                {
                    v2_MoveDirection = Vector2.left;

                }
                else if (st_Direction == "Top")
                {
                    v2_MoveDirection = Vector2.down;
                }
                else if (st_Direction == "Down")
                {
                    v2_MoveDirection = Vector2.up;
                }
            }
        }
        

    }

    public void MoveToTarget()
    {
        if (go_MyTarget != null)
        {
            if (Vector3.Distance(transform.position, go_MyTarget.transform.position) <= go_MyTarget.GetComponent<HL_ObjectProperties>().Fl_Range)
            {
                if (Vector3.Distance(transform.position, go_MyTarget.transform.GetChild(0).transform.position) >= Fl_MinimumMagneticRange || Vector3.Distance(transform.position, go_MyTarget.transform.GetChild(1).transform.position) >= Fl_MinimumMagneticRange)
                {
                    if (bl_atracting == true)
                    {
                        print("Atracting");
                        transform.Translate(v2_MoveDirection * Time.deltaTime);
                    }
                    if (bl_Repeling == true)
                    {
                        print("Repeling");
                        transform.Translate(-v2_MoveDirection * Time.deltaTime);
                    }
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collided");
        //  Fl_MinimumMagneticRange = Vector3.Distance(transform.position, go_MyTarget.transform.position);
        go_MyTarget = null;
        st_Direction = null;
        v2_MoveDirection = new Vector2(0, 0);
    }

}
