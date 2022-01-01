using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0f;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("SoundManager"));
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void LoadLevel(string levelName)
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("SoundManager"));
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }

    public void LoadLevel(int levelNumber)
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("SoundManager"));
        SceneManager.LoadSceneAsync(levelNumber, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("SoundManager"));
        int amountOfScenes = SceneManager.sceneCountInBuildSettings;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene+1 < amountOfScenes)
        {
            SceneManager.LoadSceneAsync(currentScene + 1, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }

    public bool CheckIfLastLevel()
    {
        int amountOfScenes = SceneManager.sceneCountInBuildSettings;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene + 1 < amountOfScenes)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
