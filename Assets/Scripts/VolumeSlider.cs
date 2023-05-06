using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider.value = SaveData.Instance.masterVolume;
        SoundManager.Instance.ChangeMasterVolume(slider.value);
        slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
    }
}
