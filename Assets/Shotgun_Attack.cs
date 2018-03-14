using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_Attack : MonoBehaviour {

    public int attackDamage = 1;            // The amount of health taken away per attack.
    public float shootVelocity;
    EnemyHealth enemyHealth;                // Reference to this enemy's health.
    PlayerHealth playerHealth;

    bool hit;

    private void Start()
    {
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
        else if (collision.gameObject.tag.Equals("Player") && !hit)
        {
            hit = true;
            playerHealth = collision.collider.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage);
            Destroy(gameObject);

        }
        else if (!collision.gameObject.tag.Equals("Teleporter") && !collision.gameObject.tag.Equals("Ammo"))
        {
            Destroy(gameObject);
        }
    }

}
