using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResource : MonoBehaviour
{
   playerModel playerModel;

    
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GetComponent<playerModel>();
        Debug.Log("blood = " + playerModel.getBlood());
        Debug.Log("iron = " + playerModel.getIron());

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            playerModel.addBlood(20f);
            Debug.Log("blood increase. blood = " + playerModel.getBlood());

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerModel.reduceBlood(20f);
            Debug.Log("blood decrease. blood = " + playerModel.getBlood());

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerModel.addIron(5f);
            Debug.Log("iron increase. iron = " + playerModel.getIron());

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerModel.reduceIron(5f);
            Debug.Log("iron decrease. iron = " + playerModel.getIron());
        }

    }




}
