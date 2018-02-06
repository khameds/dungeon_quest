﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAttack : MonoBehaviour {
    public int attackDamage = 1;            // The amount of health taken away per attack.
    Animator animator;                      // Reference to the animator component.
    EnemyHealth enemyHealth;                // Reference to this enemy's health.

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the entering collider is an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealth = collision.collider.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackDamage);
            Destroy(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }
}