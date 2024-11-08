using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResource : MonoBehaviour
{
   PlayerController playerController;

    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            playerController.AddReduceValue(PlayerController.ValueType.Blood, 20f, false);
            Debug.Log("blood increase. blood = " + playerController.GetBlood());

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.AddReduceValue(PlayerController.ValueType.Blood, -20f, false);
            Debug.Log("blood decrease. blood = " + playerController.GetBlood());

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.AddReduceValue(PlayerController.ValueType.Iron, 5f, false);
            Debug.Log("iron increase. iron = " + playerController.GetIron());

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerController.AddReduceValue(PlayerController.ValueType.Iron, -5f, false);
            Debug.Log("iron decrease. iron = " + playerController.GetIron());
        }

    }




}
