using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemObject", menuName = "Item/ItemObject", order = 1)]
public class ItemObject : ScriptableObject
{ 
    public int ID;
    public string label;
    public int price;
    public string description;
    public Sprite icon;

    public virtual void Use() { }
}
