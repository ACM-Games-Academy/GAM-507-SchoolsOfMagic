using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{
    //this is temp hence public

    public bool isBleeding;
    public float health;

    private void Start()
    {
        isBleeding = false;
    }
}
