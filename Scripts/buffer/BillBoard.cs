using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    private Transform lockRotation;
    // Start is called before the first frame update
    void Start()
    {
        lockRotation = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.LookAt(transform.position+lockRotation.forward);
    }
}
