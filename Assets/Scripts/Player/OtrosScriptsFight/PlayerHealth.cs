using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;

    public float invincibilityTime = 1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    void Start()
    {
        health = maxHealth;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible)
        {
            return;
        }

        health -= amount;

        Debug.Log("PLAYER HP: " + health);

        if (health <= 0)
        {
            health = 0;

            animator.SetTrigger("Death");

            Destroy(gameObject, 1f);
        }
        else
        {
            animator.SetTrigger("Hit");

            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;

        // Ignorar colisión entre Player y Enemy
        Physics2D.IgnoreLayerCollision(
            gameObject.layer,
            LayerMask.NameToLayer("Enemy"),
            true
        );

        float timer = 0f;

        // Parpadeo
        while (timer < invincibilityTime)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);

            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);

            timer += 0.2f;
        }

        // Volver a permitir colisión
        Physics2D.IgnoreLayerCollision(
            gameObject.layer,
            LayerMask.NameToLayer("Enemy"),
            false
        );

        spriteRenderer.enabled = true;

        isInvincible = false;
    }
}