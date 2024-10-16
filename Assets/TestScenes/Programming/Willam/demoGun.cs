using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demoGun : weapon
{
    public ParticleSystem bullets;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void HeldUpdate(playerMovement player)
    {
        base.HeldUpdate(player);
        if (Input.GetMouseButtonDown(0))
        {
            bullets.Emit(1);
        }
        if (Input.GetMouseButtonDown(1))
        {
            player.velocity.y = 20;
        }
    }
}
