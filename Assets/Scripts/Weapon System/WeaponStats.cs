using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon/Stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private int magazineCapacity = 30;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float bulletSpread = 0.05f; // Bullet spread in degrees

    // Public properties to provide controlled access
    public int MagazineCapacity => magazineCapacity;
    public float FireRate => fireRate;
    public float BulletSpread => bulletSpread;
}
