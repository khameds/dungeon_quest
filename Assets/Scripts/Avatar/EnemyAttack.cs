using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
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

        
        if (playerHealth == null)//target == null|| playerHealth == null || enemyHealth == null)
        {
            Debug.Log("[EnnemyAttack.cs] Cannot get PlayerHealth component... ");
        }

        if (target == null)//|| playerHealth == null || enemyHealth == null)
        {
            Debug.Log("[EnnemyAttack.cs] Cannot get PlayerController component... ");
        }

        if ( enemyHealth == null)
        {
            Debug.Log("[EnnemyAttack.cs] Cannot get EnnemyHealth component... ");
        }



    }

    private void Start()
    {
        playerHealth = target.GetComponentInParent<PlayerHealth>();
    }


    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == target)
            playerInRange = true;
    }


    void OnTriggerExit(Collider other)
    {

        // If the exiting collider is the player...
        if (other.gameObject == target)
            playerInRange = false;
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            // ... attack.
            Attack();
        }

        
        // If the player has zero or less health...
     /*   if (target.GetComponentInParent<PlayerHealth>().currentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
            target = enemyController.GetTarget();
        }
        */
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