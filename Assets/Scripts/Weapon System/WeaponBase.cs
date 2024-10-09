using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] private WeaponStats weaponStats;          // Reference to the ScriptableObject
    [SerializeField] private ParticleSystem bulletFireEffect;  // Reference to the particle system
    [SerializeField] private Transform raycastOrigin;          // Raycast position (gun barrel)
    private int currentAmmo;                                   // Ammo tracking
    private float nextFireTime;                                // Fire rate control

    private void Start()
    {
        // Initialize the ammo count
        currentAmmo = weaponStats.MagazineCapacity;
        nextFireTime = 0f;
    }

    // Public method to fire the weapon
    public virtual void Fire()
    {
        // Check if enough time has passed to fire again (based on fire rate)
        if (Time.time < nextFireTime)
        {
            return; // Wait until the next fire time
        }

        // Check if we have ammo
        if (currentAmmo > 0)
        {
            // Play particle system effect (simulate muzzle flash or bullet effect)
            if (bulletFireEffect != null)
            {
                bulletFireEffect.Play();
            }

            // Simulate bullet fire using raycasting
            ShootRay();

            // Reduce ammo count
            currentAmmo--;

            // Set the time for the next shot based on fire rate
            nextFireTime = Time.time + weaponStats.FireRate;
        }
        else
        {
            Debug.Log("Out of ammo! Reload.");
        }
    }

    // Placeholder for raycasting logic
    protected virtual void ShootRay()
    {
        RaycastHit hit;

        // Apply bullet spread by slightly randomizing the ray direction
        Vector3 spread = raycastOrigin.forward;
        spread += new Vector3(Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread),
                              Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread), 0);
        spread.Normalize(); // Normalize to ensure we still have a valid direction

        if (Physics.Raycast(raycastOrigin.position, spread, out hit))
        {
            Debug.Log("Hit: " + hit.collider.name);
            // Placeholder for hit effect or damage logic
        }
        else
        {
            Debug.Log("Missed.");
        }
    }

    // Public method to reload the weapon
    public virtual void Reload()
    {
        // Infinite ammo, so just refill the magazine
        currentAmmo = weaponStats.MagazineCapacity;
        Debug.Log("Reloading... Ammo refilled.");
    }
}
