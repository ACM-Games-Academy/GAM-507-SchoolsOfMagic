using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon/Create new weapon")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private int magazineCapacity = 30;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float bulletSpread = 0.05f;
    [SerializeField] private float bulletRange = 0.0f;
    [SerializeField] private int maxAmmo = 0;
    [SerializeField] private ParticleSystem bulletFireEffect; // Particle system reference

    public int MagazineCapacity => magazineCapacity;
    public float FireRate => fireRate;
    public float BulletSpread => bulletSpread;
    public float BulletRange => bulletRange;
    public int MaxAmmo => maxAmmo;
    public ParticleSystem BulletFireEffect => bulletFireEffect;
}
