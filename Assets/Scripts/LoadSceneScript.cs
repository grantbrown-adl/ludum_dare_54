using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void LoadSceneName(string sceneName)
    {
        GameHandler.Instance.ClearState();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneIndex(int sceneIndex)
    {
        GameHandler.Instance.ClearState();
        SceneManager.LoadScene(sceneIndex);
    }
}
