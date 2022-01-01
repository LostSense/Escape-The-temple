using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;
public class Rotateable : MonoBehaviour
{
    public WorkingType workingType = WorkingType.Single;
    public Vector3[] eulerPositions;
    public int positionNumber;
    public float timeOfAnimation = 2f;
    public float accuracy = 0.05f; //accuracy of moving object
    public bool continiusRotation = false;
    public Vector3 speedOfAnimation;
    public bool singleActivation = false;//this is flag for single activasion
    private bool wasActivated = false;//this is variable for check, if animation was activated
    private bool makeAnimation = false;

    private float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(continiusRotation)
        {
            if (speedOfAnimation != null)
                makeAnimation = true;
            else
            {
                speedOfAnimation = Vector3.zero;
                makeAnimation = true;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(makeAnimation && counter < timeOfAnimation)
        {
            transform.Rotate(speedOfAnimation * Time.fixedDeltaTime);
            if(!continiusRotation)
            {
                counter += Time.fixedDeltaTime;
            }
            
        }
        else
        {
            if(makeAnimation)
            {
                switch (workingType)//here we check what is operation mode and working threw this
                {
                    case WorkingType.Cycle:
                        {
                            positionNumber++;
                            if (positionNumber >= eulerPositions.Length)
                                positionNumber = 0;
                        }
                        break;
                    case WorkingType.Random:
                        {
                            positionNumber = Random.Range(0, eulerPositions.Length);
                        }
                        break;
                }
            }
            makeAnimation = false;
            
        }
    }

    public void RotateToAngle()
    {
        if (!wasActivated)
        {
            GetAngleSpeed();
            makeAnimation = true;
            counter = 0f;
        }
        if (singleActivation)//logic for single activation
            wasActivated = true;
    }

    private void GetAngleSpeed()
    {
        speedOfAnimation = eulerPositions[positionNumber] / timeOfAnimation;
    }
}
