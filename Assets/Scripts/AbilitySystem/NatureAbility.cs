using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureAbility : MonoBehaviour
{
    public playerController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DamageEnemies();
    }

    public void DamageEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 5)
            {
                enemy.health -= 50;
                enemy.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 5);
            }
        }
    }
}
