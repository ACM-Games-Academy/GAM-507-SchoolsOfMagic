using UnityEngine;

[CreateAssetMenu(fileName = "TankStats", menuName = "EnemyScriptableObjects/TankStats")]
public class TankStats : ScriptableObject
{
    public float health;
    public float movementSpeed;
    public float attackSpeed;
    public float attackDamage;
    public bool isArmoured;
    public float staggerDuration;
}
