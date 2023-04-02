using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField]
    private List<Collection> collections;
    [SerializeField]
    private GameObject slotPrefab;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void DisplayCollections()
    {
        for (int i = 0; i < collections.Count; i++)
        {

        }
    }
}
