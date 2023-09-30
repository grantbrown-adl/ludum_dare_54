using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            LoadPrefs();
        }
        else
        {
            LoadPrefs();
        }
    }

    public void UpdateVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    void LoadPrefs()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
    }
}
