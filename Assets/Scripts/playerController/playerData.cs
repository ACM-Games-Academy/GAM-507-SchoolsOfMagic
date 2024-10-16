using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "playerData" , menuName = "player/playerData" ,order = 1)]
public class playerData : ScriptableObject
{
    public float maxHealth;
    public float maxBlood;
    public float maxStamina;
    public float startingHealth;
    public float startingBlood;
    public float startingStamina;

    public string startingClass;
    //add other things as required
}
