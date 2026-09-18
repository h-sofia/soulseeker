using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyScript enemy = hitInfo.GetComponentInParent<EnemyScript>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
