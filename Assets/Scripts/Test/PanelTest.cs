using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTest : MonoBehaviour
{
    public Transform character;
    public Vector3 offset;

    void Update()
    {
        transform.position = character.position + offset;
    }
}
