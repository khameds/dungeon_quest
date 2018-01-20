﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{

    [SerializeField] private float maxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float jumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool airControl = true;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                 // A mask determining what is ground to the character
    [SerializeField] private float wallSlideSpeed = 3f;
    [SerializeField] private float wallRangeDetection = 0.8f;

    private Transform groundCheck;    // A position marking where to check if the player is grounded.
    const float groundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    public bool grounded;            // Whether or not the player is grounded.
                                     // private Transform headCheck;   // A position marking where to check for ceilings
    const float headRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;            // Reference to the player's animator component.
    private Rigidbody2D rigidBody;
    public bool facingRight = true;  // For determining which way the player is currently facing.
    private bool jumping = false;
    Vector2 input;
    private bool wallJumpAvailable = true;

    private void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("GroundCheck");
        //headCheck = transform.Find("HeadCheck");
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        input = new Vector2();
    }


    private void FixedUpdate()
    {
        grounded = false;
        bool wallSliding = false;

        // The enemy is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                grounded = true;
        }
        animator.SetBool("Ground", grounded);

        // Set the vertical animation
        animator.SetFloat("vSpeed", rigidBody.velocity.y);


        // Wall jump handling
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallRangeDetection);

        if (hit.collider != null && rigidBody.velocity.y <= 0 && !grounded)
        {
            wallSliding = true;
            if (rigidBody.velocity.y < -wallSlideSpeed)
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -wallSlideSpeed);
        }

        if (wallSliding && !grounded)
        {

            int wallDirection = (this.facingRight) ? 1 : -1;

            if (Input.GetButtonDown("Jump") && (input.x != wallDirection) && wallJumpAvailable)
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce));
                wallJumpAvailable = false;
            }

            if (input.x == wallDirection && (input.x != 0))
                rigidBody.velocity = new Vector2(0, -wallSlideSpeed);
        }

        if (hit.collider == null)
            wallJumpAvailable = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(float move, bool jump)
    {
        if (grounded || airControl)
        {
            //TODO

        }

        // If the player should jump...
        if (grounded && jump && animator.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            grounded = false;
            animator.SetBool("Ground", false);
            rigidBody.AddForce(new Vector2(0f, jumpForce));
        }
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}