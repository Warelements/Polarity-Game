using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HL_ObjectProperties : MonoBehaviour
{

    [SerializeField]
    private Transform[] Children;
    private SpriteRenderer SR_spriterenderer;

    public bool unchangeable=false;
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
        FixedMetal
    }
    public enum Rotation
    {
        VerticalObject,
        HorisontalObject,
    }
    [SerializeField] private Rotation ObjectRotation;

    [SerializeField]
    public ObjectType MyObjectType;
    private ObjectType StartingObjecttype;

    //timer for metal
    public float Fl_ReverseTimer = 5f;
    public float Fl_Starttime = 5f;
    public TextMesh Tm_Textmesh;
    public bool Bl_CanDecreaseTimer;
    public Gradient textcolour;
    //----------

    //Sprites
    [SerializeField] private Sprite Sp_MagnetSprite;
    [SerializeField]
    private Sprite Sp_MetalSprite;
    [SerializeField]
    private Sprite Sp_FixedMetalSprite;
    public Sprite Sp_NorthPole;
    public Sprite Sp_SouthPole;
    //------------
    public string st_Direction;
    public Vector2 v2_MoveDirection;


    public bool bl_atracting;
    public bool bl_Repeling;

    public string St_Collidedobjectlocation;
    public List<RaycastHit2D> lt_CosisionsList = new List<RaycastHit2D>();
    public RaycastHit2D curentDown;
    public RaycastHit2D curentUp;
    public RaycastHit2D curentLeft;
    public RaycastHit2D curentRight;

    //boxcasat Variables 
    public Rect Box;
    public Rect Box2;
    public float BoxDistance;
    public LayerMask mask;

    // Update is called once per frame
    private void Start()
    {
        SR_spriterenderer = GetComponent<SpriteRenderer>();
        SR_spriterenderer.color = Color.white;
        Tm_Textmesh.gameObject.SetActive(false);
        StartingObjecttype = MyObjectType;
    }
    void Update()
    {
        Raycast2dArray();
        //---
        AtractOrReppel();
        SetDirection();
        MoveToTarget();
        if (Bl_CanDecreaseTimer)
        {
            Tm_Textmesh.gameObject.SetActive(true);
            if (Fl_ReverseTimer > 0)
            {
                Fl_ReverseTimer -= Time.deltaTime;
            }
            else
            {
                Tm_Textmesh.gameObject.SetActive(false);
                Fl_ReverseTimer = Fl_Starttime;
                Bl_CanDecreaseTimer = false;
                MyObjectType = StartingObjecttype;
            }
            ShowTimer();
        }
        Children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Children[i] = transform.GetChild(i);
        }
        DefineObjectTypes();
    }
    void ShowTimer()
    {
        Tm_Textmesh.text = Mathf.Round(Fl_ReverseTimer).ToString();
        Tm_Textmesh.color = textcolour.Evaluate(1 / Fl_ReverseTimer);
    }
    void DefineObjectTypes()
    {
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.Magnet)
        {
            //BoxCollider.enabled = true;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Magnets();
        }
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.Metal)
        {
            //BoxCollider.enabled = true;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            Metal();
        }
        #region ADDtoAndy
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.FixedMagnet)
        {
            //BoxCollider.enabled = true;
            // gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            FixedMagnet();
        }
        if (gameObject.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.FixedMetal)
        {
            //BoxCollider.enabled = true;
            //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            FixedMetal();
        }
        
        #endregion




    }
    void Magnets()
    {
        SR_spriterenderer.enabled = false;
        for (int i = 0; i < Children.Length; i++)
        {
            if (Children[i].GetComponent<TextMesh>() == null)
            {
                Children[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    void FixedMagnet()
    {
        SR_spriterenderer.enabled = false;
        for (int i = 0; i < Children.Length; i++)
        {
            if (Children[i].GetComponent<TextMesh>() == null)
            {
                Children[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    void UnaffectedMetal()
    {
        SR_spriterenderer.enabled = true;SR_spriterenderer.sprite = Sp_MetalSprite;
        for (int i = 0; i < Children.Length; i++)
        {
            if (Children[i].GetComponent<TextMesh>() == null)
            {
                Children[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    void Metal()
    {
        SR_spriterenderer.enabled = true;
        SR_spriterenderer.sprite = Sp_MetalSprite;
        for (int i = 0; i < Children.Length; i++)
        {
            if (Children[i].GetComponent<TextMesh>() == null)
            {
                Children[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    void FixedMetal()
    {
        SR_spriterenderer.enabled = true;
        SR_spriterenderer.sprite = Sp_FixedMetalSprite;
        for (int i = 0; i < Children.Length; i++)
        {
            if (Children[i].GetComponent<TextMesh>() == null)
            {
                Children[i].GetComponent<SpriteRenderer>().enabled = false;
            }
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
        if (bl_atracting == true)
        {
            bl_Repeling = false;
        }
        else if (bl_Repeling == true)
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
                    // atracting  done
                    if (bl_Repeling == false)
                    {

                        if (!lt_CosisionsList.Contains(curentRight))
                        {

                            if (transform.eulerAngles.z == 90)
                            {
                                v2_MoveDirection = Vector2.down;
                            }
                            else v2_MoveDirection = Vector2.right;
                        }

                    }
                    //repeling
                    if (bl_Repeling == true)
                    {
                        if (!lt_CosisionsList.Contains(curentLeft))
                        {
                            if (transform.eulerAngles.z == 90)
                            {
                                v2_MoveDirection = Vector2.down;
                            }
                            else v2_MoveDirection = Vector2.right;
                        }
                    }
                }
                else if (st_Direction == "Right")
                {
                    // atracting done
                    if (bl_Repeling == false)
                    {
                        if (!lt_CosisionsList.Contains(curentLeft))
                        {
                            if (transform.eulerAngles.z == 90)
                            {
                                //print("right.up");
                                v2_MoveDirection = Vector2.up;
                            }
                            else v2_MoveDirection = Vector2.left;
                        }

                    }
                    //repeling
                    if (bl_Repeling == true)
                    {
                        if (!lt_CosisionsList.Contains(curentRight))
                        {
                            if (transform.eulerAngles.z == 90)
                            {
                                //print("right.up");
                                v2_MoveDirection = Vector2.up;
                            }
                            else v2_MoveDirection = Vector2.left;
                        }
                    }
                }
                else if (st_Direction == "Top")
                {
                    //atracting
                    if (bl_Repeling == false)
                    {

                        if (!lt_CosisionsList.Contains(curentDown))
                        {

                            if (transform.eulerAngles.z == 90)
                            {
                                //  print("top.left 3");

                                v2_MoveDirection = Vector2.left;
                            }
                            else v2_MoveDirection = Vector2.down;
                        }

                    }
                    // repeling
                    if (bl_Repeling == true)
                    {
                        if (!lt_CosisionsList.Contains(curentUp))
                        {
                            if (transform.eulerAngles.z == 90)
                            {
                                v2_MoveDirection = Vector2.left;
                            }
                        }
                    }
                }
                else if (st_Direction == "Down")
                {
                    //atracting
                    if (bl_Repeling == false)
                    {
                        if (!lt_CosisionsList.Contains(curentUp))
                        {
                            if (transform.eulerAngles.z == 90)
                            {
                                v2_MoveDirection = Vector2.right;
                            }
                            else v2_MoveDirection = Vector2.up;
                        }

                    }
                    //repeling
                    if (bl_Repeling == true)
                    {
                        if (!lt_CosisionsList.Contains(curentDown))
                        {
                            if (transform.eulerAngles.z == 90)
                            {
                                //print("down.right repeling");
                                v2_MoveDirection = Vector2.right;
                            }
                            else v2_MoveDirection = Vector2.up;
                        }
                    }
                }
            }
        }
    }
    public void MoveToTarget()
    {   
        // this makes myself move.
        if (go_MyTarget != null)
        {
            // if target is a not a fixed metal(first one was magnet.) or fixed magnet
            if (go_MyTarget.GetComponent<HL_ObjectProperties>().MyObjectType != ObjectType.FixedMetal || go_MyTarget.GetComponent<HL_ObjectProperties>().MyObjectType != ObjectType.FixedMagnet)
            {
                // if the distance is less than the targets magnetic  ranage
                //old
                /*
                if (Vector3.Distance(transform.position, go_MyTarget.transform.position) <= go_MyTarget.GetComponent<HL_ObjectProperties>().Fl_Range)
                {
                    // of distance between the 2 pools les or equal to my minimum magentic range
                    if (Vector3.Distance(transform.position, go_MyTarget.transform.GetChild(0).transform.position) >= Fl_MinimumMagneticRange || Vector3.Distance(transform.position, go_MyTarget.transform.GetChild(1).transform.position) >= Fl_MinimumMagneticRange)
                    {
                        if (bl_atracting == true)
                        {
                            transform.Translate(v2_MoveDirection * Time.deltaTime);
                        }
                        if (bl_Repeling == true)
                        {
                            transform.Translate(-v2_MoveDirection * Time.deltaTime);
                        }
                    }
                }
                */
                // new atract
                if (Vector3.Distance(transform.position, go_MyTarget.transform.position) <= go_MyTarget.GetComponent<HL_ObjectProperties>().Fl_Range + 6.1f && bl_atracting == true)
                {

                    transform.Translate(v2_MoveDirection * Time.deltaTime);
                
                    
                }
                // new repeling
                if (Vector3.Distance(transform.position, go_MyTarget.transform.position) <= go_MyTarget.GetComponent<HL_ObjectProperties>().Fl_Range && bl_Repeling== true)
                {

                transform.Translate(-v2_MoveDirection * Time.deltaTime);
                }
                
                
                // if im out of my targets range my target is null
               if (Vector3.Distance(transform.position, go_MyTarget.transform.position) > go_MyTarget.GetComponent<HL_ObjectProperties>().Fl_Range)
                {
                    go_MyTarget = null;
                }
            }
            if (go_MyTarget != null)
            {
                if (go_MyTarget.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.Metal || go_MyTarget.GetComponent<HL_ObjectProperties>().MyObjectType == ObjectType.FixedMetal)
                {
                    print("is magnet" + gameObject.name);
                    go_MyTarget = null;
                    st_Direction = null;
                    v2_MoveDirection = new Vector2(0, 0);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Player")
        {
            go_MyTarget = null;
            st_Direction = null;
            v2_MoveDirection = new Vector2(0, 0);
        }
    }
    void Raycast2dArray()
    {
        switch (ObjectRotation)
        {
            case Rotation.HorisontalObject:

                Box.width = 0.58f;
                Box.height = 0.01f;
                Box2.width = 0.01f;
                Box2.height = 0.28f;
                //w 0.01 h.0.28
                RaycastHit2D HitLeft = Physics2D.BoxCast(new Vector2((transform.position.x - 0.18523f * 2) - Box2.center.x, transform.position.y), Box2.size, 0, Vector2.left, BoxDistance, mask);
                RaycastHit2D HitRight = Physics2D.BoxCast(new Vector2((transform.position.x + 0.18523f * 2) + Box2.center.x, transform.position.y), Box2.size, 0, Vector2.right, BoxDistance, mask);

                //w 0.58 , h0.01 distance 0.01
                RaycastHit2D HitTop = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y + 0.1765523f + Box.center.y), Box.size, 0, Vector2.up, BoxDistance, mask);
                RaycastHit2D HitDown = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y - 0.1765523f - Box.center.y), Box.size, 0, Vector2.down, BoxDistance, mask);



                // debugging
                //set 1
                Debug.DrawLine(new Vector3(transform.position.x + 0.175f, transform.position.y - 0.175f, 0), new Vector3(transform.position.x + 0.175f, transform.position.y - 0.175f, 0) + Vector3.down, Color.green);
                Debug.DrawLine(new Vector3(transform.position.x + 0.175f, transform.position.y + 0.175f, 0), new Vector3(transform.position.x + 0.175f, transform.position.y - 0.175f, 0) + Vector3.up, Color.green);
                //set 2
                Debug.DrawLine(new Vector3(transform.position.x - 0.175f, transform.position.y - 0.175f, 0), new Vector3(transform.position.x - 0.175f, transform.position.y - 0.175f, 0) + Vector3.down, Color.green);
                Debug.DrawLine(new Vector3(transform.position.x - 0.175f, transform.position.y + 0.175f, 0), new Vector3(transform.position.x - 0.175f, transform.position.y - 0.175f, 0) + Vector3.up, Color.green);
                //other
                Debug.DrawLine(new Vector3(transform.position.x + 0.1765523f * 2, transform.position.y, 0), transform.position + Vector3.right, Color.green);
                Debug.DrawLine(new Vector3(transform.position.x - 0.1765523f * 2, transform.position.y, 0), transform.position + Vector3.left, Color.green);

                // asigning
                curentDown = HitDown;
                curentUp = HitTop;
                curentLeft = HitLeft;
                curentRight = HitRight;

                ADDandRemoveraycast(HitLeft);
                ADDandRemoveraycast(HitRight);
                ADDandRemoveraycast(HitTop);
                ADDandRemoveraycast(HitDown);

                break;
            case Rotation.VerticalObject:

                Box.width = 0.01f;
                Box.height = 0.58f;
                Box2.width = 0.28f;
                Box2.height = 0.01f;
                //w 0.01 h.0.28
                RaycastHit2D HitLeft2 = Physics2D.BoxCast(new Vector2(transform.position.x - 0.1765523f - Box.center.x, transform.position.y), Box.size, 0, Vector2.left, BoxDistance, mask);
                RaycastHit2D HitRight2 = Physics2D.BoxCast(new Vector2(transform.position.x + 0.1765523f + Box.center.x, transform.position.y), Box.size, 0, Vector2.right, BoxDistance, mask);

                //w 0.58 , h0.01 distance 0.01
                RaycastHit2D HitTop2 = Physics2D.BoxCast(new Vector2(transform.position.x, (transform.position.y + 0.18523f * 2) + Box2.center.y), Box2.size, 0, Vector2.up, BoxDistance, mask);
                RaycastHit2D HitDown2 = Physics2D.BoxCast(new Vector2(transform.position.x, (transform.position.y - 0.18523f * 2) - Box2.center.y), Box2.size, 0, Vector2.down, BoxDistance, mask);

                if (HitDown2.transform != null)
                {
                    print(HitDown2.transform.name);
                }

                curentDown = HitDown2;
                curentUp = HitTop2;
                curentLeft = HitLeft2;
                curentRight = HitRight2;

                ADDandRemoveraycast(HitLeft2);
                ADDandRemoveraycast(HitRight2);
                ADDandRemoveraycast(HitTop2);
                ADDandRemoveraycast(HitDown2);
                break;
        }
    }
    void ADDandRemoveraycast(RaycastHit2D vHit)
    {
        if (vHit.collider != null && vHit.transform.tag != "Player")
        {
            if (!lt_CosisionsList.Contains(vHit))
            {
                lt_CosisionsList.Add(vHit);
            }
        }
        else if (vHit.collider == null)
        {
            if (lt_CosisionsList.Contains(vHit))
            {
                lt_CosisionsList.Remove(vHit);
            }
        }
    }
}
