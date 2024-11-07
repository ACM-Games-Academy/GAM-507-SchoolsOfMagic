//Ewan Mason
//MagicBase object for Metal magic

using UnityEngine;

namespace Magic
{
    public class Metal : MagicBase
    {
        public const MagicType magicType = MagicType.Metal;
        private MagicController magicController;
        private GameObject ironGripPrefab;

        //Constructs a new Metal object
        public Metal(MagicController _magicController)
        {
            magicController = _magicController;
            _magicController.TryGetPrefab("IronGripPrefab", out ironGripPrefab);
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
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameObject grip = Object.Instantiate(ironGripPrefab);
            }
        }
    }
}