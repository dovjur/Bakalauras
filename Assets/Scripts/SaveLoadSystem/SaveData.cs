using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData instance;
    public static SaveData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveData();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public PlayerData player = new PlayerData();
    public Inventory inventory = new Inventory();
    public RunData runData = new RunData();
    public float masterVolume;
}