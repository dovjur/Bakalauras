using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBeatController : MonoBehaviour
{
    [SerializeField]
    private float startingPlaybackSpeed = 1.0f;

    private float pitch;
    void Start()
    {
        pitch = GetComponent<AudioSource>().pitch;
        pitch = startingPlaybackSpeed;
    }

    void Update()
    {
        float timeElapsed = Time.time;
        float newPlaybackSpeed = startingPlaybackSpeed + (timeElapsed / 30.0f) * 0.15f;
        pitch = newPlaybackSpeed;
    }
}
