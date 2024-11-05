using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BloodPrimaryData", menuName = "BloodWizard/PrimaryAbil")]
public class BloodPrimaryData : ScriptableObject
{
    public float cooldown;
    public float radiusRange;
    public float damage;
    public float healAmount;
    public float healRate;
    public float speedBoost;
    public float boostDuration;
}
