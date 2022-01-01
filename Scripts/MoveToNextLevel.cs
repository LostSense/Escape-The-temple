using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToNextLevel : MonoBehaviour
{
    public string levelName = "";
    public GameObject loadingScreen;
    public Text loadPercent;
    //private AsyncOperation scene;
    public void LoadNewScene()
    {
        if(levelName != "")
        {
            //Time.timeScale = 0f;
            if (loadingScreen != null)
                loadingScreen.SetActive(true);
            StartCoroutine(LoadSceneAsynchoniosly());
        }
    } 

    IEnumerator LoadSceneAsynchoniosly()
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);

        while(!scene.isDone)
        {
            if(loadPercent!=null)
                loadPercent.text = (scene.progress * 100).ToString() + "%";
            yield return null;
        }
    }
}
