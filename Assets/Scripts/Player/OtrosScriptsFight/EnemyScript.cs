using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public Slider healthBar;

    public GameObject levelSelectUI;

    private Animator anim;

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
        health -= damage;

        healthBar.value = health;

        if (health <= 0)
        {
            health = 0;

            anim.SetTrigger("Death");

            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

            Invoke("ShowLevelSelect", 1f);
        }
    }

    void ShowLevelSelect()
    {
        levelSelectUI.SetActive(true);
    }
}