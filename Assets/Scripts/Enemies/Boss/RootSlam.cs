using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSlam : MonoBehaviour
{
    [SerializeField] private Transform attackPointLeft;
    [SerializeField] private Transform attackPointRight;
    [SerializeField] private BossStats bossStats;
    private float lastAttackTime;

    public void TriggerSlam(Vector3 playerPosition)
    {
        if (Time.time - lastAttackTime >= bossStats.rootSlamCooldown)
        {
            lastAttackTime = Time.time; // Update attack timestamp
            StartCoroutine(ExecuteSlam());
        }
    }

    private IEnumerator ExecuteSlam()
    {
        // Wind-up 
        GetComponentInChildren<Animator>().Play("Branch Slam");
        Debug.Log("Root Slam charging...");

        yield return new WaitForSeconds(1.8f);

        // Slam 
        Debug.Log("Slammed!");

      
        Collider[] hitCollidersLeft = Physics.OverlapSphere(attackPointLeft.position, bossStats.rootSlamSplashDamageRange);
        Collider[] hitCollidersRight = Physics.OverlapSphere(attackPointRight.position, bossStats.rootSlamSplashDamageRange);

        // Combine colliders into a single list to avoid duplicate processing
        HashSet<Collider> uniqueHitColliders = new HashSet<Collider>(hitCollidersLeft);
        uniqueHitColliders.UnionWith(hitCollidersRight);

        foreach (Collider hit in uniqueHitColliders)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Player Hit!");
                ApplyDamageToPlayer(hit);
            }
        }
    }

    private void ApplyDamageToPlayer(Collider playerCollider)
    {
        // Access the PlayerController and apply damage
        PlayerController playerController = playerCollider.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.AddReduceValue(PlayerController.ValueType.Health, -bossStats.rootSlamDamage, false);
        }
        else
        {
            Debug.LogWarning("PlayerController not found on the hit player object!");
        }
    }
}
