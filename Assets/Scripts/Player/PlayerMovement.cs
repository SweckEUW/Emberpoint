using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    //Controller
    private CharacterController charController;

    private AudioSource[] sounds;

    //Player Model
    [Header("Model")]
    public Transform playerBody;

    //Running
    [Header("Running")]
    private Vector3 moveDirection;
    private Vector3 moveVector;
    private Vector2 animationDirection;
    [SerializeField] private float maxStartSpeed = 10f;
    [SerializeField] private float slowSpeed = 2f;
    private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 5f;
    private bool isMoving = false;
    private bool isSlowed = false;

    //Dash
    [Header("Dash")]
    [SerializeField] private VisualEffect dashEffect;
    [SerializeField] private int quickDashAmount = 2;
    [SerializeField] private float dashSpeed = 1000f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private DashCounter dashBar;
    private int availableDashes = 0;
    private float timeToEndDash;
    private float timeToNextDash = 0f;
    private bool isDashing = false;

    //Animation
    [Header("Animation")]
    public Animator animator;

    private Player.Player player;

    private bool stopped = false;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        sounds = GameObject.Find("Sounds").GetComponents<AudioSource>();

        player = GetComponent<Player.Player>();

        moveDirection = Vector3.zero;
        moveVector = Vector3.zero;
        animationDirection = Vector2.zero;
        isMoving = false;
        isSlowed = false;

        availableDashes = quickDashAmount;
        timeToEndDash = dashDuration;
        timeToNextDash = dashCooldown;
        isDashing = false;
        dashEffect.Stop();
        
        dashBar.SetValue(quickDashAmount);
    }
    
    /**
     * Changes the movement direction based on the key inputs
     */
    public void ChangeHorizontalMovement(Vector2 direction)
    {
        direction = Quaternion.AngleAxis(-45, Vector3.forward) * direction;
        moveDirection.x = direction.x;
        moveDirection.z = direction.y;

        if (direction.sqrMagnitude > 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    /**
     * Slows the player while he uses an item
     */
    public void SlowDown()
    {
        isSlowed = true;
    }

    /**
     * Speeds the player up after his item action is finished
     */
    public void SpeedUp()
    {
        isSlowed = false;
    }

    /**
     * Moves the player character in the move direction and accelerates depending on the vector
     */
    private void Move()
    {
        moveVector.x = Mathf.Lerp(moveVector.x,moveDirection.x,acceleration * Time.deltaTime);
        moveVector.z = Mathf.Lerp(moveVector.z, moveDirection.z, acceleration * Time.deltaTime);

        if(!isMoving && moveVector.sqrMagnitude < 0.1)
        {
            moveVector = Vector3.zero;
        }

        if (isSlowed)
        {
            maxSpeed = Mathf.Lerp(maxSpeed, slowSpeed, 0.3f);
        }
        else
        {
            maxSpeed = Mathf.Lerp(maxSpeed, maxStartSpeed, 0.3f);
        }
        charController.Move((moveVector - Vector3.up) * maxSpeed * Time.deltaTime);
    }

    /**
     * Triggers the dashing movement
     */
    public void Dash()
    {
        if (player.isDead) return;
        if(availableDashes > 0)
        {
            sounds[4].Play();
            isDashing = true;
            availableDashes--;
            dashBar.SetValue(availableDashes);
            timeToEndDash = dashDuration;
            dashEffect.Play();
        }
        
    }

    /**
     * Dashes into the looking direction of the player by increasing the move speed for the dash duration
     */
    private void DashMove()
    {
        if(timeToEndDash > 0)
        {
            charController.Move(playerBody.forward * dashSpeed * Time.deltaTime);
            timeToEndDash -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            dashEffect.Stop();
        }
    }


    /**
     * Reduces the time until the dash can be used again
     */
    private void ReduceDashCooldown()
    {
        if(availableDashes < quickDashAmount)
        {
            if (timeToNextDash > 0)
            {
                timeToNextDash -= Time.deltaTime;
            }
            else
            {
                availableDashes++;
                timeToNextDash = dashCooldown;
                
                dashBar.SetValue(availableDashes);
            }
        }
        
    }

    /**
     * Rotates the players body to the direction of the mouse
     */
    public void RotateBodyToMouse(Quaternion rotation)
    {
        
        playerBody.rotation = Quaternion.Slerp(playerBody.rotation, rotation, 0.15f);
        DetermineAnimationDirection(rotation);

    }

    /**
     * Calculates the direction of the move animation depending on the angle between the look and movement direction
     */
    private void DetermineAnimationDirection(Quaternion rotation)
    {
        float angle = Vector3.Angle(playerBody.forward, moveDirection.normalized) * Mathf.Deg2Rad;

        Vector3 cross = Vector3.Cross(playerBody.forward, moveDirection.normalized);
        float vectorDir = Vector3.Dot(cross, Vector3.up);

        // if angle is 270 degrees it would be detected as 90 so it must be changed to -90 degrees
        if (vectorDir < 0.0f)
        {
            angle *= -1;
        }
        animationDirection.x = Mathf.Sin(angle);
        animationDirection.y = Mathf.Cos(angle);

    }

    private void CheckFootstepSound()
    {
        if(moveVector.sqrMagnitude > 0.01f)
        {
            if(!sounds[10].isPlaying)
                sounds[10].Play();
        }
        else
        {
            if(sounds[10].isPlaying)
                sounds[10].Stop();
        }
    }

    private void Update()
    {
        if (player.isDead)
        {
            isDashing = false;
            dashEffect.Stop();
            return;
        }
        if (isDashing)
        {
            DashMove();
        }
        else
        {
            Move();
        }

        // sets the animator values
        animator.SetFloat("velocity", moveVector.sqrMagnitude);
        animator.SetFloat("forwardMove", animationDirection.y);
        animator.SetFloat("sidewayMove", animationDirection.x);

        ReduceDashCooldown();
        CheckFootstepSound();
    }

}
