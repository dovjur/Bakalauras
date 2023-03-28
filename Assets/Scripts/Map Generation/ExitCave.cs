using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCave : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BoxCollider2D>().enabled = false;
            RunData.current.EndOfRun();
            SceneLoadManager.instance.LoadMenu();
        }
    }
}
