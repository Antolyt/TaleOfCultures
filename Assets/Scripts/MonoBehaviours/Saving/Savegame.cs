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

    void Awake()
    {
        saveGamePath = Application.persistentDataPath + "/Savegames/save01.dat";

        Debug.Log(saveGamePath);

        // make singleton
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

    /// <summary>
    /// Save Game
    /// </summary>
    public static void Save(GameObject plants, Inventory inventory, QuickSlots quickSlots)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveGamePath);

        savegameData = new SavegameData();

        // Save Plants
        foreach(Plant plant in plants.GetComponentsInChildren<Plant>())
        {
            savegameData.plants.Add(plant.PlantToData());
        }

        // Save Inventory
        savegameData.inventorySize = inventory.availableSize;
        for(int i = 0; i < inventory.availableSize; i++)
        {
            savegameData.inventoryItems[i] = inventory.inventoryItems[i].ToData();
        }

        // Save QuickSlots and References to inventory
        for(int i = 0; i < quickSlots.quickSlotItems.Length; i++)
        {
            savegameData.quickSlotItems[i] = quickSlots.quickSlotItems[i].ToData();
            savegameData.inventoryQuickSlotRef[i] = quickSlots.inventoryReference[i];
        }

        bf.Serialize(file, savegameData);
        file.Close();

        Debug.Log("File Saved");
    }

    /// <summary>
    /// Load Game
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

    // Inventory & Items
    public int inventorySize;
    public ItemData[] inventoryItems;
    public ItemData[] quickSlotItems;
    public int[] inventoryQuickSlotRef;

    //public ChestData[] chests;

    // NPC

    public SavegameData()
    {
        plants = new List<PlantData>();
        inventoryItems = new ItemData[20];
        quickSlotItems = new ItemData[4];
        inventoryQuickSlotRef = new int[4];
    }
}
