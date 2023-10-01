using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private static GameHandler _instance;
    public static GameHandler Instance { get => _instance; set => _instance = value; }

    [Header("View Variables")]
    [SerializeField] private bool _renderTrails;
    [SerializeField] private bool _showRunes;
    [SerializeField] private bool _showCube;
    [SerializeField] private bool _autoFire;
    [SerializeField] private bool _showRuneExplosions;
    [SerializeField] private bool _showProjectileExplosions;
    [SerializeField] private bool _showHealth;
    [SerializeField] private bool _allowPlayerGravity;
    [SerializeField] private bool _showScore;
    [SerializeField] private int _healthSpawnRate;

    public bool ShowCube { get => _showCube; set => _showCube = value; }
    public bool ShowRunes { get => _showRunes; set => _showRunes = value; }
    public bool RenderTrails { get => _renderTrails; set => _renderTrails = value; }
    public bool AutoFire { get => _autoFire; set => _autoFire = value; }
    public bool ShowRuneExplosions { get => _showRuneExplosions; set => _showRuneExplosions = value; }
    public bool ShowProjectileExplosions { get => _showProjectileExplosions; set => _showProjectileExplosions = value; }
    public bool ShowHealth { get => _showHealth; set => _showHealth = value; }
    public bool AllowPlayerGravity { get => _allowPlayerGravity; set => _allowPlayerGravity = value; }
    public int HealthSpawnRate { get => _healthSpawnRate; set => _healthSpawnRate = value; }
    public bool ShowScore { get => _showScore; set => _showScore = value; }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        ClearState();
    }

    private void ClearState()
    {
        _renderTrails = false;
        _showRunes = false;
        _showCube = false;
        _autoFire = false;
        _showRuneExplosions = false;
        _showProjectileExplosions = false;
        _showHealth = false;
        _allowPlayerGravity = false;
        _healthSpawnRate = 15;
        _showScore = false;
    }
}
