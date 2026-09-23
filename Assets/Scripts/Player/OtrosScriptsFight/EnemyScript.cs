using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public Slider healthBar;

    private Animator anim;

    void Start()
    {
        healthBar.maxValue = 500;
        healthBar.value = health;

        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.value = health;

        if (health <= 0)
        {
            health = 0;

            anim.SetTrigger("Death");

            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 2f);
    }
}