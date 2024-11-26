using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] private BossStats bossStats;
    [SerializeField] private BossEnemy bossEnemy;

    private Transform player;
    private bool attackInProgress = false;

    // Attacks
    private RootSlam rootSlam;
    private SporeAttack sporeAttack;
    private RootsFromGround rootsAttack;
    private SpawnManager spawnManager;

    private float rootSlamLastAttackTime;
    private float sporeAttackLastAttackTime;
    private float rootsAttackLastAttackTime; 

    private enum AttackType { None, RootSlam, SporeAttack, RootsAttack, SpawnEmeny }
    private AttackType currentAttack = AttackType.None;

    private void Start()
    {
        rootSlam = GetComponent<RootSlam>();
        sporeAttack = GetComponent<SporeAttack>();
        rootsAttack = GetComponent<RootsFromGround>(); // Reference the RootsFromGround component
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Prevent multiple attacks at once
        if (attackInProgress) return;

        float playerDistance = Vector3.Distance(transform.position, player.position);
        Vector3 playerPosition = player.position;

        // Determine which attack to perform
        AttackType nextAttack = DetermineNextAttack(playerDistance);

        // Trigger the attack if valid
        switch (nextAttack)
        {
            case AttackType.RootSlam:
                TriggerRootSlam(playerPosition);
                break;
            case AttackType.SporeAttack:
                TriggerSporeAttack(playerPosition);
                break;
            case AttackType.RootsAttack:
                TriggerRootsAttack(playerPosition);
                break;
            default:
                // No attack to perform
                break;
        }
        //Debug.Log(nextAttack);
    }

    private AttackType DetermineNextAttack(float playerDistance)
    {
        // Prioritize Root Slam if the player is within range
        if (playerDistance <= bossStats.rootSlamAttackRange)
        {
            return AttackType.RootSlam;
        }

        // Use Spore Attack if available
        if (IsSporeAttackOffCooldown())
        { 
            return AttackType.SporeAttack;
        }
       
        // Use Roots Attack if available
        if (IsRootsAttackOffCooldown())
        {
            return AttackType.RootsAttack;
        }

        return AttackType.None;


    }

    //========== Root Slam ==========

    private bool IsRootSlamOffCooldown()
    {
        return Time.time - rootSlamLastAttackTime >= bossStats.rootSlamCooldown;
    }

    private void TriggerRootSlam(Vector3 playerPosition)
    {
        attackInProgress = true; // Mark attack as in progress
        currentAttack = AttackType.RootSlam;

        rootSlamLastAttackTime = Time.time; // Update last attack time
        rootSlam.TriggerSlam(playerPosition);

        StartCoroutine(ResetAttackStateAfterDelay(bossStats.rootSlamDuration));
    }

    //========== Spore Attack ==========

    private bool IsSporeAttackOffCooldown()
    {
        return Time.time - sporeAttackLastAttackTime >= bossStats.sporeAttackCooldown;
    }

    private void TriggerSporeAttack(Vector3 playerPosition)
    {
        attackInProgress = true; // Mark attack as in progress
        currentAttack = AttackType.SporeAttack;

        sporeAttackLastAttackTime = Time.time; // Update last attack time
        sporeAttack.TriggerSpore(playerPosition);

        // Use a coroutine to manage attack completion
        StartCoroutine(ResetAttackStateAfterDelay(bossStats.sporeAttackDuration));
    }

    //========== Roots Attack ==========

    private bool IsRootsAttackOffCooldown()
    {
        return Time.time - rootsAttackLastAttackTime >= bossStats.rootsFromGroundCooldown;
    }

    private void TriggerRootsAttack(Vector3 playerPosition)
    {
        attackInProgress = true; // Mark attack as in progress
        currentAttack = AttackType.RootsAttack;

        rootsAttackLastAttackTime = Time.time; // Update last attack time

        // Pass the player's position at the start of the attack
        rootsAttack.TriggerRoots(playerPosition);

        // Use a coroutine to manage attack completion
        StartCoroutine(ResetAttackStateAfterDelay(bossStats.rootsAttackDuration));
    }

    //========== Reset Attack State ==========

    private IEnumerator ResetAttackStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        attackInProgress = false;        // Allow new attacks
        currentAttack = AttackType.None; // Reset current attack
    }

   
}
