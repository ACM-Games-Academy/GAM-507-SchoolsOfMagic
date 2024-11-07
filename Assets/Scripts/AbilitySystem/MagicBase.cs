//Ewan Mason
//Template object for MagicBase objects to be correctly integrated into the MagicController

using UnityEngine;

namespace Magic
{
    public abstract class MagicBase : MonoBehaviour
    {
        public virtual void Equip() {}
        public virtual void Unequip() {}
        public abstract void Update();
    }
}
