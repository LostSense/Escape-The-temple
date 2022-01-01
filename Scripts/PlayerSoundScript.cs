using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour
{
    private SoundManager _sm;
    private float timer = 0f;
    private bool didSound = false;

    void Start()
    {
        _sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void FixedUpdate()
    {
        CheckIfObjectDidSound();
    }

    private void CheckIfObjectDidSound()
    {
        if (didSound)
        {
            StartCountDelayTime();
        }
    }

    private void StartCountDelayTime()
    {
        float delayTimeForNextSound = 2f;
        timer += Time.fixedDeltaTime;
        if (timer > delayTimeForNextSound)
        {
            didSound = false;
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckIfObjectHaveReferenceToSoundManager();
        MakeASound();
    }

    private void MakeASound()
    {
        if (!didSound)
        {
            _sm.PlayRandomHitCLip();
            didSound = true;
        }
    }

    private void CheckIfObjectHaveReferenceToSoundManager()
    {
        if (_sm == null)
        {
            _sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        }
    }
}
