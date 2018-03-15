using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool isDead;
    public bool damaged;
    public float percentageRevive = 0.5f;


    AudioSource playerAudio;
    SpriteRenderer spriteRenderer;
    Color baseColor;

    Animator animator;
    UserControl userControl;

    void Awake()
    {
        Screen.fullScreen = !Screen.fullScreen;
        // Set all references
        animator = GetComponent<Animator>();
        userControl = GetComponent<UserControl>();
 
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            if(collision.collider.gameObject.GetComponent<PlayerHealth>().isDead)
            {
                collision.collider.gameObject.GetComponent<PlayerHealth>().Revive();
            }
        }
    }
    void Update()
    {

        if (damaged)
            spriteRenderer.color = flashColor;
        else
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, /*Color.clear*/baseColor, flashSpeed * Time.deltaTime);

        damaged = false;
        
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = (float) currentHealth / maxHealth;

        // Play the hurt sound effect.
        //playerAudio.Play();
        //Debug.Log(this.gameObject.name + " takes " + amount + "damage. (" + currentHealth + "/" + maxHealth + ")");

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            currentHealth = 0;
            Death();
        }
    }

    public void Revive()
    {
        userControl.Dead = isDead = false;
        animator.SetBool("IsDead", false);
        animator.Play("Idle");
        currentHealth = (int)(maxHealth / 2);
        Debug.Log(this.gameObject.name + " revived !");
        // Set the health bar's value to the current health.
        healthSlider.value = (float)currentHealth / maxHealth;
    }

    public void FullRevive()
    {
        userControl.Dead = isDead = false;
        animator.SetBool("IsDead", false);
        animator.Play("Idle");
        currentHealth = (int)maxHealth;
        // Set the health bar's value to the current health.
        healthSlider.value = (float)currentHealth / maxHealth;
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        userControl.Dead = isDead = true;

        // Tell the animator that the player is dead.
        animator.SetBool("IsDead", true);
        Debug.Log(this.gameObject.name + " died !");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off the movement and shooting scripts.
        //userControl.enabled = false;
        userControl.Dead = true;

    }
}

