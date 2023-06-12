using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Script for the moving platforms in the second temple
    [SerializeField] private float movingSpeed = 10f;
    [SerializeField] private float movingDistance = 10f;
    [SerializeField] private float startOffset = 0f;
    [SerializeField] private int moveAxis = 0; // 0 = x, 1 = z
    [SerializeField] private int startDirection = 1;
    [SerializeField] private float turnWaitTime = 0.2f;
    private float timeToEndWait = 0f;
    private int currentDirection = 1;
    private Vector3 moveVector = Vector3.zero;
    private Vector3 startPosition = Vector3.zero;
    private Transform playerTransform = null;

    private void Awake()
    {
        timeToEndWait = 0f;
        startPosition = transform.position;
        currentDirection = startDirection;
        DetermineMoveVector();
        SetToOffsetPosition();
        playerTransform = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(timeToEndWait <= 0)
        {
            if ((startPosition - transform.position).magnitude > movingDistance)
            {
                SetToMaxPosition();
                ChangeDirection();
            }
            Move();
        }
        else
        {
            timeToEndWait -= Time.fixedDeltaTime;
        }
        

    }

    /**
     * Changes the direction of the movement
     */
    private void ChangeDirection()
    {
        currentDirection *= -1;
        timeToEndWait = turnWaitTime;
    }

    /**
     * Determines the moveVector depending on the axis of the movement
     */
    private void DetermineMoveVector()
    {
        if(moveAxis == 0)
        {
            moveVector = Vector3.right;
        }
        else
        {
            moveVector = Vector3.forward;
        }
    }

    /**
     * Sets the position of the platform to the maximum position in the moving direction
     */
    private void SetToMaxPosition()
    {
        transform.position = startPosition + moveVector * (movingDistance - 0.001f) * currentDirection;
    }

    /**
     * Sets the position of the platform to the startoffset
     */
    private void SetToOffsetPosition()
    {
        transform.Translate(moveVector * startOffset * startDirection);
    }

    /**
     * Moves the platform
     */
    private void Move()
    {
        transform.Translate(moveVector * movingSpeed * Time.deltaTime * currentDirection);
        if(playerTransform != null)
        {
            playerTransform.Translate(moveVector * movingSpeed * Time.deltaTime * currentDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTransform = null;
        }
    }


}
