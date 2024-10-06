using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Assets/TestScenes/Programming/Launcelot/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public float damage;
    public float fireRate;
}
