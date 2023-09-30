using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private static int scoreNeeded;
    [SerializeField] private int scoreView;
    [SerializeField] private static int happiness = 0;
    [SerializeField] private static bool isPaused;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas overlayCanvas;
    [SerializeField] private Canvas gameFinishedCanvas;

    public static int Happiness { get => happiness; set => happiness = value; }
    public static bool IsPaused { get => isPaused; set => isPaused = value; }
    public static int ScoreNeeded { get => scoreNeeded; set => scoreNeeded = value; }

    private void Start()
    {
        scoreNeeded = scoreView;
        //isPaused = false;
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isPaused)
        {
            overlayCanvas.gameObject.SetActive(false);
        }
        else
            overlayCanvas.gameObject.SetActive(true);

        if(Input.GetButtonDown("Fire1"))
        {
            //happiness++;
        }

        if(Input.GetButtonDown("Fire2"))
        {
            //happiness--;
        }

        if(isPaused)
        {
            Time.timeScale = 0.0f;
        }
        else
            Time.timeScale = 1.0f;

        slider.value = happiness;

        if(happiness >= 10)
        {
            happiness = 0;
            if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            else if(SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                isPaused = true;
                gameFinishedCanvas.gameObject.SetActive(true);
            }
        }
        else if (happiness <= -10)
        {
            isPaused = true;
            gameOverCanvas.gameObject.SetActive(true);
        }
    }

    public void LoadMenu()
    {
        happiness = 0;
        SceneManager.LoadScene(1);
        Destroy(this.gameObject);
    }

    public void ReloadLevel()
    {
        happiness = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex == 2)
            Destroy(this.gameObject);
        else
        {
            isPaused = false;
            gameOverCanvas.gameObject.SetActive(false);
        }
    }
}
