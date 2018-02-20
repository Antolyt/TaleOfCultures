﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Savegame : MonoBehaviour
{
    public static Savegame savegame;
    private static string saveGamePath;        // Requieres set in Awake
    public static SavegameData savegameData;
    //public GameObject plants;

    void Awake()
    {
        saveGamePath = Application.persistentDataPath + "/Savegames/save01.dat";

        Debug.Log(saveGamePath);

        if (savegame == null)
        {
            DontDestroyOnLoad(gameObject);
            savegame = this;
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //LoadGame();
    }

    void Update()
    {

    }

    /// <summary>
    /// Save Settings
    /// </summary>
    public static void Save(GameObject plants)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveGamePath);

        SavegameData sd = new SavegameData();
        foreach(Plant plant in plants.GetComponentsInChildren<Plant>())
        {
            sd.plants.Add(plant.PlantToData());
        }

        bf.Serialize(file, sd);
        file.Close();

        Debug.Log("File Saved");
    }

    /// <summary>
    /// Load Settings
    /// </summary>
    public static void Load()
    {
        if (File.Exists(saveGamePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveGamePath, FileMode.Open);
            SavegameData sd = (SavegameData)bf.Deserialize(file);
            file.Close();

            savegameData = sd;
        }
        else
        {
            savegameData = new SavegameData();
        }
    }
}

[Serializable]
public class SavegameData
{
    public List<PlantData> plants;

    public SavegameData()
    {
        plants = new List<PlantData>();
    }
}