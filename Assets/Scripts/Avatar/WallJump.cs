using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class WallJump : MonoBehaviour
{
    public float distance = 1f;
    PlayerController movement;
    public float speed = 2f;
    bool wallJumping;

    // Use this for initialization
    void Start()
    {
        movement = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
        

        if (CrossPlatformInputManager.GetButtonDown("Jump") && !movement.grounded && hit.collider != null)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(speed * hit.normal.x, speed);
            StartCoroutine("Flip");     // Flip character's avatar
        }
    }

    // Flip character's avatar using multi-thread
    IEnumerator Flip()
    {
        yield return new WaitForFixedUpdate();
        transform.localScale = transform.localScale.x == 1 ? new Vector2(-1, 1) : Vector2.one;
        movement.facingRight = !movement.facingRight;
    }

    // Draw gizmo for debug purpose
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}