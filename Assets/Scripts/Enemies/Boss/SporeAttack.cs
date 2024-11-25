using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeAttack : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;              
    [SerializeField] private float projectileCooldown = 3f;     
    [SerializeField] private GameObject projectilePrefab;       
    [SerializeField] private Transform projectileSpawnPoint;    
    private float lastAttackTime;

    public void TriggerSpore(Vector3 targetPosition)
    {
        // Check if cooldown has elapsed
        if (Time.time - lastAttackTime >= bossStats.sporeAttackCooldown)
        {
            lastAttackTime = Time.time; // Update attack timestamp
           SpawnSpore(targetPosition);
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
