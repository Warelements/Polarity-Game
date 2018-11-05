using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MU_ObjectProperties : MonoBehaviour
{
    [SerializeField]
    private Transform[] Children;
    private SpriteRenderer SR_spriterenderer;
    public enum ObjectType
    {
        Magnet,
        Metal,
        FixedMagnet,FixedMetal
    }
    [SerializeField]
    public ObjectType MyObjectType;
    #region add to andy
    public float Fl_ReverseTimer = 5f;
    public float Fl_Starttime = 5f;

    public bool Bl_CanDecreaseTimer;
    public ObjectType StartingObjecttype;
    #endregion





    //private BoxCollider2D BoxCollider;
    // Update is called once per frame
    private void Start()
    {
        SR_spriterenderer = GetComponent<SpriteRenderer>();
        #region addtoandy
        StartingObjecttype = MyObjectType;
        #endregion
        // BoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        #region addtoandy
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
                MyObjectType = StartingObjecttype;
            }
        }
        #endregion

        // print(SR_spriterenderer.enabled);  
        Children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Children[i] = transform.GetChild(i);
        }
        DefineObjectTypes();
    }
    void DefineObjectTypes()
    {
        if (gameObject.GetComponent<MU_ObjectProperties>().MyObjectType == ObjectType.Magnet)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Magnets();
        }
        if (gameObject.GetComponent<MU_ObjectProperties>().MyObjectType == ObjectType.Metal)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            Metal();
        }
        #region ADDtoAndy
        if (gameObject.GetComponent<MU_ObjectProperties>().MyObjectType == ObjectType.FixedMagnet)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            FixedMagnet();
        }
        if (gameObject.GetComponent<MU_ObjectProperties>().MyObjectType == ObjectType.FixedMetal)
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
}
