using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalBossHealth : MonoBehaviour
{
    public int maxHealth = 1000;
    public int health = 1000;
    public FireballAttack fireballAttack;

    public Slider healthBar;

    private bool isDead = false;

    void Start()
    {
        health = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        health -= damage;

        if (health < 0)
        {
            health = 0;
        }

        Debug.Log("FINAL BOSS HP: " + health);

        if (healthBar != null)
        {
            healthBar.value = health;
        }

        if (health <= 500 && fireballAttack != null)
        {
            fireballAttack.phaseTwo = true;
        }

        if (health <= 0)
        {
            isDead = true;

            Debug.Log("FINAL BOSS DEFEATED!");
            SceneManager.LoadScene("Outro");
        }
    }
}