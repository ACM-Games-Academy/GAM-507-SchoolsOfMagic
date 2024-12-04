using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsFromGround : MonoBehaviour
{
    [SerializeField] private GameObject rootPrefab;                 // Prefab for the root spike
    [SerializeField] private BossStats bossStats;                   // Reference to boss stats
    [SerializeField] private ParticleSystem chargeEffectPrefab;     // Particle effect for wind-up
    [SerializeField] private int numberOfRoots = 3;                 // Total number of roots to spawn
    [SerializeField] private float timeBetweenRoots = 2f;           // Time interval between spawning roots
    [SerializeField] private Vector3 rootRotation = Vector3.zero;   // Desired rotation for the roots
    [SerializeField] private float heightOffset = 0f;               // Height offset for root spawn position

    private float lastAttackTime = -Mathf.Infinity;                 // Tracks the last time the ability was used

    public void TriggerRoots(Vector3 initialPlayerPosition)
    {
        if (Time.time - lastAttackTime >= bossStats.rootsFromGroundCooldown)
        {
            lastAttackTime = Time.time; // Update attack timestamp
            StartCoroutine(ChargeAndSpawnRoots(initialPlayerPosition)); // Pass initial position
        }
    }

    private IEnumerator ChargeAndSpawnRoots(Vector3 initialPlayerPosition)
    {
        // Spawn the initial charge-up particle effect
        if (chargeEffectPrefab != null)
        {
            ParticleSystem chargeEffect = Instantiate(chargeEffectPrefab, initialPlayerPosition, Quaternion.identity);
            chargeEffect.Play();
            Destroy(chargeEffect.gameObject, chargeEffect.main.duration); // Destroy after playing
        }

        // Wait for the charge-up time
        yield return new WaitForSeconds(2f);

        // Spawn roots at the captured position with delays
        for (int i = 0; i < numberOfRoots; i++)
        {
            SpawnRoot(initialPlayerPosition, Quaternion.Euler(rootRotation));

            // Wait between spawning roots
            if (i < numberOfRoots - 1)
            {
                yield return new WaitForSeconds(timeBetweenRoots);
            }
        }
    }

    private void SpawnRoot(Vector3 playerPosition, Quaternion rotation)
    {
        // Apply the height offset to the spawn position
        Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y + heightOffset, playerPosition.z);

        // Spawn the root spike at the modified position with the specified rotation
        GameObject root = Instantiate(rootPrefab, spawnPosition, rotation);

        // Attach a particle effect to the root
        if (chargeEffectPrefab != null)
        {
            ParticleSystem rootEffect = Instantiate(chargeEffectPrefab, root.transform.position, Quaternion.identity, root.transform);
            rootEffect.Play();
            Destroy(rootEffect.gameObject, rootEffect.main.duration); // Clean up after playing
        }

        // Add a damage handler to the root
        if (root.TryGetComponent(out RootDamageHandler rootDamageHandler))
        {
            rootDamageHandler.Initialize(bossStats.rootsFromGroundDamage);
        }
    }
}
