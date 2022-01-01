using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour
{
    private GameObject loseGameCanvas;
    private InputManager _im;
    private GameManager _gm;
    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        loseGameCanvas = GameObject.FindGameObjectWithTag("PauseMenu").GetComponent<PauseMenuManager>().loseGameCanvas;
        _im = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseGameAction()
    {
        _gm.PauseGame();
        loseGameCanvas.SetActive(true);
        _im.EnableMouse();
    }
}
