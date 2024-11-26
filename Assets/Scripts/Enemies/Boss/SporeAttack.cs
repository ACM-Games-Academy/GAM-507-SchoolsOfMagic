using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeAttack : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;               // Boss stats
    [SerializeField] private GameObject projectilePrefab;       // Projectile prefab
    [SerializeField] private Transform projectileSpawnPoint;    // Spawn point for the spores
    [SerializeField] private float burstCooldown = 5f;          // Cooldown between bursts

    private float lastAttackTime;

    public void TriggerSpore(Vector3 targetPosition)
    {
        // Check if cooldown has elapsed
        if (Time.time - lastAttackTime >= burstCooldown)
        {
            lastAttackTime = Time.time; // Update attack timestamp
            StartCoroutine(SpawnSporeBurst(targetPosition));
        }
    }

    private IEnumerator SpawnSporeBurst(Vector3 targetPosition)
    {
        for (int i = 0; i < bossStats.sporesPerBurst; i++)
        {
            SpawnSpore(targetPosition);

            // Wait between spawning each spore
            yield return new WaitForSeconds(bossStats.timeBetweenSpores);
        }
    }

    private void SpawnSpore(Vector3 targetPosition)
    {
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

            // Orient the projectile to face the target position
            projectileInstance.transform.LookAt(targetPosition);

            // Assign player reference to the projectile script
            EnemyProjectile projectileScript = projectileInstance.GetComponent<EnemyProjectile>();
            if (projectileScript != null)
            {
                projectileScript.player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
            }
        }
    }
}
