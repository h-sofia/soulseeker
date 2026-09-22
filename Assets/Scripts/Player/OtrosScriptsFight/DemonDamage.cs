using UnityEngine;

public class DemonDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerFight")
        {
            playerHealth.TakeDamage(damage);
        }
    }


    
}
