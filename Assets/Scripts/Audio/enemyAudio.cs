using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyerEnemyAudio : MonoBehaviour
{
    [Header("Flyer Sounds")]
    public AK.Wwise.Event flyerFlap;
    public AK.Wwise.Event flyerSpawn;
    public AK.Wwise.Event flyerAttack;
    
    [Header("Universal Enemy Sounds")]
    public AK.Wwise.Event enemyDeathThud;
    public AK.Wwise.Event hitMarker;
    public AK.Wwise.Event hitMarkerArmour;

    [Header("Melee Enemy Sounds")]
    public AK.Wwise.Event meleeSpawn;
    public AK.Wwise.Event swarmerMove;
    public AK.Wwise.Event swarmerAttack;
    public AK.Wwise.Event armouredMove;
    public AK.Wwise.Event armouredAttack;

    [Header("Tank Enemy Sounds")]
    public AK.Wwise.Event tankSpawn;
    public AK.Wwise.Event tankMove;
    public AK.Wwise.Event tankAttack;

    public void Enable()
    {
        if (this.gameObject.CompareTag("Heavy Enemy"))
        {
            tankSpawn.Post(this.gameObject);
        }
        if (this.gameObject.CompareTag("Flying Enemy"))
        {
            flyerSpawn.Post(this.gameObject);
        }
        if (this.gameObject.CompareTag("Melee Enemy"))
        {
            meleeSpawn.Post(this.gameObject);
        }
    }
    public void PlayTankMove()
    {
        tankMove.Post(this.gameObject);
    }
    public void PlayTankAttack()
    {
        tankAttack.Post(this.gameObject);
    }
    public void PlayFlyerFlap()
    {
        flyerFlap.Post(this.gameObject);
    }
    public void PlayFlyerAttack()
    {
        flyerAttack.Post(this.gameObject);
    }
    public void PlaySwarmerMove()
    {
        swarmerMove.Post(this.gameObject);
    }
    public void PlaySwarmerAttack()
    {
        swarmerAttack.Post(this.gameObject);
    }
    public void PlayerArmouredMove()
    {
        armouredMove.Post(this.gameObject);
    }
    public void PlayerArmouredAttack()
    {
        armouredAttack.Post(this.gameObject);
    }
}
