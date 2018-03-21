using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public AudioClip deathClip;


    Animator animator;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    bool isDead;


    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();


        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            print("[EnemyHealth]return;");
            return;
        }
        Debug.Log("Ennemy" + currentHealth);
        //enemyAudio.Play();
        currentHealth -= amount;

        if (currentHealth <= 0)
            Death();
    }


    void Death()
    {
        isDead = true;
        //animator.SetTrigger("Dead");
        //enemyAudio.clip = deathClip;
        //enemyAudio.Play();

        // TODO add particule effect
        Destroy(gameObject);
    }

}
