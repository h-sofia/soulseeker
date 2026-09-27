using System.Collections;
using UnityEngine;

public class FireballAttack : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform fireballSpawn;

    public float attackInterval = 2f;

    public float minX = -8f;
    public float maxX = 8f;

    // Esto lo va a cambiar el BossHealth
    public bool phaseTwo = false;

    void Start()
    {
        StartCoroutine(FireballLoop());
    }

    IEnumerator FireballLoop()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            SpawnFireballs();

            yield return new WaitForSeconds(attackInterval);
        }
    }

    void SpawnFireballs()
    {
        // Primera fase: 1 fireball
        if (!phaseTwo)
        {
            SpawnFireball();
        }
        // Segunda fase: 2 fireballs
        else
        {
            SpawnFireball();
            SpawnFireball();
        }
    }

    void SpawnFireball()
    {
        float randomX = Random.Range(minX, maxX);

        Vector3 spawnPosition = new Vector3(
            randomX,
            fireballSpawn.position.y,
            fireballSpawn.position.z
        );

        Instantiate(
            fireballPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}