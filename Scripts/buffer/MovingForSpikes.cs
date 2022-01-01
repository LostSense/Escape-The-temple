using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingForSpikes : MonoBehaviour
{
    public Vector3[] directions;
    public int arrayIterator = 0;
    public float timeOfAnimation = 1f;
    private Vector3 speed = Vector3.zero;
    private bool startingMoveSpikes = false;
    private float couter = 0f;


    private void FixedUpdate()
    {
        if(startingMoveSpikes)
        {
            if(couter < timeOfAnimation)
            {
                MoveSpykes();
                couter += Time.fixedDeltaTime;
            }
        }
    }


    public void MoveToNextPosition()
    {
        speed = directions[arrayIterator] / (timeOfAnimation/Time.fixedDeltaTime);
        couter = 0f;
        startingMoveSpikes = true;
        arrayIterator++;
        if(arrayIterator >= directions.Length)
        {
            arrayIterator = 0;
        }
    }

    private void MoveSpykes()
    {
        transform.Translate(speed, Space.Self);
        
    }
}
