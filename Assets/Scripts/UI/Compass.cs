using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    private Vector3 position;
    private Vector3 destination;

    void Update()
    {
        position = RunManager.Player.transform.position;
        float angle = Mathf.Atan2(destination.y - position.y, destination.x - position.x) * 180 / Mathf.PI;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 1f);
    }

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
    }
}
