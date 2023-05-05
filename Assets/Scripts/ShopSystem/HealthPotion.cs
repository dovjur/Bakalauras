using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Item/HealthPotion", order = 1), Serializable]
public class HealthPotion : ItemObject
{
    public int heal;

    public override void Use()
    {
        RunManager.Player.IncreaseHealth(heal);
        SaveData.Instance.inventory.RemoveItem(this);
    }
}
