using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Library;
public class Movable : MonoBehaviour
{
    public WorkingType workingType = WorkingType.Single;
    public int positionNumber = 0;//this i need to get number if we have not single rotation
    public Vector3[] positions;
    public float timeOfAnimation = 2f;
    public float accuracy = 0.05f; //accuracy of moving object
    public bool singleActivation = false;//this is flag for single activasion
    private bool wasActivated = false;//this is variable for check, if animation was activated
    private float distance;
    private Vector3 direction;
    private float speedOfAnimation;
    private bool makeAnimation = false;


    private void Start()
    {
        if(positions !=null)
        {
            if(positions.Length>0)
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[i] += transform.position;
                }
            }
        }
    }
    public void MoveToPosition()//this method move to selected position in script by default
    {
        if (!wasActivated)
        {
            GetDistance();
            speedOfAnimation = distance / timeOfAnimation;
            makeAnimation = true;

            
        }
        if (singleActivation)//logic for single activation
            wasActivated = true;
    }

    public void MoveToSelectedPosition(int position)//this method move to selected position threw method.
    {
        positionNumber = position;
        if (!wasActivated)
        {
            GetDistance();
            speedOfAnimation = distance / timeOfAnimation;
            makeAnimation = true;


        }
        if (singleActivation)//logic for single activation
            wasActivated = true;
    }

    private void GetDistance()
    {
        distance = (transform.position - positions[positionNumber]).magnitude;
    }

    private void FixedUpdate()
    {
        
        if(makeAnimation && distance > accuracy)
        {
            GetDirection();
            transform.Translate(direction * speedOfAnimation * Time.fixedDeltaTime, Space.Self);
            GetDistance();
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
                            if (positionNumber >= positions.Length)
                                positionNumber = 0;
                        }
                        break;
                    case WorkingType.Random:
                        {
                            positionNumber = Random.Range(0, positions.Length);
                        }
                        break;
                }
            }
            makeAnimation = false;
        }

    }

    private void GetDirection()
    {
        direction = (positions[positionNumber] - transform.position).normalized;
    }
}
