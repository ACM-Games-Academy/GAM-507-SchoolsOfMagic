//Ewan Mason
//Template object for MagicBase objects to be correctly integrated into the MagicController

namespace Magic
{
    public abstract class MagicBase
    {
        public virtual void Equip() {}
        public virtual void Unequip() {}
        public abstract void Update();
    }
}
