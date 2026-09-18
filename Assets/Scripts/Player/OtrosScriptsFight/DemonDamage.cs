using UnityEngine;

public class DemonDamage : MonoBehaviour
{
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovementwa player = hitInfo.GetComponentInParent<PlayerMovementwa>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
