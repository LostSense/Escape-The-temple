using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spikes : MonoBehaviour
{
    #region For Unity Engine access variables
    public bool isActivated = false;
    public UnityEvent spikesEvent;
    public float timeForAcrivation = 5f; 
    #endregion
    private float timer = 0f;

    void Start()
    {
        InitiateEventsIfThemNull();
    }

    private void InitiateEventsIfThemNull()
    {
        if (spikesEvent == null)
        {
            spikesEvent = new UnityEvent();
        }
    }

    void FixedUpdate()
    {
        InvokeEventsIfFlagIsUp();
    }

    private void InvokeEventsIfFlagIsUp()
    {
        if (isActivated)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= timeForAcrivation)
            {
                timer = 0f;
                spikesEvent.Invoke();
            }
        }
    }

    public void SetActiveSpikes()
    {
        isActivated = true;
    }

    public void SetDisabledSpikes()
    {
        isActivated = false;
    }
}
