using UnityEngine;

public class HandDamage : MonoBehaviour
{
    public int damage = 25;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}