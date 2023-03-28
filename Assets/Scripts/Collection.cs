using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Collection : ScriptableObject
{
    public string collectionName;
    public List<LootCard> collectionCards;
}
