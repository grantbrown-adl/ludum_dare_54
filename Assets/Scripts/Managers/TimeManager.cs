using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    [SerializeField] private bool _isDialogShowing;
    private static TimeManager _instance;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _gameWonPanel;
    [SerializeField] private bool _isGameOver;
    [SerializeField] private bool _isGameWon;
    [SerializeField] private float _currentTimeScale;
    [SerializeField] private bool _cheatsEnabled = false;
    [SerializeField] private int _cheatEnabler;
    [SerializeField] private TextMeshProUGUI _timeScaleDisplay;
    [SerializeField] private float _startTime;
    [SerializeField] private float _elapsedTime;
    [SerializeField] private int _minutesElapsed;

    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public static TimeManager Instance { get => _instance; set => _instance = value; }
    public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }
    public bool CheatsEnabled { get => _cheatsEnabled; set => _cheatsEnabled = value; }
    public bool IsDialogShowing { get => _isDialogShowing; set => _isDialogShowing = value; }
    public bool IsGameWon { get => _isGameWon; set => _isGameWon = value; }
    public float StartTime { get => _startTime; set => _startTime = value; }
    public float ElapsedTime { get => _elapsedTime; set => _elapsedTime = value; }
    public int MinutesElapsed { get => _minutesElapsed; set => _minutesElapsed = value; }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        _isGameOver = false;
        _isGameWon = false;
        // Always unpause on start
        _currentTimeScale = 1.0f;
        Time.timeScale = _currentTimeScale;
        if (IsPaused) IsPaused = false;
        _cheatEnabler = 0;
        _cheatsEnabled = false;
    }

    private void Start()
    {
        _minutesElapsed = 0;
        _startTime = 0.0f;
        _startTime = Time.time;
    }

    private void Update()
    {
        _elapsedTime = Time.time - _startTime;

        _minutesElapsed = Mathf.FloorToInt(_elapsedTime / 60.0f);

        if (_isGameWon && !_isDialogShowing)
        {
            _gameWonPanel.SetActive(true);
            Time.timeScale = 0;
            return;
        }

        
        if (_timeScaleDisplay != null) _timeScaleDisplay.text = $"{_currentTimeScale:n2}x";
        if (isPaused)
        {
            if (!_isGameOver && !_isDialogShowing && !_isGameWon)
            { 
                _pausePanel.SetActive(true);
            }
            else if (_isGameOver && !_isDialogShowing && !_isGameWon)
            {
                _gameOverPanel.SetActive(true);

            }
            Time.timeScale = 0;
        }
        else
        {
            _gameOverPanel.SetActive(false);
            _pausePanel.SetActive(false);
            _gameWonPanel.SetActive(false);
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
        if (_currentTimeScale > 100) _currentTimeScale = 100;
    }

    public void DecrementTimeScale()
    {
        _currentTimeScale /= 2;
        if (_currentTimeScale < 0.25f) _currentTimeScale = 0.25f;
    }

    public IEnumerator StopTimeAfterDelay(float delay)
    {
        Debug.Log($"Starting StopTimeAfterDelay in {delay}");
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log("It happened");
        IsPaused = true;
    }
}
