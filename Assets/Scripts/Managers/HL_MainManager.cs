using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HL_MainManager : MonoBehaviour
{
    public static HL_MainManager instance;
    public static bool loadgameoverlevel;

    protected int Curent_Level;     // curent level where the plyaer is.


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            StartLoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(loadgameoverlevel)
        //{
        //    LoadGameoverLevel("GameOver");
        //}
    }
    // recive mesage/by referece to trigger loading of level by using int information
    public void SceenManagement(int refLevel)
    {
        SceneManager.LoadScene("Level " + refLevel);
    }
    // this gets triggered on the begining of the game opening
    public void StartLoad()
    {

    }
    // at the end of a level it triggeres the save calculation
    // triggered by end of level.
    public void TriggerSave()
    {
        // check if the curent level is higher of the saaved level to trigger save. otherwhise do nothinng when weplaying older levels
        if (Curent_Level > HL_SaveLoad.instance.LevelTotal())
        {
            // thrigger save in save load script.
            HL_SaveLoad.instance.SetLevelTotal(Curent_Level);
            HL_SaveLoad.instance.FileSave();
        }
    }
    //---------------------------

    //reference for other scripts
    public int Curent_Level_Ref()
    {
        return Curent_Level;
    }
    public void SerCurent_Level(int Set_Level)
    {
        Curent_Level = Set_Level;
    }
    public static void LoadGameoverLevel(string level)
    {
        print("i workedi workedi workedi workedi workedi workedi workedi workedi worked");
    SceneManager.LoadScene(level);
    }
        //leave for azlan-----------------------
        //public void OnLevelWasLoaded(int level)
        //{

        //}
        //list of all pcs in the game
        //add to GM for holding a list in a singleton for scene dependant access
        // public List<GameObject> allObjects = new List<GameObject>();
        //if(currentlyloaddedpc.isdead,scenemanager.loadscene(gameover);
        //
        //add class to each object you want to add in the scenes to add them to singleton list
        //public class GameObjectAutoAdd : MonoBehaviour
        //{
        //    void Awake()
        //    {
        //        //Add this GameObject to List when it is created
        //        GameObjectManager.instance.allObjects.Add(gameObject);
        //    }

        //    void OnDestroy()
        //    {
        //        //Remove this GameObject from the List when it is about to be destroyed
        //        GameObjectManager.instance.allObjects.Remove(gameObject);
        //    }
        //}
        //------------------
        // foreach(gameobject go in allobject)
        //{
        // if(go.scene==scenemanager.getactivescene())
        //{
        //currentlyloadedpc=go;
        //break
        //}
        //------------------------}
        }
