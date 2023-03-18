using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatController : MonoBehaviour
{
    [SerializeField]
    private float startingPlaybackSpeed = 1.0f;

    void Start()
    {
        GetComponent<AudioSource>().pitch = startingPlaybackSpeed;
    }

    void Update()
    {
        float timeElapsed = Time.time;
        float newPlaybackSpeed = startingPlaybackSpeed + (timeElapsed / 30.0f) * 0.15f;
        GetComponent<AudioSource>().pitch = newPlaybackSpeed;
    }
}
