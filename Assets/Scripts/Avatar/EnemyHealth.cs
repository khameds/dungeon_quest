using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public int currentHealth;
    public AudioClip deathClip;


    Animator animator;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    bool isDead;


    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();


        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            print("[EnemyHealth]return;");
            return;
        }

        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
            Death();
    }


    void Death()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        //TODO : death animation and destroy
    }

}
