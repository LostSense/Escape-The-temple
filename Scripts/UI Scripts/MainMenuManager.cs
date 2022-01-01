using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName = "";
    public TMP_Text volumeLabel;
    public TMP_Text mouseSensetivityValue;
    public Slider volumeSlider;
    public Slider sensetiveSlider;
    private OptionsSafeFile osf;
    private string optionsFileName = "options.ini";
    private IOManager iOManager;
    private SoundManager _sm;
    private PlaneOrbitRotation por;
    public GameObject[] loadButtons;
    public GameObject screenOfEndOfGame;


    // Start is called before the first frame update
    void Start()
    {
        _sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        //por = GameObject.FindGameObjectWithTag("Platform").GetComponent<PlaneOrbitRotation>();
        iOManager = new IOManager();
        GetOptionsFile();
        ApplyOptions();
        
        DontDestroyOnLoad(_sm);
    }


    public void CloseGame()
    {
        Application.Quit();
    }


    public void StartGame()
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    public void ChangeVolumeValue(Slider slider)
    {
        volumeLabel.text = string.Format("{0, 0:P0}", slider.value);
        osf.volume = slider.value;
        _sm.audio.volume = osf.volume;
    }

    public void ChangeSensetivityValue(Slider slider)
    {
        mouseSensetivityValue.text = string.Format("{0, 0:P0}", slider.value);
        osf.sensetive = slider.value;
    }

    private void GetOptionsFile()//here we try to get options file. If we don't have one - create new.
    {
        object getFile;
        getFile = iOManager.ReadFile(optionsFileName);
        if(getFile != null)
        {
            try
            {
                osf = getFile as OptionsSafeFile;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                string path = iOManager.GetSystemPath();
                //must remove file and create a new one
                iOManager.DeleteFile(path + "/" + optionsFileName);
                CreateOSFFile();
            }
        }
        else
        {
            CreateOSFFile();
        }
    }

    private void CreateOSFFile()
    {
        string path = iOManager.GetSystemPath();
        osf = new OptionsSafeFile(1f, 1f, path);
    }

    private void ApplyOptions()//this method must apply all options to game. Right now he applay only at start of game, must to separate it
    {
        volumeSlider.value = osf.volume;
        ChangeVolumeValue(volumeSlider);
        sensetiveSlider.value = osf.sensetive;
        ChangeSensetivityValue(sensetiveSlider);
        ActivateAllButtons();
        //por.SetSensetivity(osf.sensetive);
        ActivateEndOfGameScreen();
    }

    public void SafeChangedValues()//applays every time when options are closed
    {
        //Debug.Log("I save here: " + iOManager.GetSystemPath());
        iOManager.SafeFile(optionsFileName, osf);
    }

    public void ActivateAllButtons()
    {
        //Debug.Log($"{osf.level}", this);
        for (int i = 0; i < osf.level; i++)
        {
            loadButtons[i].SetActive(true);
        }
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadSceneAsync(levelNumber, LoadSceneMode.Single);
    }

    public void ActivateEndOfGameScreen()
    {
        if (osf.isLastLevelCompleate)
        {
            osf.isLastLevelCompleate = false;
            screenOfEndOfGame.SetActive(true);
            SafeChangedValues(); 
        }
    }
}
