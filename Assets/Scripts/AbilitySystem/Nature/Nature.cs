using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nature : MagicBase
{
    public const MagicType magicType = MagicType.Nature;
    private MagicController magicController;
    public GameObject cactusPrefab;
    public float cooldown = 5;

    //private GameObject ironGripPrefab;

    //Constructs a new Metal object
    public Nature(MagicController _magicController)
    {
        magicController = _magicController;
        //_magicController.TryGetPrefab("IronGrip", out ironGripPrefab);
    }

    // Called when Metal is equipped in MagicController object
    public override void Equip()
    {

    }

    //Called when Metal is unequipped in a MagicController object
    public override void Unequip()
    {

    }

    //Called when Metal is updated in a MagicController object
    public override void MagicUpdate()
    {
        //TODO: Write in input controls for Metal abilities
        //Example of how an input control and subsequent prefab instantiation may look
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    GameObject grip = Object.Instantiate(ironGripPrefab);
        //}

        cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && cooldown <= 0)
        {
            GameObject.Instantiate(cactusPrefab, transform.position, Quaternion.identity);
            cooldown = 5;
        }
    }
}

