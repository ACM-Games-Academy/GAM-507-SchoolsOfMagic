using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBaseTest : MonoBehaviour
{
    public float damage;
    public float firerate;

    public ParticleSystem particleSys;
    public Transform raycastPosition;

    public WeaponData gunModel;

    private void OnEnable()
    {
        damage = gunModel.damage;
        firerate = gunModel.fireRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Fire()
    {
        particleSys.Play();
    }

    public void Reload()
    {

    }
}
