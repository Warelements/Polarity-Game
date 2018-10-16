using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HL_SaveLoad : MonoBehaviour {

    public static HL_SaveLoad instance;
    protected int int_LevelsCompletel;

    // Use this for initialization
    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
        if (File.Exists(Application.persistentDataPath + "/save.data"))
        {
            print("file Exits");
            LoadData();

        }
        else if (!File.Exists(Application.persistentDataPath + "/save.data"))
        {
            print("file doese not Exits");
            FileCreate();
        }
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void FileCreate()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save.data", FileMode.Create);

        PlayerData data = new PlayerData();

        // here is the level u start as
        data.Saved_levelsCompleted = 1;
        int_LevelsCompletel = 1;
        //----
        bf.Serialize(stream, data);
         
        stream.Close();
    }

    public void FileSave()
    {
        print("saving");
        print(int_LevelsCompletel);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save.data", FileMode.Create);

        PlayerData data = new PlayerData();

        // saved data here
        data.Saved_levelsCompleted = int_LevelsCompletel;

        //-----------------
        bf.Serialize(stream, data);
        stream.Close();

    }
    public void LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save.data", FileMode.Open);

        PlayerData data = bf.Deserialize(stream) as PlayerData;
        stream.Close();

        // load the save data below

        int_LevelsCompletel = data.Saved_levelsCompleted;
    }

    // allow int to be accesed by other scripts
    public int LevelTotal()
    {
        return int_LevelsCompletel;
    }
    public void SetLevelTotal(int newTotal)
    {
        int_LevelsCompletel = newTotal;
    }
    //--------------------
}


[Serializable]
public class PlayerData
{
    public int Saved_levelsCompleted;

}