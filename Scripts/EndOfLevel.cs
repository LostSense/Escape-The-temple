using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    //this class i did because of wrong architecture. And for more simple work with levels i MUST do this script.
    private GameManager _gm;
    private GameObject _canvasOfNextLevel;
    private InputManager _im;
    public bool severalTriggersForEndOfLevel = false;
    public bool[] triggers;
    
    void Start()
    {
        GetAllManagers();
    }

    private void GetAllManagers()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _canvasOfNextLevel = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuManager>().endOfGameCanvas;
        _im = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
    }

    public void EndGameAction()
    {
        _gm.PauseGame();
        GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuManager>().OpenNextLevel();
        _canvasOfNextLevel.SetActive(true);
        _im.EnableMouse();
    }

    public void SetTriggerActive(int trigger)
    {
        triggers[trigger] = true;
        CheckForEndOfGame();
    }

    public void SetTriggerDisabled(int trigger)
    {
        triggers[trigger] = false;
    }

    private void CheckForEndOfGame()
    {
        bool canEndThisGame = true;
        for (int i = 0; i < triggers.Length; i++)
        {
            if (!triggers[i])
                canEndThisGame = false;
        }
        if (canEndThisGame)
        {
            EndGameAction(); 
        }
    }
}
