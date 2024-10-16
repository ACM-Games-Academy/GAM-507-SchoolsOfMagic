using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "movementStats", menuName = "Movement System/movementStats")]
public class movementStats : ScriptableObject
{
    public float movementSpeed = 5;
    public float groundedAcceleration = 10;
    public float aerialAcceleration = 2.5f;
    public float jumpHeight = 5;
}
