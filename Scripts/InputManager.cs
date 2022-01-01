using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public KeyboardAndMouse controller;
    void Awake()
    {
        controller = new KeyboardAndMouse();
        controller.Player.Enable();
        DisableMouse();
    }

    public void EnableMouse()
    {
        Cursor.lockState = CursorLockMode.None;          
        Cursor.visible = true;
        controller.Player.MouseMove.Disable();
    }

    public void DisableMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;          
        Cursor.visible = false;
        controller.Player.MouseMove.Enable();

    }

    public void DestroyManager()
    {
        controller.Disable();
        controller.Dispose();
    }
}
