using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance { get => _instance; set => _instance = value; }
    public int Score { get => _currentScore; set => _currentScore = value; }
    public int HighScore { get => _highScore; set => _highScore = value; }

    [SerializeField] private int _currentScore;
    [SerializeField] private int _highScore;


    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        else
        {
            _instance = this;
        }
        _currentScore = 0;       
    }

    private void Start()
    {
        LoadHighScore();
    }

    private void Update()
    {
        if (Score == 5)
        {
            GameHandler.Instance.ShowProjectileExplosions = true;
            DialogueManager.Instance.StartDialogue(dialogueIndex: 10);
        }

        if (Score == 10)
        {
            GameHandler.Instance.ShowScore = true;
            GameHandler.Instance.RenderTrails = true;
            DialogueManager.Instance.StartDialogue(dialogueIndex: 7);
        }

        if(_currentScore > _highScore)
        {
            PlayerPrefs.SetInt("high_score", _currentScore);
            _highScore = GetHighScore();
        }
    }

    void LoadHighScore()
    {
        if (!PlayerPrefs.HasKey("high_score"))
        {
            PlayerPrefs.SetInt("high_score", 0);
            GetHighScore();
        }
        else
        {
            GetHighScore();
        }
    }

    public int GetHighScore()
    {
        _highScore = PlayerPrefs.GetInt("high_score");
        return _highScore;
    }

}


