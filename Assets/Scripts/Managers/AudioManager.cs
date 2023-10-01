using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get => _instance; set => _instance = value; }
    public GameObject AudioSource { get => _audioSource; set => _audioSource = value; }

    [SerializeField] private GameObject _audioSource;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else
        {
            _instance = this;
        }
    }

    public void EnableAudioSource()
    {
        _audioSource.SetActive(true);
    }
}
