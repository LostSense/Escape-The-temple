using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    
    private KeyboardAndMouse controller;
    public GameObject mainMenu;
    private bool SomthingActive = false;
    private List<GameObject> opennedObjects;
    private InputManager _iM;
    private GameManager _gm;

    public Slider volumeSlider;
    public Slider sensetiveSlider;
    public TMP_Text volumeLabel;
    public TMP_Text mouseSensetivityValue;
    private OptionsSafeFile osf;
    private string optionsFileName = "options.ini";
    private IOManager iOManager;
    public GameObject loseGameCanvas;
    public GameObject endOfGameCanvas;
    private SoundManager _sm;
    public GameObject soundManagerPrefab;
    private PlaneOrbitRotation por;
    public GameObject[] loadButtons;
    //TODO: Add input manager
    // Start is called before the first frame update
    void Start()
    {
        opennedObjects = new List<GameObject>();
        por = GameObject.FindGameObjectWithTag("Platform").GetComponent<PlaneOrbitRotation>();
        //TODO: Add action in to input manager
        _iM = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        try
        {
            _sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        }
        catch (System.Exception)
        {
            Instantiate(soundManagerPrefab);
            _sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        }
        controller = _iM.controller;
        controller.Player.EscButton.performed += cntx => PressEscButt();
        loseGameCanvas.SetActive(false);
        endOfGameCanvas.SetActive(false);
        iOManager = new IOManager();
        GetOptionsFile();
        ApplyOptions();
    }

    private void PressEscButt()
    {
        try
        {
            if (!SomthingActive)
            {
                if (mainMenu.activeInHierarchy)
                {
                    CloseMenu();
                }
                else
                {
                    OpenMenu();
                }
            }
            else
            {
                //close active menu
                CloseActiveMenu();
            }
        }
        catch (System.Exception)
        {

        }
    }
    private void OpenMenu()
    {
        mainMenu.SetActive(true);
        //TODO: pause game in GameManager
        _iM.EnableMouse();
        _gm.PauseGame();
    }

    public void CloseMenu()
    {
        mainMenu.SetActive(false);
        //TODO: resume game in GameManager
        _iM.DisableMouse();
        _gm.UnpauseGame();
    }

    public void CloseActiveMenu()
    {
        int lenght = opennedObjects.Count;
        if(lenght == 1)
        {
            opennedObjects[0].SetActive(false);
            SomthingActive = false;
        }
        else
        {
            opennedObjects[lenght - 1].SetActive(false);
        }
        opennedObjects.RemoveAt(lenght - 1);
    }

    public void AddOpenedMenu(GameObject obj)//main logic: if we open somthing - we add this to list, and close one by one.
    {
        opennedObjects.Add(obj);
        SomthingActive = true;
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
        por.SetSensetivity(osf.sensetive);
    }

    private void GetOptionsFile()//here we try to get options file. If we don't have one - create new.
    {
        object getFile;
        getFile = iOManager.ReadFile(optionsFileName);
        if (getFile != null)
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
        por.SetSensetivity(osf.sensetive);
        ActivateAllButtons();
    }

    public void SafeChangedValues()//applays every time when options are closed
    {
        //Debug.Log("I save here: " + iOManager.GetSystemPath());
        iOManager.SafeFile(optionsFileName, osf);
    }

    public void LoadMainMenu() //this method i write because i must do object for easy to use in level building.
    {
        _gm.LoadLevel("MainMenu");
    }

    public void LoadNextLevel()
    {
        osf.isLastLevelCompleate = _gm.CheckIfLastLevel();
        SafeChangedValues();
        _iM.DestroyManager();
        _gm.LoadNextLevel();
    }

    public void RestartLevel()
    {
        _iM.DestroyManager();
        _gm.RestartLevel();
    }

    public void OpenNextLevel()
    {
        UnityEngine.SceneManagement.Scene sc = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (osf.level <= sc.buildIndex && sc.buildIndex+1 < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings)
        {
            osf.level = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        }
        SafeChangedValues();
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
        _gm.LoadLevel(levelNumber);
    }

    public void ExitGame()
    {
        _gm.CloseApplication();
    }
}
