using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuSpin : MonoBehaviour
{
    public float spinSpeed_Y = 1f;
    
    void Update()
    {
        transform.Rotate(0, spinSpeed_Y * Time.deltaTime, 0);
    }
}
