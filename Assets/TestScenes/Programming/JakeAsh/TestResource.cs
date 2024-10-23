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
        
    }
}
