using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tempUIHealth : MonoBehaviour
{

    private PlayerController controller;
    private WeaponController weaponController;
    private TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        weaponController = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
        healthText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + controller.GetHealth() + "/" + controller.GetMaxHealth() + "\nCurrent Ammo: " + weaponController.LoadedAmmo + "/" + weaponController.MagSize;
    }
}
