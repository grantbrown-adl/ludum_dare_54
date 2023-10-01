using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance { get => _instance; set => _instance = value; }
    public int PlayerHealth { get => _playerHealth; set => _playerHealth = value; }
    public bool PlayerDead { get => _playerDead; set => _playerDead = value; }
    public bool InputAllowed { get => _inputAllowed; set => _inputAllowed = value; }

    [Header("View Variables")]
    [SerializeField] private int _playerHealth;
    [SerializeField] private bool _playerDead;
    [SerializeField] private bool _inputAllowed;
    [SerializeField] private int _collisions;

    private void Awake()
    {
        _collisions = 0;

        if (_instance != null && _instance != this) Destroy(gameObject);
        else
        {
            _instance = this;
        }

        PlayerHealth = 5; 
        _playerDead = false;
    }

    public void IncrementCollisions()
    {
        _collisions++;

        if (_collisions >= 3)
        {
            DialogueManager.Instance.StartDialogue(dialogueIndex: 8);
            GameHandler.Instance.ShowHealth = true;
        }
    }
}
