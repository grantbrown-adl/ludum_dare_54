using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCanvasScript : MonoBehaviour
{
    private void Start()
    {
        VolumeLoad();
        GameManagerScript.IsPaused = true;        
    }

    public void Unpause()
    {
        GameManagerScript.IsPaused = false;
    }

    void VolumeLoad()
    {
        if(!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            UpdateVolume();
        }
        else
        {
            UpdateVolume();
        }
    }

    public void UpdateVolume()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }
}
