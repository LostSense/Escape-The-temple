using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit_FPS : MonoBehaviour
{
    public int fps = 60; // Sets The Limit for the FPS can be set in editor


    // Awake is called when the instance is being loaded
    void Awake()
    {
        // #if UNITY_EDITOR // Used to limit only in editor
        QualitySettings.vSyncCount = 0;     // VSync must be disabled
        Application.targetFrameRate = fps;  // Set Game FPS to target FPS
        // #endif
    }

}