using System.Collections;
using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public Transform leftSpawn;
    public Transform rightSpawn;

    public LayerMask floorLayer;

    public float moveUp = 1f;
    public float moveDownSpeed = 4f;
    public float stayTime = 2f;

    // Tiempo entre ataques
    public float attackInterval = 3f;

    private SpriteRenderer spriteRenderer;
    private Collider2D handCollider;
    private bool isAttacking;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        handCollider = GetComponent<Collider2D>();

        spriteRenderer.enabled = false;
        handCollider.enabled = false;

        // Empieza el ciclo automático
        StartCoroutine(AttackLoop());
    }

    IEnumerator AttackLoop()
    {
        // Espera antes del primer ataque
        yield return new WaitForSeconds(attackInterval);

        while (true)
        {
            StartAttack();

            // Esperar hasta que termine el ataque actual
            while (isAttacking)
            {
                yield return null;
            }

            // Esperar antes de volver a atacar
            yield return new WaitForSeconds(attackInterval);
        }
    }

    public void StartAttack()
    {
        if (isAttacking)
            return;

        if (leftSpawn == null || rightSpawn == null)
        {
            Debug.LogError(
                "HandAttack necesita tener asignados leftSpawn y rightSpawn.",
                this
            );
            return;
        }

        if (spriteRenderer == null)
        {
            Debug.LogError(
                "HandAttack necesita un SpriteRenderer en el mismo GameObject.",
                this
            );
            return;
        }

        if (handCollider == null)
        {
            Debug.LogError(
                "HandAttack necesita un Collider2D en el mismo GameObject.",
                this
            );
            return;
        }

        isAttacking = true;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        bool fromRight = Random.value > 0.5f;

        Transform spawn = fromRight ? rightSpawn : leftSpawn;

        transform.position = spawn.position;

        spriteRenderer.flipX = fromRight;

        // Mostrar mano y activar daño
        spriteRenderer.enabled = true;
        handCollider.enabled = true;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            20f,
            floorLayer
        );

        if (hit.collider != null)
        {
            float floorY = hit.point.y;

            float handHeight = spriteRenderer.bounds.extents.y;

            float targetY = floorY + handHeight;

            // Subir primero
            Vector3 upPosition =
                transform.position + Vector3.up * moveUp;

            transform.position = upPosition;

            // Bajar hasta el Floor
            while (transform.position.y > targetY)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(
                        transform.position.x,
                        targetY,
                        transform.position.z
                    ),
                    moveDownSpeed * Time.deltaTime
                );

                yield return null;
            }

            transform.position = new Vector3(
                transform.position.x,
                targetY,
                transform.position.z
            );
        }
        else
        {
            Debug.LogError(
                "HandAttack no encontró el suelo. Revisa floorLayer y la posición de los puntos de spawn.",
                this
            );

            spriteRenderer.enabled = false;
            handCollider.enabled = false;
            isAttacking = false;

            yield break;
        }

        // Se queda atacando
        yield return new WaitForSeconds(stayTime);

        // Quitar daño
        handCollider.enabled = false;

        // Ocultar mano
        spriteRenderer.enabled = false;

        isAttacking = false;
    }
}