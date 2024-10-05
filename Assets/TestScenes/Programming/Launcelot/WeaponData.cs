using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Assets/TestScenes/Programming/Launcelot/WeaponDataMenu", order = 1)]
public class WeaponData : ScriptableObject
{
    public float damage;
    public float fireRate;
}
