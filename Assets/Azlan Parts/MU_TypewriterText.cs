using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MU_TypewriterText : MonoBehaviour
{
    public TextMesh Tm_Text;
    string Story;
    string StartText;
    bool playtext;

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
    private void OnEnable()
    {
        //print("Enable");
        Tm_Text.text = "";
        StartCoroutine("PlayText");

}
    private void OnDisable()
    {
        Tm_Text.text = StartText;
       // Debug.Log("PrintOnDisable: script was disabled");
    }
    IEnumerator PlayText()
    {
        foreach (char c in Story)
        {
            Tm_Text.text += c;
            yield return new WaitForSeconds(0.125f);
        }
    }
}
