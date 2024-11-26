using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronDeposit : MonoBehaviour
{
    [SerializeField] private float currentIron;
    [SerializeField] private float startingIron;
    [SerializeField] private float ironYield;

    private PlayerController controller;
    private Vector3 startingScale;

    private void Start()
    {
        currentIron = startingIron;
        startingScale = transform.localScale;
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("hit by a particle");

        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player is hitting me Iron");

            if (controller.currentMagic == "Metal")
            {
                //this means its currently the metal wizard and its shot at the iron desposit
                
                float availableIron = 0;

                //this is for if the iron left in the desposit is less than what one shot gives to the player
                if (currentIron < ironYield)
                {
                    availableIron = currentIron;
                }
                else
                {
                    availableIron = ironYield;
                }


                float difference = controller.GetMaxIron() - controller.GetIron();

                if (difference < availableIron)
                {
                    controller.AddReduceValue(PlayerController.ValueType.Iron, difference, false);
                    currentIron -= difference;
                }
                else
                {
                    controller.AddReduceValue(PlayerController.ValueType.Iron, ironYield, false);
                    currentIron -= availableIron;
                }

                transform.localScale = startingScale * (currentIron / startingIron); //brackets just in case you forgot Bidmas ;)
            }
        }
    }
}
