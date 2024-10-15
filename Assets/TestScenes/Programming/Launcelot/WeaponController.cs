using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    WeaponBaseTest currentGun;

    // Start is called before the first frame update
    void Start()
    {
        UpdateGun();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentGun.Fire();
            Debug.Log("Fired");
        }
    }

    void UpdateGun()
    {
        currentGun = GetComponentInChildren<WeaponBaseTest>();
    }
}
