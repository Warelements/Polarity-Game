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
        FixedMagnet
    }
    [SerializeField]
    public ObjectType MyObjectType;
    //private BoxCollider2D BoxCollider;
    // Update is called once per frame
    private void Start()
    {
        SR_spriterenderer = GetComponent<SpriteRenderer>();
       // BoxCollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        print(SR_spriterenderer.enabled);  
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

        if (gameObject.GetComponent<MU_ObjectProperties>().MyObjectType == ObjectType.FixedMagnet)
        {
            //BoxCollider.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            FixedMagnet();
        }
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


}
