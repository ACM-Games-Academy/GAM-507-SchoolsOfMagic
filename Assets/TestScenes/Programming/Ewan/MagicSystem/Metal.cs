/*
Author:     Ewan Mason
Purpose:    IMagic object for Metal magic
*/

namespace Magic
{
    public class Metal : IMagic
    {
        public MagicType magicType { get; private set; }

        /*
        Method:     Metal
        Function:   Constructs a new Metal object
        */
        public Metal()
        {
            magicType = MagicType.Metal;
        }

        /*
        Method:     Equip
        Function:   Called when Metal is equipped in a MagicController
        */
        public override void Equip()
        {
            
        }

        /*
        Method:     Unequip
        Function:   Called when Metal is unequipped in a MagicController
        */
        public override void Unequip()
        {
            
        }

        /*
        Method:     Update
        Function:   Called when Metal is updated in a MagicController
        */
        public override void Update()
        {
            
        }
    }
}