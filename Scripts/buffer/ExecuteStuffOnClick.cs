using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteStuffOnClick : MonoBehaviour
{
    public UnityEvent myEvent;

    private void Awake()
    {
        if(myEvent == null)
        {
            myEvent = new UnityEvent();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            myEvent.Invoke();
        }
    }

    public void OnMouseDown()
    {
        myEvent.Invoke();
    }
}
