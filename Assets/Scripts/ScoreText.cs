using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _highScore;
    [SerializeField] GameObject _scorePanel;

    private void Update()
    {
        _scorePanel.SetActive(GameHandler.Instance.ShowScore);

        _score.text = $"{ScoreManager.Instance.Score}";
        _highScore.text = $"{ScoreManager.Instance.HighScore}";
    }
}
