using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderScript : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = SoundManager.Instance.GetVolume();
    }

    public void UpdateVolume()
    {
        AudioListener.volume = volumeSlider.value;
        SoundManager.Instance.SetVolume(volumeSlider.value);
    }
}
