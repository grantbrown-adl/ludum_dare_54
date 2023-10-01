using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    private void Start()
    {
        //Pause the game
        TimeManager.Instance.IsGameOver = true;
        TimeManager.Instance.IsPaused = true;
        //Time.timeScale = 0;
    }
}
