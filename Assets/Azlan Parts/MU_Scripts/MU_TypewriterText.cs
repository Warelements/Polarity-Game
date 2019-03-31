using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MU_TypewriterText : MonoBehaviour
{
    public TextMesh Tm_Text;
    public Text Globaltext;
    string Story;
    string StartText;
    bool playtext;
    public float TextSpeed;
    public HL_Text_Trigger triggerscript;
    void Awake()
    {
        
        Tm_Text = GetComponent<TextMesh>();
        if(Tm_Text==null)
        {
            return;
        }
        Story = Tm_Text.text;
        StartText = Story;
        // TODO: add optional delay when to start     
    }
    private void Update()
    {
        if (triggerscript != null)
        {
            if (triggerscript.Bl_Enabled)
            {
                GetComponent<MeshRenderer>().enabled = false;
                if (Globaltext != null)
                {
                    Globaltext.text = "";

                    StartCoroutine("PlayText");
                }
            }
            //if (!triggerscript.Bl_Enabled)
            //{
            //    print("hnn");
            //    Globaltext.text = "";
            //    Tm_Text.text = StartText;
            //    print("huh");

            //}
        }
    }
    //    private void OnEnable()
    //    {
    //        //print("Enable");
    //        //global text.text == blank
    //        //if (triggerscript!=null)
    //        //{
    //        //    if (triggerscript.Bl_Enabled)
    //        //    {

    //        //    } 
    //        //}


    //        if (Globaltext != null)
    //        {
    //            Globaltext.text = "";

    //            StartCoroutine("PlayText");
    //        }
    //}
    private void OnDisable()
    {
        print("hnn");
        if (Globaltext!=null)
        {
            Globaltext.text = ""; 
        }
        Tm_Text.text = StartText;
        print("huh");
        //Tm_Text.text = StartText;
        // Debug.Log("PrintOnDisable: script was disabled");
    }
    IEnumerator PlayText()
    {
        foreach (char c in Story)
        {
            Globaltext.text+= c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
}
