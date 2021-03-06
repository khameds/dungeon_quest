﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] public float maxSpeed = 10f;                  // The fastest the player can travel in the x axis.
    [SerializeField] private float jumpForce = 650f;                // Amount of force added when the player jumps.
    [SerializeField] private bool airControl = true;                // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                // A mask determining what is ground to the character
    [SerializeField] private float wallSlideSpeed = 3f;             // Speed of the player over Y-axis while "wall sliding" 
    [SerializeField] private float wallRangeDetection = 0.8f;       // Distance from player's collider within colliding objects are detect
    [SerializeField] private float wallStickTime = 0.25f;           // Wall jump period of time availability
    [SerializeField] private float reviveTimer = 0.75f;
    private float timeUntilRevive = 0f;

    [HideInInspector] public bool facingRight = true;  // For determining which way the player is currently facing.
    [HideInInspector] public bool grounded;            // Whether or not the player is grounded.

    private Transform groundCheck;              // A position marking where to check if the player is grounded.
    const float groundedRadius = .1f;           // Radius of the overlap circle to determine if grounded
    
    private Transform headCheck;              // A position marking where to check for ceilings
    private const float headRadius = .01f;      // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;                  // Reference to the player's Animator component.
    private Rigidbody2D rigidBody;              // Reference to the player's Rigibody2D component 
    private bool wantToJump = false;            // Did the player pressed the "Jump" button
    private float timeToWallUnstick;            // Define if the player is still considered as "wall sliding"
    //private Vector2 input;                      // Store input informations
    private bool canJump = true;                       // Check if the player can jump again;
    [SerializeField] private GameObject hud;
    [HideInInspector] public PlayerInventory inventory;
   

    [HideInInspector] public PlayerHealth playerHealth;
    // At script load
    private void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("GroundCheck");
        headCheck = transform.Find("HeadCheck");
       
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        inventory = GetComponent<PlayerInventory>();
    
      //  input = new Vector2();
    }


    private void FixedUpdate()
    {

        grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                grounded = true;
        }
        animator.SetBool("Ground", grounded);
        animator.SetBool("IsDead", playerHealth.isDead);
        // Set the vertical animation
        animator.SetFloat("vSpeed", rigidBody.velocity.y);


        /**************************/
        /*** Wall jump handling ***/
        /**************************/

        Physics2D.queriesStartInColliders = false; // In order to not collide with our own collider
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallRangeDetection);

        if (hit.collider != null && rigidBody.velocity.y <= 0 && !grounded)
        {
            timeToWallUnstick = wallStickTime;
            
            if (rigidBody.velocity.y < -wallSlideSpeed)
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -wallSlideSpeed);
        }
        else if (hit.collider == null)
        {
            timeToWallUnstick -= Time.deltaTime;
            canJump = true;
        }

        //If still considered as "wall sliding"
        if (timeToWallUnstick > 0 && !grounded)
        {
            int wallDirection = (this.facingRight) ? 1 : -1;

            if (wantToJump && canJump)
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce));
                wantToJump = canJump = false;
            }

            if (GameInputManager.direction == wallDirection)// If running to the wall, just slide over it   
                rigidBody.velocity = new Vector2(0, -wallSlideSpeed);
        }
        else wantToJump = false;

        /***********************/
        /*** Revive Handling ***/
        /***********************/
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            PlayerHealth coopPlayerHealth = hit.collider.GetComponent<PlayerHealth>();
            if (coopPlayerHealth != null)
            {
                if (coopPlayerHealth.isDead)
                {
                   
                    timeUntilRevive += Time.deltaTime;
                    if (timeUntilRevive >= reviveTimer)
                    {
                        Debug.Log("[Revive] CoopPlayerHealth revive.");
                        coopPlayerHealth.Revive();
                        timeUntilRevive = 0f;
                    }
                }
            }
            else
                Debug.Log("[Revive] CoopPlayerHealth == null...");
        }
        else
            timeUntilRevive = 0;

    }
    

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // Store input information for FixedUpdate method
        /*if (!wantToJump) //In usercontrol
        {
            // Read the jump input in Update so button presses aren't missed.
            wantToJump = Input.GetKeyDown(GameInputManager.GIM.jump);
        }*/
        int direction = 0;
        if (Input.GetKey(GameInputManager.GIM.left))
            direction--;
        if (Input.GetKey(GameInputManager.GIM.right))
            direction++;
        GameInputManager.direction = direction; //test
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //input = new Vector2(direction, Input.GetAxis("Vertical"));
    }

    public bool Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            animator.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);

            // If the input is moving the player right and the player is facing left and vis-versa...
            if ((move > 0 && !facingRight) || (move < 0 && facingRight))
                Flip();    
        }

        // If the player should jump...
        if (grounded && jump && animator.GetBool("Ground"))
        {
            // Add a vertical force to the player.
            grounded = false;
            animator.SetBool("Ground", false);
            rigidBody.AddForce(new Vector2(0f, jumpForce));
            wantToJump = false;
            
        }

        return false;
    }

    public void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        hud.transform.localScale = theScale;
    }

    // Draw gizmo for debug purpose
    void OnDrawGizmos()
    {
        //Detect facing wall
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * wallRangeDetection);
    }
}
