using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionsSafeFile
{
    public float volume;
    public float sensetive;
    public string systemPath;
    public int level;
    public bool isLastLevelCompleate = false;
    public OptionsSafeFile(float newVolume, float newSensetive, string path)
    {
        volume = newVolume;
        sensetive = newSensetive;
        systemPath = path;
        level = 1;
    }
}
