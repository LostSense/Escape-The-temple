using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneOrbitRotation : MonoBehaviour
{

    public float mouseSensitivity = 10f; // Sets the mouse sensetivety.
    public float rotationLimitAngle = 10f;  // Limits the maximum rotation the plane could spin in each direction
    //private Vector2 rotate;                 // Stores the mouse location variables.
    private KeyboardAndMouse controller;
    private float xAngle = 0f;
    private float zAngle = 0f;

    void Start()
    {
        AddActionToInputController();
    }

    private void AddActionToInputController()
    {
        controller = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>().controller;
        controller.Player.MouseMove.started += ctnx => RotateWorld(ctnx.ReadValue<Vector2>());
    }

    private void RotateWorld(Vector2 rotation)
    {
        rotation = CheckForLimitsAndSetRotationVector(rotation);
        rotation = rotation.normalized;
        Vector3 position = transform.rotation.eulerAngles;
        Vector3 rotationAngle = new Vector3(position.x + rotation.y * mouseSensitivity, 0, position.z - rotation.x * mouseSensitivity);
        gameObject.transform.rotation = Quaternion.Euler(rotationAngle);
        StoreInformationOfLocalWorldPosition(rotation);
    }

    private void StoreInformationOfLocalWorldPosition(Vector2 rotation)
    {
        xAngle += rotation.x * mouseSensitivity;
        zAngle += rotation.y * mouseSensitivity;
    }

    private Vector2 CheckForLimitsAndSetRotationVector(Vector2 rotation)
    {
        if (rotation.x > 0 && xAngle >= rotationLimitAngle)
        {
            rotation.x = 0;
        }
        if (rotation.x < 0 && -xAngle >= rotationLimitAngle)
        {
            rotation.x = 0;
        }
        if (rotation.y > 0 && zAngle >= rotationLimitAngle)
        {
            rotation.y = 0;
        }
        if (rotation.y < 0 && -zAngle >= rotationLimitAngle)
        {
            rotation.y = 0;
        }

        return rotation;
    }

    public void SetSensetivity(float sensetivityValue)
    {
        mouseSensitivity = sensetivityValue/3;
    }
}
