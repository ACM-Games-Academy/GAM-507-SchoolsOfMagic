/*
Author:     Ewan Mason
Purpose:    Interface for IMagic objects to be correctly integrated into the MagicController
*/

namespace Magic
{
    public abstract class IMagic
    {
        public virtual void Equip() {}
        public virtual void Unequip() {}
        public abstract void Update();
    }
}
