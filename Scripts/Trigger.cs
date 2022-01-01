using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public string targetTag = "";
    public float triggerDistance = 5f;
    public GameObject target;
    public UnityEvent enterTrigger;
    public UnityEvent exitTrigger;
    public bool useColliderAsTrigger = false;
    public bool activateOnce = false;
    private bool wasActivated = false;
    
    void Start()
    {
        GetReferenceToSelectedTag();
        CheckIfEventsAreNullAndInicializeThem();
    }
    private void GetReferenceToSelectedTag()
    {
        if (targetTag != "")
            target = GameObject.FindGameObjectWithTag(targetTag);
    }

    private void CheckIfEventsAreNullAndInicializeThem()
    {
        if (enterTrigger == null)
            enterTrigger = new UnityEvent();
    }


    void FixedUpdate()
    {
        CheckIfFlagWasntUpAndActivateEvents();
    }

    private void CheckIfFlagWasntUpAndActivateEvents()
    {
        if (!wasActivated)
        {
            try
            {
                float distanceToObject = (target.transform.position - transform.position).magnitude;
                if (triggerDistance > distanceToObject && !useColliderAsTrigger)
                {
                    ActivateEnterEvents();
                }
            }
            catch
            {
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckIfEnteredTargetAreCorrect(other))
        {
            ActivateEnterEvents();
        }
    }

    private bool CheckIfEnteredTargetAreCorrect(Collider other)
    {
        bool tagAreCorrect = (other.tag == targetTag && useColliderAsTrigger);
        bool hashCodeAreCorrect = (other.gameObject.GetHashCode() == target.GetHashCode());
        return ((tagAreCorrect || hashCodeAreCorrect) && useColliderAsTrigger && !wasActivated);
    }

    private void ActivateEnterEvents()
    {
        enterTrigger.Invoke();
        if (activateOnce)
            wasActivated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (CheckIfEnteredTargetAreCorrect(other))
        {
            exitTrigger.Invoke();
        }
    }
}
