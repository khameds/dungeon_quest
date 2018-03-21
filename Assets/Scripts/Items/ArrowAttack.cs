using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack : MonoBehaviour {
    public int attackDamage = 1;            // The amount of health taken away per attack.
    public float shootVelocity = 20;
    Animator animator;                      // Reference to the animator component.
    EnemyHealth enemyHealth;                // Reference to this enemy's health.
    PlayerHealth playerHealth;
    private Rigidbody2D rb;
    bool hit;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hit = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the entering collider is an enemy
        if (collision.gameObject.tag == "Enemy" && !hit)
        {
            hit = true;
            enemyHealth = collision.collider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Equals("Player"))
        {
            hit = true;
            playerHealth = collision.collider.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage);
            Destroy(gameObject);

        }
        else if (!collision.gameObject.tag.Equals("Teleporter"))
        {
            Destroy(gameObject);
        } 
    }

    private void Update()
    {
        Vector3 dir = rb.velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}