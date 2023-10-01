using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { get => _instance; set => _instance = value; }
    public int PlayerHealth { get => _playerHealth; set => _playerHealth = value; }
    public bool PlayerDead { get => _playerDead; set => _playerDead = value; }

    [Header("View Variables")]
    [SerializeField] private int _playerHealth;
    [SerializeField] private bool _playerDead;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if(_playerHealth <= 0) PlayerHealth = 3; 
        _playerDead = false;
    }
}
