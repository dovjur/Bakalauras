using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Continue();
            }
        }
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Settings()
    {

    }

    public void MainMenu()
    {
        SceneLoadManager.instance.LoadMenu();
    }
}
