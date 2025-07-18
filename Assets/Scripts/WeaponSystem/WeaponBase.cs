using TMPro;
using UnityEngine;
using System.Collections;
using System;



public class WeaponBase : MonoBehaviour
{
    [Header("Gun Switch Event")]
    public AK.Wwise.Event gunShoot;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private ParticleSystem bulletFireEffect;

    private int currentAmmo;
    public int CurrentAmmo { get { return currentAmmo; } }

    private float nextFireTime;

    [SerializeField] private WeaponStats weaponStats;
    public WeaponStats WeaponStats
    { get { return weaponStats; } }
     
    private bool isShooting;
    private bool isReloading = false;
    public bool canShoot;

    private float originalSpeed;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float shootSpeedReduction;
    [SerializeField] private float holdGunSpeedReduction;

    [SerializeField] private WeaponAnimations weaponAnimator;

    [SerializeField] private GameObject bleedingEffect;

    private Coroutine firingCoroutine;

    public event EventHandler gunFired;

    private bool initalized = false;

    private void OnEnable()
    {
        canShoot = true;

        GetGunModel().SetActive(true);
    }

    public void enableWeapon()
    {
        if (!initalized)
        {
            currentAmmo = weaponStats.MagazineCapacity;
            originalSpeed = playerSpeed;

            initalized = true;
        }
    }

    private void Update()
    {
        playerSpeed = isShooting ? originalSpeed - shootSpeedReduction : originalSpeed - holdGunSpeedReduction;
    }

    public virtual void Fire()
    {
        if (Time.time < nextFireTime || isReloading) return;

        if (bulletFireEffect != null && currentAmmo > 0 && canShoot)
        {
            bulletFireEffect.Play();
            gunShoot.Post(this.gameObject);

            ShootRay();

            weaponAnimator.AnimateGunShot();

            currentAmmo--;
        }
        else
        {
            //no ammo
            //PUT NO AMMO SOUNDS HERE
        }
        
        nextFireTime = Time.time + weaponStats.FireRate;

        //put fire event here
        gunFired?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void ShootRay()
    {
        RaycastHit hit;
        Vector3 spread = raycastOrigin.forward;
        spread += new Vector3(UnityEngine.Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread),
                              UnityEngine.Random.Range(-weaponStats.BulletSpread, weaponStats.BulletSpread), 0);
        spread.Normalize();

        if (Physics.Raycast(raycastOrigin.position, spread, out hit, weaponStats.BulletRange))
        {
            

            if (hit.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // Calculate fixed damage, modified by armor if applicable
                float damage = CalculateDamage(enemy.IsArmoured);
                Debug.Log("Hit: " + hit.collider.name + " with damage: " + damage);

                enemy.GiveDamage(damage, weaponStats.CausesStagger);

                if (weaponStats.CausesBleeding && !enemy.IsBleeding)
                {
                    GameObject particleEffect = Instantiate(bleedingEffect, hit.point, Quaternion.FromToRotation(Vector3.zero, hit.normal), hit.transform);
                    enemy.GiveBleeding(weaponStats.BleedingDuration, weaponStats.BleedingDmgPerSecond, particleEffect);   
                }                
            }
        }
    }

    // Calculate fixed damage, applying armor multiplier if the enemy is armored
    private float CalculateDamage(bool isArmored)
    {
        float baseDamage = weaponStats.Damage;
        return isArmored ? baseDamage * weaponStats.ArmourMultiplier : baseDamage;
    }

    public void AddAmmo(int ammo)
    {
        currentAmmo += ammo;
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

    public void SetActiveShooting(bool active)
    {
        canShoot = active;
    }

    public GameObject GetGunModel()
    {
        return weaponAnimator.gameObject;
    }

    private void OnDisable()
    {
        GetGunModel().SetActive(false);
        StopAllCoroutines();
    }
}
