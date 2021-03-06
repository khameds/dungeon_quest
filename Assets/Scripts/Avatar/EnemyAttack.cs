﻿using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1f;   // The time in seconds between each attack.
    public int attackDamage = 1;            // The amount of health taken away per attack.


    Animator animator;                      // Reference to the animator component.
    GameObject target;                      // Reference to the player GameObject.
    PlayerHealth playerHealth;              // Reference to the player's health.
    EnemyHealth enemyHealth;                // Reference to this enemy's health.
    bool playerInRange;                     // Whether player is within the trigger collider and can be attacked.
    float timer;                            // Timer for counting up to the next attack.
    [SerializeField] private float timeSwitchTarget = 0.5f;
    EnemyController enemyController;
    Collider2D collide;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the entering collider is the player...

        if (enemyController.isChasing && collision.collider.gameObject.tag == "Player")
        {
            //Debug.Log(this.gameObject.name + " : " + target.name + " is in attack range");
            playerInRange = true;
            collide = collision.collider;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // If the exiting collider is the player...
        if (enemyController.isChasing && collision.collider.gameObject.tag == "Player")
        {
            playerInRange = false;
            collide = null;
        }
    }

  
    void Update()
    {
        if (enemyController.isChasing)
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;

            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && collide.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                Debug.Log(this.gameObject.name + " attacks " + target.name + " (" + playerHealth.currentHealth + "/" + playerHealth.maxHealth + "HP)");
                Attack();
            }


            // If the player has zero or less health, switch to valid target
            if (playerHealth.currentHealth <= 0)
            {
                target.GetComponent<Animator>().SetBool("IsDead", true);
               
                target = enemyController.GetTarget();
                if (target != null && enemyController.isChasing)
                {
                    playerHealth = target.GetComponent<PlayerHealth>();
                    playerInRange = false;
                    Debug.Log("[EnemyAttack] Update : Switch to target : " + target.name + ".");
                }
            }
        }

        //this.gameObject.SetActive(enemyController.isChasing);
    }

    IEnumerator Tempo()
    {
        while (!enemyController.isChasing) { }
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(timeSwitchTarget);
    }

    void Attack()
    {
        if (enemyController.isChasing)
        {
            // Reset the timer.
            timer = 0f;

            // If the player has health to lose...
            if (collide.GetComponent<PlayerHealth>().currentHealth > 0)
            {
                // ... damage the player.
                collide.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }
}