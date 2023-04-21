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
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IncreaseHealth(heal);
        SaveData.Instance.Inventory.RemoveItem(this);
    }
}
