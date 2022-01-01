using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableElements : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

}
