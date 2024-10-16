using TMPro;
using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private WeaponStats[] weaponStatsArray;     // Array to store multiple weapon configurations
    [SerializeField] private Transform raycastOrigin;            // Raycast position
    [SerializeField] private Transform particleEffectSpawnPoint; // Transform for spawning particle systems
    private ParticleSystem bulletFireEffect;                     // Reference to the particle system
    private int currentAmmo;                                     // Ammo tracking
    private float nextFireTime;                                  // Fire rate control
    private int currentWeaponIndex = 0;                          // Track which weapon is currently active
    private WeaponStats currentWeaponStats;                      // Store the current weapon's stats
    private bool isShooting;                                     // Track whether automatic fire is enabled
    private bool isReloading = false;                            // Track if the weapon is currently reloading

    // Player speed reduction vars
    private float originalSpeed;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float shootSpeedReduction;
    [SerializeField] private float holdGunSpeedReduction;

    // UI elements
    public TextMeshProUGUI currentWeapon, ammoCount, reloadingNotif;

    private Coroutine firingCoroutine;

    private void Start()
    {
        originalSpeed = playerSpeed; // Save the original player speed
        reloadingNotif.text = ""; // Hide reload text

        if (weaponStatsArray.Length > 0)
        {
            SetWeapon(0); // Initialize the first weapon
            reloadAmmoCount();
        }
    }

    private void Update()
    {
        // Adjust player speed while holding gun or shooting
        if (isShooting)
        {
            playerSpeed = originalSpeed - shootSpeedReduction;
        }
        else
        {
            playerSpeed = originalSpeed - holdGunSpeedReduction;
        }

        // Reload when 'R' key is pressed and not already reloading
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public virtual void Fire()
    {
        if (Time.time < nextFireTime || isReloading)
            return; // Wait until the next fire time or finish reloading

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

        reloadAmmoCount();
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
        reloadAmmoCount();

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
        currentWeapon.text = ("Current weapon: " + currentWeaponStats.name);
        reloadAmmoCount();
    }

    // Placeholder for raycasting logic
    protected virtual void ShootRay()
    {
        RaycastHit hit;

        Vector3 spread = raycastOrigin.forward;
        spread += new Vector3(Random.Range(-currentWeaponStats.BulletSpread, currentWeaponStats.BulletSpread),
                              Random.Range(-currentWeaponStats.BulletSpread, currentWeaponStats.BulletSpread), 0);
        spread.Normalize();

        if (Physics.Raycast(raycastOrigin.position, spread, out hit, currentWeaponStats.BulletRange))
        {
            // Apply damage based on distance
            float damage = CalculateDamage(hit.distance);
            Debug.Log("Hit: " + hit.collider.name + " with damage: " + damage);
            // Implement damage application here
        }
        else
        {
            Debug.Log("Missed.");
        }
    }

    // Calculate damage with drop-off over distance
    private float CalculateDamage(float distance)
    {
        if (distance <= currentWeaponStats.DamageDropOffStart)
        {
            return currentWeaponStats.Damage;
        }

        float damageFalloff = (distance - currentWeaponStats.DamageDropOffStart) / (currentWeaponStats.BulletRange - currentWeaponStats.DamageDropOffStart);
        return Mathf.Clamp(currentWeaponStats.Damage * (1 - damageFalloff), 0, currentWeaponStats.Damage);
    }

    // Coroutine to handle reloading speed based on reloadSpeed
    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        reloadingNotif.text = "Reloading...";

        yield return new WaitForSeconds(currentWeaponStats.ReloadSpeed);

        currentAmmo = currentWeaponStats.MagazineCapacity;
        reloadAmmoCount();

        reloadingNotif.text = ""; // Clear reloading notification
        isReloading = false;
    }

    public void reloadAmmoCount()
    {
        ammoCount.text = $"Ammo: {currentAmmo}/{currentWeaponStats.MagazineCapacity}";
    }



    public void StartFiring()
    {
        if (currentWeaponStats.IsAutomatic && !isReloading)
        {
            if (firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(AutoFire());
            }
        }
        else if (!currentWeaponStats.IsAutomatic && !isReloading)
        {
            Fire();
        }

        isShooting = true;
    }

    public void StopFiring()
    {
        if (currentWeaponStats.IsAutomatic && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }

        isShooting = false;
    }

    private IEnumerator AutoFire()
    {
        while (isShooting && !isReloading)
        {
            Fire();
            yield return new WaitForSeconds(currentWeaponStats.FireRate);
        }
    }

    
}
