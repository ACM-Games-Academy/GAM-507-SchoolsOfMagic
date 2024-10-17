using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class tempCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentClass;
    [SerializeField] private playerModel playerModel;

    // Start is called before the first frame update
    void Start()
    {
        //get text thingys to work
    }

    // Update is called once per frame
    void Update()
    {
        currentClass.text = playerModel.CurrentClass;
    }
}
