﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class PlayerMovement : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float bounceVelocityRatio = 0.5f;
    public float timeToJumpApex = .4f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    public bool canWallSlide;
    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding = false;
    private int wallDirX;

    private bool bouncing = false;

    public AudioClip jumpSound;
    public AudioClip grabSound;
    public AudioClip throwSound;
    //public AudioClip dropSound;
    public AudioClip stunSound;
    public AudioSource audioSource;

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private PlayerInput input;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
    }

    private void Update() 
    {
        // || !input.CanMove()
        if (Time.timeScale == 0f ) {
            return;
        }

        CalculateVelocity();
        if (canWallSlide) {
            HandleWallSliding();
        }

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || IsGrounded())
        {
            velocity.y = 0f;
        }
    }

    private void LateUpdate()
    {
        if (directionalInput.x != 0f)
        {
            spriteRenderer.flipX = directionalInput.x > 0;
        }
        anim.SetBool("Running", Mathf.Abs(directionalInput.x) > 0);
        anim.SetBool("Grounded", IsGrounded());
        anim.SetBool("WallSliding", wallSliding);
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (canWallSlide && wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
            isDoubleJumping = false;
        }
        if (IsGrounded())
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
            audioSource.PlayOneShot(jumpSound);
            //anim.SetInteger("animationState", 2);
        }
        if (canDoubleJump && !IsGrounded() && !isDoubleJumping && !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = true;
        }
    }

    public void OnJumpInputUp() {
        if (!bouncing && velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }
    
    private void HandleWallSliding() {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !IsGrounded() && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    private void CalculateVelocity() {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (IsGrounded() ? accelerationTimeGrounded : accelerationTimeAirborne));
        velocity.y += gravity * Time.deltaTime;
    }

    public void Bounce() {
        bouncing = true;
        velocity.y = maxJumpVelocity * bounceVelocityRatio;
        //anim.SetInteger("animationState", 2);
        StartCoroutine(BounceRoutine());
    }

    private IEnumerator BounceRoutine() {
        float t = 0f;
        while (t < timeToJumpApex && !IsGrounded()) {
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        bouncing = false;
    }

    public bool IsGrounded() {
        return controller.collisions.below;
    }

    public bool IsFalling()
    {
        return velocity.y < 0f;
    }

    public void Die() {
        PlayerInput input = GetComponent<PlayerInput>();
        input.DisableMovement();
        //anim.SetTrigger("Die");
        //audioSource.PlayOneShot(deathSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Controller2D>().enabled = false;
        enabled = false;
    }
}
