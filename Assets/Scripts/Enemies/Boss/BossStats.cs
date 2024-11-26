using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "BossStats", menuName = "EnemyScriptableObjects/BossStats")]
public class BossStats : ScriptableObject
{
    [Header("========== Boss stats ==========")]
    [Header(" ")]
    public float health;
    public bool isArmoured;
    [Header("========== Root slam ==========")]
    [Header(" ")]
    public float rootSlamDamage;
    public float rootSlamCooldown;
    public float rootSlamAttackRange;
    public float rootSlamSplashDamageRange;
    public float rootSlamDuration;

    [Header("========== Roots from ground ==========")]
    [Header(" ")]
    public float rootsFromGroundDamage;
    public float rootsFromGroundCooldown;
    public float rootsAttackDuration;

    [Header("========== Spore attack ==========")]
    [Header(" ")]
    public float sporeAttackDamage;
    public float sporeAttackCooldown;
    public float sporeAttackDuration;
    public float sporesPerBurst;
    public float timeBetweenSpores;

    [Header("========== Spawn enemies ==========")]
    [Header(" ")]
    public float spawnEnemiesCooldown;
    public float spawnEnemiesRange;

}
