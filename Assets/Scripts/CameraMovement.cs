using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float smoothing;

    private Transform target;
    public Vector2 maxPosition;

    private void Update()
    {
        if (target == null)
        {
            target = GameManager.Player.transform;
        }
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,target.position.y,transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,0,maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,0,maxPosition.y);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothing);
        }
    }
}
