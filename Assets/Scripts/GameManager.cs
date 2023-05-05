using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        SaveData.Instance = (SaveData)SaveLoad.Load();
    }
}
