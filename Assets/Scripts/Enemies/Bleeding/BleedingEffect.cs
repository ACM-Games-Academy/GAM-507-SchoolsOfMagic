using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedingEffect : MonoBehaviour
{
    //this controls the bleeding effect. 
    //when the enemy stops bleeding it will stop
    private Enemy enemy;

    private void OnEnable()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        if (!enemy.IsBleeding)
        {
            Destroy(this.gameObject);
        }
    }
}
