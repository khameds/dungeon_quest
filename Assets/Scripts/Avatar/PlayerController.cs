using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float maxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float jumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool airControl = true;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask whatIsGround;                 // A mask determining what is ground to the character

    private Transform groundCheck;    // A position marking where to check if the player is grounded.
    const float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool grounded;            // Whether or not the player is grounded.
    private Transform headCheck;   // A position marking where to check for ceilings
    const float headRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator animator;            // Reference to the player's animator component.
    private Rigidbody2D rigidBody;
    public bool facingRight = true;  // For determining which way the player is currently facing.

    private void Awake()
    {
        // Setting up references.
        groundCheck = transform.Find("GroundCheck");
        headCheck = transform.Find("CeilingCheck");
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
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

        // Set the vertical animation
        animator.SetFloat("vSpeed", rigidBody.velocity.y);
    }


    public void Move(float move, bool crouch, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (grounded || airControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            animator.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            rigidBody.velocity = new Vector2(move * maxSpeed, rigidBody.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !facingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
                Flip();
            }
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


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
