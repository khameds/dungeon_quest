using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1f;     // The time in seconds between each attack.
    public int attackDamage = 1;               // The amount of health taken away per attack.


    Animator animator;                              // Reference to the animator component.
    GameObject target;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    float timer;                                // Timer for counting up to the next attack.

    EnemyController enemyController;


    void Awake()
    {
        // Setting up the references.
        enemyController = GetComponent<EnemyController>();
        target = enemyController.GetTarget();
       
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();

        
        if (target == null|| enemyController == null || enemyHealth == null)
        {
            Debug.Log("[EnnemyAttack.cs] Cannot get component references... ");
            return;
        }
    }

    private void Start()
    {   
        playerHealth = target.GetComponent<PlayerHealth>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        Debug.Log(this.gameObject.name + " : " + target.name + " is in attack range");
        if (other.gameObject == target)
            playerInRange = true;
    }


    void OnTriggerExit(Collider other)
    {

        // If the exiting collider is the player...
        if (other.gameObject == target)
            playerInRange = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the entering collider is the player...

        if (collision.collider.gameObject == target)
        {
            Debug.Log(this.gameObject.name + " : " + target.name + " is in attack range");
            playerInRange = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // If the exiting collider is the player...
        if (collision.collider.gameObject == target)
            playerInRange = false;
    }

    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Debug.Log(this.gameObject.name + " attacks " + target.name);
            Attack();
        }

        
        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
           // animator.SetBool("IsDead");
            target = enemyController.GetTarget();
        }
        
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player.
            playerHealth.TakeDamage(attackDamage);
        }
    }
}