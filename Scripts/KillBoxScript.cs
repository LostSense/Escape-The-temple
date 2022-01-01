using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxScript : MonoBehaviour
{
    public Vector3 resetPosition;
    public bool useFullVector = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (useFullVector)//if use full vector - its sets player to vector position
            {
                other.gameObject.transform.position = resetPosition;
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; 
            }
            else
            {
                other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, resetPosition.y, other.gameObject.transform.position.z);
                other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
