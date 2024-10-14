/*
Author:     Ewan Mason
Function:   MonoBehaviour script controlling IMagic objects, functionality similar to a StateMachine
*/

using System.Collections.Generic;
using UnityEngine;

namespace Magic
{
    public enum MagicType { None, Blood, Metal, Nature, Arcane }

    public class MagicController : MonoBehaviour
    {
        private Dictionary<MagicType, IMagic> magics;
        private IMagic activeMagic;

        /*
        Method:     Awake
        Function:   Called at runtime, initializes the magics dictionary and sets the initially equipped magic
        */
        void Awake()
        {
            // Initialize magics dictionary
            magics = new Dictionary<MagicType, IMagic>
            {
                { MagicType.Metal, new Metal() },
            };

            EquipMagic(MagicType.Metal);
        }

        /*
        Method:     Update
        Function:   Calls the Update method of the currently active magic (if any)
        */
        void Update()
        {
            if (activeMagic != null)
            {
                activeMagic.Update();
            }
        }

        /*
        Method:     UnequipCurrentMagic
        Function:   Calls the Unequip method of the currently active magic (if any) and sets activeMagic to null   
        */
        private void UnequipCurrentMagic()
        {
            if (activeMagic != null)
            {
                activeMagic.Unequip();
                activeMagic = null;
            }
        }

        /*
        Method:     EquipMagic
        Function:   Unequips the currently active magic (if any), updates the currently active magic, and calls the Equip method of the newly active magic
        */
        public void EquipMagic(MagicType magicType)
        {
            // Validate parsed MagicType
            if (magics.TryGetValue(magicType, out IMagic magic))
            {
                UnequipCurrentMagic();

                // Update active magic and call the Equip method
                activeMagic = magic;
                activeMagic.Equip();
            } 
            else
            {
                Debug.LogError("Invalid ability type: " + magicType.ToString());
            }
        }
    }
}