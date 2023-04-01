using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem
{
    private static string filePath = Application.persistentDataPath + "/saves/" + "GetBackAlive.save";
    public static void Save(object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

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

        BinaryFormatter formatter = GetBinaryFormatter();

        FileStream file = File.Open(filePath, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch(Exception e)
        {
            Debug.LogErrorFormat("Failed to load file at {0}: {1}", file, e);
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        SurrogateSelector surrogateSelector = new SurrogateSelector();

        DictionarySerializationSurrogate<ItemObject,InventoryItem> dictionarySurrogate = new DictionarySerializationSurrogate<ItemObject, InventoryItem>();

        surrogateSelector.AddSurrogate(typeof(Dictionary<ItemObject, InventoryItem>),new StreamingContext(StreamingContextStates.All), dictionarySurrogate);

        formatter.SurrogateSelector = surrogateSelector;

        return formatter;
    }
}