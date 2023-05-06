using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        AudioListener.volume = SaveData.Instance.masterVolume;
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
        SaveData.Instance.masterVolume = value;
        SaveLoad.Save(SaveData.Instance);
    }
}
