using UnityEngine;

public class HL_TriggerLevelProgress : MonoBehaviour
{
    protected bool bl_TriggerSave;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (bl_TriggerSave == false)
            {
                HL_MainManager.instance.SceenManagement(HL_MainManager.instance.Curent_Level_Ref() + 1);
                HL_MainManager.instance.SerCurent_Level(HL_MainManager.instance.Curent_Level_Ref() + 1);
                HL_MainManager.instance.TriggerSave();
                bl_TriggerSave = true;
            }
        }
    }
}
