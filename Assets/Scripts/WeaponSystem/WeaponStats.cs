using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon/Create new weapon")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private int magazineCapacity = 30;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float bulletSpread = 0.05f; //Not implimented
    [SerializeField] private float bulletRange = 100f;
    [SerializeField] private float damage = 25f;
    [SerializeField] private float damageDropOffStart = 50f;
    [SerializeField] private bool isAutomatic = false;
    [SerializeField] private float reloadSpeed = 1.5f;
    [SerializeField] private float armourMultiplier = 0f;
    [SerializeField] private bool causesStagger;
    [SerializeField] private bool causesBleeding;
    [SerializeField] private float bleedingDmgPerSecond;
    [SerializeField] private float bleedingDuration;


    public int MagazineCapacity => magazineCapacity;
    public float FireRate => fireRate;
    public float BulletSpread => bulletSpread;
    public float BulletRange => bulletRange;
    public float Damage => damage;
    public float DamageDropOffStart => damageDropOffStart;
    public bool IsAutomatic => isAutomatic;
    public float ReloadSpeed => reloadSpeed;

    public float ArmourMultiplier => armourMultiplier;
    public bool CausesStagger => causesStagger;

    public bool CausesBleeding => causesBleeding;
    public float BleedingDmgPerSecond => bleedingDmgPerSecond;
    public float BleedingDuration => bleedingDuration;
}
