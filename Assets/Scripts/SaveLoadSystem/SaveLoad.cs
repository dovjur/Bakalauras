using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem
{
    private static string filePath = Application.persistentDataPath + "/saves/" + "GetBackAlive.save";
    public static void Save(object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        FileStream file = File.Create(filePath);

        formatter.Serialize(file, saveData);

        file.Close();
    }

    public static object Load()
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(filePath, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", file);
            file.Close();
            return null;
        }
    }

}