using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityNameData", menuName = "Assets/Scripts/AbilitySystem/AbilityData", order = 1)]
public class AbilityData : ScriptableObject
{
    //Effected enemies effects
    public float damage;
    public float bleedDamage;
    public float bleedDuration;
    public float physicsPower;  //this is for visual effects e.g. pushing enemies away

    //resources buffs
    public float bloodGain;
    public float metalGain;

    //health buffs
    public float healthGain;
}
