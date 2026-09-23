using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public Slider healthBar;

    public GameObject levelSelectUI;

    private Animator anim;
    private bool isDead;

    void Start()
    {
        healthBar.maxValue = 500;
        healthBar.value = health;

        anim = GetComponent<Animator>();

        // Ocultar el Level Selector al comenzar
        levelSelectUI.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        health -= damage;

        healthBar.value = health;

        if (health <= 0)
        {
            isDead = true;
            health = 0;

            anim.SetTrigger("Death");

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            Invoke(nameof(LoadScene2), 1f);
        }
    }

    private void LoadScene2()
    {
        SceneManager.LoadScene("scene2");
    }
}