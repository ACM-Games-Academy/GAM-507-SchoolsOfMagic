using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "movementStats", menuName = "Movement System/movementStats")]
public class movementStats : ScriptableObject
{
    [Header("Standard Movement")]
    public float movementSpeed = 5;
    public float groundedAcceleration = 10;
    public float aerialAcceleration = 2.5f;
    public float jumpHeight = 5;

    [Header("Movement Abilities")]
    public float forwardBurstSpeed = 15;
    public int blinkAmount = 2;
    public float bloodBoostSpeedBoostMultiplier = 0.5f;
}
