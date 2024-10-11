using TMPro;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private WeaponStats[] weaponStatsArray;     // Array to store multiple weapon configurations
    [SerializeField] private Transform raycastOrigin;            // Raycast position (gun barrel)
    [SerializeField] private Transform particleEffectSpawnPoint; // Transform for spawning particle systems
    private ParticleSystem bulletFireEffect;                     // Reference to the particle system
    private int currentAmmo;                                     // Ammo tracking
    private float nextFireTime;                                  // Fire rate control
    private int currentWeaponIndex = 0;                          // To track which weapon is currently active
    private WeaponStats currentWeaponStats;                      // To store the current weapon's stats

    //temp
    public TextMeshProUGUI currentWeapon, ammoCount;


    private void Start()
    {
        if (weaponStatsArray.Length > 0)
        {
            SetWeapon(0); // Initialize the first weapon
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button to fire
        {
            Fire();
        }

        // Switch weapon with number keys 
        if (Input.GetKeyDown(KeyCode.Alpha1)){SetWeapon(0);}
        if (Input.GetKeyDown(KeyCode.Alpha2)){SetWeapon(1);}

    }

    public virtual void Fire()
    {
        if (Time.time < nextFireTime)
            return; // Wait until the next fire time

        if (currentAmmo > 0)
        {
            // Play the particle system when firing
            if (bulletFireEffect != null)
            {
                bulletFireEffect.Play();
            }

            Debug.Log("Fired: " + currentWeaponStats.name);
            ShootRay();

            currentAmmo--;
            nextFireTime = Time.time + currentWeaponStats.FireRate;
        }
        else
        {
            Debug.Log("Out of ammo! Reload.");
        }

      //  ammoCount.text = ("Current weapon: " + currentWeaponStats.name);
    }

    // Set the current weapon and its related properties
    public void SetWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponStatsArray.Length)
        {
            Debug.LogWarning("Invalid weapon index.");
            return;
        }

        currentWeaponIndex = weaponIndex;
        currentWeaponStats = weaponStatsArray[weaponIndex];

        // Initialize ammo for the new weapon
        currentAmmo = currentWeaponStats.MagazineCapacity;

        // Destroy the previous particle system if it exists
        if (bulletFireEffect != null)
        {
            Destroy(bulletFireEffect.gameObject);
        }

        // Instantiate the new ParticleSystem from WeaponStats at the appropriate spawn point
        if (currentWeaponStats.BulletFireEffect != null)
        {
            bulletFireEffect = Instantiate(currentWeaponStats.BulletFireEffect,
                                           particleEffectSpawnPoint.position,
                                           particleEffectSpawnPoint.rotation,
                                           particleEffectSpawnPoint);
        }

        Debug.Log("Switched to: " + currentWeaponStats.name);
        currentWeapon.text = ("Current weapon: " + currentWeaponStats.name);
    }

    // Placeholder for raycasting logic
    protected virtual void ShootRay()
    {
        RaycastHit hit;

        Vector3 spread = raycastOrigin.forward;
        spread += new Vector3(Random.Range(-currentWeaponStats.BulletSpread, currentWeaponStats.BulletSpread),
                              Random.Range(-currentWeaponStats.BulletSpread, currentWeaponStats.BulletSpread), 0);
        spread.Normalize();

        if (Physics.Raycast(raycastOrigin.position, spread, out hit))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Missed.");
        }
    }

    public virtual void Reload()
    {
        currentAmmo = currentWeaponStats.MagazineCapacity;
        Debug.Log("Reloading... Ammo refilled.");
    }
}
