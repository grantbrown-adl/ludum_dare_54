using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    private static TimeManager _instance;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] private bool _isGameOver;
    [SerializeField] private float _currentTimeScale;
    [SerializeField] private bool _cheatsEnabled = false;
    [SerializeField] private int _cheatEnabler;
    [SerializeField] private TextMeshProUGUI _timeScaleDisplay;

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public static TimeManager Instance { get => _instance; set => _instance = value; }
    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }
    public bool CheatsEnabled { get => _cheatsEnabled; set => _cheatsEnabled = value; }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        _isGameOver = false;
        // Always unpause on start
        _currentTimeScale = 1.0f;
        Time.timeScale = _currentTimeScale;
        if (IsPaused) IsPaused = false;
        _cheatEnabler = 0;
        _cheatsEnabled = false;
    }
    private void Update()
    {
        if(_timeScaleDisplay != null) _timeScaleDisplay.text = $"{_currentTimeScale:n2}x";
        if (isPaused)
        {
            if (!_isGameOver) _pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pausePanel.SetActive(false);
            Time.timeScale = _currentTimeScale;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            _cheatEnabler++;
            if (_cheatEnabler > 3)
            {
                _cheatsEnabled = !_cheatsEnabled;
                _cheatEnabler = 0;
            }
        }
    }

    public void IncrementTimeScale()
    {
        _currentTimeScale *= 2;
        //if (_currentTimeScale > 8 && !ResourceManager.Instance.IsDebug) _currentTimeScale = 8;
        if (_currentTimeScale > 100) _currentTimeScale = 100;
    }

    public void DecrementTimeScale()
    {
        _currentTimeScale /= 2;
        if (_currentTimeScale < 0.25f) _currentTimeScale = 0.25f;
    }

}
