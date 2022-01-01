using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource audio;
    public AudioClip[] mainClips;
    public AudioClip[] hitSounds;
    
    private void Awake()
    {
        CheckIfThisSingleInstanceAndDestroyIfNot();
    }

    private void CheckIfThisSingleInstanceAndDestroyIfNot()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (!audio.isPlaying)
            PlayRandomMainClip();
    }

    public void PlayRandomMainClip()
    {
        audio.clip = mainClips[Random.Range(0, mainClips.Length)];
        audio.Play();
    }

    public void ChangeAudioVolume(float volumeValue)
    {
        audio.volume = volumeValue;
    }

    public void PlayRandomHitCLip()
    {
        audio.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
    }
}
