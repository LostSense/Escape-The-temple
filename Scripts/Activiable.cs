using Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Activiable : MonoBehaviour
{
    public float radiusOfActivation = 2f;
    public UnityEvent activateEvent;//this is event whose we throw in inspector
    public UnityEvent approachEvent; //this we need when you approach object
    public UnityEvent disaproachEvent; //this we need when you dissaproach
    public bool singleActivation = false;//this is flag for single activasion
    public bool useCollider = false; //here we use a collider instead of radius
    private bool targetEntered = false;//check for collider if target entered
    private bool wasActivated = false;//this is variable for check, if animation was activated
    public string targetTag = "Player";//tag for player, maybe later i will change it
    private float distance;//this is distance between 2 objects: target and myself
    private GameObject target;
    private bool animationEnabled = false;

    //TODO: add input system manager/script
    //private *inputManager* _im;

    // Start is called before the first frame update
    void Awake()
    {
        //here we check for EVENTS and create empty if they are null.
        if(activateEvent == null)
        {
            activateEvent = new UnityEvent();
        }
        if (approachEvent == null)
        {
            approachEvent = new UnityEvent();
        }
        if (disaproachEvent == null)
        {
            disaproachEvent = new UnityEvent();
        }
        target = GameObject.FindGameObjectWithTag(targetTag);//getting target
    }

    private void Start()
    {
        //TODO: get inputManager/script here from scene
    }

    private void FixedUpdate()
    {
        if(target != null)//cheking distance to target
        {
            distance = (transform.position - target.transform.position).magnitude;
        }
        if (!useCollider && target != null)
        {
            if (distance <= radiusOfActivation)  //if target close - we activate aproach events
            {
                if (!animationEnabled)
                {
                    OnApproachEnable();
                    animationEnabled = true;
                }

            }
            else //here we activate disable events.
            {
                if (animationEnabled)
                {
                    OnDisapproachDisable();
                    animationEnabled = false;
                }

            } 
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ExecuteAction()//this method is action Method
    {
        if (!wasActivated)
        {
            activateEvent.Invoke();
            if (singleActivation)//logic for single activation
                wasActivated = true; 
        }
    }

    private void OnApproachEnable()
    {
        approachEvent.Invoke();
        //TODO: Add lambda Expression to input manager. Method for execution - ExecuteAction().
    }

    private void OnDisapproachDisable()
    {
        disaproachEvent.Invoke();
        //TODO: remove lambda Expression to input Manager. Method for remove: ExecuteAction().
    }


    private void OnTriggerEnter(Collider other)
    {
        if(useCollider)
        {
            if(other.tag == targetTag)
            {
                targetEntered = true;
                OnApproachEnable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (useCollider)
        {
            if (other.tag == targetTag)
            {
                targetEntered = false;
                OnDisapproachDisable();
            }
        }
    }
}
