//Ewan Mason
//MonoBehaviour script controlling MagicBase objects, functionality similar to a StateMachine

using System;
using System.Collections.Generic;
using UnityEngine;

public enum MagicType { None, Blood, Metal, Nature }

[System.Serializable]
public struct MagicPrefab
{
    public string name;
    public GameObject prefab;
}

public class MagicController : MonoBehaviour
{
    public List<MagicPrefab> magicPrefabs;
    private Dictionary<MagicType, MagicBase> magics;
    [SerializeField]
    private MagicBase activeMagic;
    [SerializeField] private PlayerController controller;
    [SerializeField] private playerInput input;
    [SerializeField] string debugText;

    //Attempts to find a prefab of given name in the magicPrefabs array
    public bool TryGetPrefab(string prefabName, out GameObject gameObject)
    {
        foreach (MagicPrefab magicPrefab in magicPrefabs)
        {
            if (magicPrefab.name == prefabName)
            {
                gameObject = magicPrefab.prefab;
                return true;
            }
        }

        gameObject = null;
        return false;
    }

    //Called at runtime, initializes the magics dictionary and sets the initially equipped magic
    void Start()
    {      
        // Initialize magics dictionary
        magics = new Dictionary<MagicType, MagicBase>
        {
            { MagicType.Metal, transform.GetComponent<Metal>() },
                { MagicType.Nature, transform.GetComponent<Nature>() },
                { MagicType.Blood, transform.GetComponent<Blood>() },
        };

        switch (controller.GetCurrentClass())
        {
            case "Metal":
                EquipMagic(MagicType.Metal);
                break;
            case "Nature":
                EquipMagic(MagicType.Nature);
                break;
            case "Blood":
                EquipMagic(MagicType.Blood);
                break;

        }

    }

    private void OnEnable()
    {
        input.BloodMagic += ChangeMagicBlood;
        input.MetalMagic += ChangeMagicMetal;
        input.NatureMagic += ChangeMagicNature;
    }

    //Calls the Update method of the currently active magic
    void Update()
    {
        if (activeMagic != null)
        {
            activeMagic.MagicUpdate();
        }

        switch (controller.GetCurrentClass())
        {
            case "Metal":
                EquipMagic(MagicType.Metal);
                break;
            case "Nature":
                EquipMagic(MagicType.Nature);
                break;
            case "Blood":
                EquipMagic(MagicType.Blood);
                break;

        }
    }

    //Calls the unequip method of the currently active magic
    private void UnequipCurrentMagic()
    {
        if (activeMagic != null)
        {
            activeMagic.Unequip();
            activeMagic = null;
        }       
    }

    //Unequips the currently active magic, changes the currently active magic, and calls the Equip method of the newly active magic
    public void EquipMagic(MagicType magicType)
    {
        //Validate parsed MagicType
        if (magics.TryGetValue(magicType, out MagicBase magic))
        {
            UnequipCurrentMagic();

            //Update active magic and call the Equip method
            activeMagic = magic;
            activeMagic.Equip();
        } 
        else
        {
            Debug.LogError("Invalid ability type: " + magicType.ToString());
        }

        //Debug.Log("Magic changed to: " + magicType.ToString());
    }

    private void OnDisable()
    {
        input.BloodMagic -= ChangeMagicBlood;
        input.MetalMagic -= ChangeMagicMetal;
        input.NatureMagic -= ChangeMagicNature;
    }

    private void ChangeMagicBlood(object sender, EventArgs e)
    {
        EquipMagic(MagicType.Blood);
    }

    private void ChangeMagicNature(object sender, EventArgs e)
    {
        EquipMagic(MagicType.Nature);
    }

    private void ChangeMagicMetal(object sender, EventArgs e)
    {
        EquipMagic(MagicType.Metal);
    }
}