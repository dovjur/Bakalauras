using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI collectionTitle;
    [SerializeField]
    private GameObject contentPanel;

    public void SetName(string name)
    {
        collectionTitle.text = name;
    }
}
