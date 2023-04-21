using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCave : MonoBehaviour
{
    public delegate void EndRun(bool dead);
    public static event EndRun onRunEnded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onRunEnded?.Invoke(false);
        }
    }
}
