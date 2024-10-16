using TMPro;
using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    //[SerializeField] private WeaponStats[] weaponStatsArray;     // Array to store multiple weapon configurations
    [SerializeField] private Transform raycastOrigin;            // Raycast position
    [SerializeField] private ParticleSystem bulletFireEffect; // Transform for spawning particle systems
    private int currentAmmo;                                     // Ammo tracking
    private float nextFireTime;                                  // Fire rate control
    [SerializeField] private WeaponStats weaponStats;                      // Store the current weapon's stats
    private bool isShooting;                                     // Track whether automatic fire is enabled
    private bool isReloading = false;                            // Track if the weapon is currently reloading

    // Player speed reduction vars
    private float originalSpeed;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float shootSpeedReduction;
    [SerializeField] private float holdGunSpeedReduction;

    // UI elements
    //public TextMeshProUGUI currentWeapon, ammoCount, reloadingNotif;

    private Coroutine firingCoroutine;

    private void Start()
    {
        originalSpeed = playerSpeed; // Save the original player speed
        //reloadingNotif.text = ""; // Hide reload text

        
        //reloadAmmoCount();       
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

            Debug.Log("Fired: " + weaponStats.name);
            ShootRay();

            currentAmmo--;
            nextFireTime = Time.time + weaponStats.FireRate;
        }
        else
        {
            Debug.Log("Out of ammo! Reload.");
        }

        //reloadAmmoCount();
    }

    // Set the current weapon and its related properties
    /*
    public void SetWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponStatsArray.Length)
        {
            Debug.LogWarning("Invalid weapon index.");
            return;
        }

        currentWeaponIndex = weaponIndex;
        weaponStats = weaponStatsArray[weaponIndex];

        // Initialize ammo for the new weapon
        currentAmmo = weaponStats.MagazineCapacity;
        reloadAmmoCount();

        // Destroy the previous particle system if it exists
        if (bulletFireEffect != null)
        {
            Destroy(bulletFireEffect.gameObject);
        }

        // Instantiate the new ParticleSystem from WeaponStats at the appropriate spawn point
        if (weaponStats.BulletFireEffect != null)
        {
            bulletFireEffect = Instantiate(weaponStats.BulletFireEffect,
                                           particleEffectSpawnPoint.position,
                                           particleEffectSpawnPoint.rotation,
                                           particleEffectSpawnPoint);
        }
        currentWeapon.text = ("Current weapon: " + weaponStats.name);
        reloadAmmoCount();
    }
    */

    // Placeholder for raycasting logic
    protected virtual void ShootRay()
    {
        RaycastHit hit;

        Vector3 spread = raycastOrigin.forward;
        spread += new Vector3(Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread),
                              Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread), 0);
        spread.Normalize();

        if (Physics.Raycast(raycastOrigin.position, spread, out hit, weaponStats.BulletRange))
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
        if (distance <= weaponStats.DamageDropOffStart)
        {
            return weaponStats.Damage;
        }

        float damageFalloff = (distance - weaponStats.DamageDropOffStart) / (weaponStats.BulletRange - weaponStats.DamageDropOffStart);
        return Mathf.Clamp(weaponStats.Damage * (1 - damageFalloff), 0, weaponStats.Damage);
    }

    // Coroutine to handle reloading speed based on reloadSpeed
    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        //reloadingNotif.text = "Reloading...";

        yield return new WaitForSeconds(weaponStats.ReloadSpeed);

        currentAmmo = weaponStats.MagazineCapacity;
        //reloadAmmoCount();

        //reloadingNotif.text = ""; // Clear reloading notification
        isReloading = false;
    }

    /*
    public void reloadAmmoCount()
    {
        ammoCount.text = $"Ammo: {currentAmmo}/{weaponStats.MagazineCapacity}";
    }
    */



    public void StartFiring()
    {
        if (weaponStats.IsAutomatic && !isReloading)
        {
            if (firingCoroutine == null)
            {
                firingCoroutine = StartCoroutine(AutoFire());
            }
        }
        else if (!weaponStats.IsAutomatic && !isReloading)
        {
            Fire();
        }

        isShooting = true;
    }

    public void StopFiring()
    {
        if (weaponStats.IsAutomatic && firingCoroutine != null)
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
            yield return new WaitForSeconds(weaponStats.FireRate);
        }
    }

    
}
