using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class WallJump : MonoBehaviour
{
    public float distance = 1f;
  
    public float wallSlideSpeed = 3f;
    public Vector2 wallJumpForce;
    public Vector2 wallJumpOff;

    PlayerController playerController;
    new Rigidbody2D rigidbody;

    bool wallJumping;
    

    // Use this for initialization
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool wallSliding = false;

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);

        if (hit.collider != null)
            Debug.Log(string.Concat("Hit : ", hit.collider.ToString()));

        if(hit.collider != null && rigidbody.velocity.y < 0)
        {
            wallSliding = true;
            if (rigidbody.velocity.y < -wallSlideSpeed)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -wallSlideSpeed);
        }
            
        if (CrossPlatformInputManager.GetButtonDown("Jump") && wallSliding) //&& !playerController.grounded && hit.collider != null)
        {
            Vector2 input = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
            int wallDirection = (playerController.facingRight) ? 1 : -1;

            if (input.x != wallDirection)
                playerController.Move(1, true);
        }
    }


    // Draw gizmo for debug purpose
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}