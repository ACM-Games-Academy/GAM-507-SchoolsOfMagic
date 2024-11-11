using TMPro;
using UnityEngine;
using System.Collections;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private ParticleSystem bulletFireEffect;
    private int currentAmmo;
    private float nextFireTime;
    [SerializeField] private WeaponStats weaponStats;
    private bool isShooting;
    private bool isReloading = false;

    private float originalSpeed;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float shootSpeedReduction;
    [SerializeField] private float holdGunSpeedReduction;

    private Coroutine firingCoroutine;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        originalSpeed = playerSpeed;
    }

    private void Update()
    {
        playerSpeed = isShooting ? originalSpeed - shootSpeedReduction : originalSpeed - holdGunSpeedReduction;

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public virtual void Fire()
    {
        if (Time.time < nextFireTime || isReloading) return;

        if (bulletFireEffect != null) bulletFireEffect.Play();


        ShootRay();

        currentAmmo--;
        nextFireTime = Time.time + weaponStats.FireRate;
    }

    protected virtual void ShootRay()
    {
        RaycastHit hit;
        Vector3 spread = raycastOrigin.forward;
        spread += new Vector3(Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread),
                              Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread), 0);
        spread.Normalize();

        if (Physics.Raycast(raycastOrigin.position, spread, out hit, weaponStats.BulletRange))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Calculate fixed damage, modified by armor if applicable
                float damage = CalculateDamage(enemy.IsArmoured);
                Debug.Log("Hit: " + hit.collider.name + " with damage: " + damage);

                enemy.GiveDamage(damage, weaponStats.CausesStagger);
            }
        }
    }

    // Calculate fixed damage, applying armor multiplier if the enemy is armored
    private float CalculateDamage(bool isArmored)
    {
        float baseDamage = weaponStats.Damage;
        return isArmored ? baseDamage * weaponStats.ArmourMultiplier : baseDamage;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(weaponStats.ReloadSpeed);
        currentAmmo = weaponStats.MagazineCapacity;
        isReloading = false;
    }

    public void StartFiring()
    {
        if (weaponStats.IsAutomatic && !isReloading)
        {
            if (firingCoroutine == null)
                firingCoroutine = StartCoroutine(AutoFire());
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
