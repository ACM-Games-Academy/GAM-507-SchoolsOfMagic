using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

namespace Magic
{
    public class Blood : MagicBase
    {
        private playerInput playerInput;
        private playerController controller;

        public const MagicType magicType = MagicType.Blood;
        private MagicController magicController;
        GameObject primaryPrefab;

        [SerializeField] private BloodPrimaryData primaryData;

        float primaryCooldown;

        //Constructs a new Metal object
        public Blood(MagicController _magicController)
        {
            magicController = _magicController;
            _magicController.TryGetPrefab("Exsanguination", out primaryPrefab);

            playerController playerController = this.GetComponent<playerController>();
            playerInput = new playerInput();

            playerInput.primaryAbil += primaryFired;
        }

        // Called when Metal is equipped in MagicController object
        public override void Equip()
        {
            primaryCooldown = 0;
        }

        //Called when Metal is unequipped in a MagicController object
        public override void Unequip()
        {

        }

        //Called when Metal is updated in a MagicController object
        public override void Update()
        {
            primaryCooldown -= Time.deltaTime;
            if (primaryCooldown < 0)
            {
                primaryCooldown = 0;
            }
        }

        private void primaryFired(object sender, EventArgs e)
        {
            if (primaryCooldown == 0)
            {
                GameObject.Instantiate(primaryPrefab, controller.transform);
                primaryCooldown = primaryData.cooldown;
            }       
        }
    }
}
