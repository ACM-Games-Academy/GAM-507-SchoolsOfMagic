using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public PlayerController player;
    public GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;
        int LayersToIgnore = (LayerMask.GetMask("Default"));
        if (Physics.CheckSphere(transform.position, 0.25f, LayersToIgnore))
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        LayersToIgnore = (LayerMask.GetMask("Player"));
        if (Physics.CheckSphere(transform.position, 0.25f, LayersToIgnore))
        {
            player?.giveDamage(10);
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
