using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public string weaponName;

    public damageType type;

    public enum damageType
    {
        normal, fire, ice, lightning, poison, metal, nature, blood
    }

    // Start is called before the first frame update
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void HeldUpdate(playerScript player)
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
    }
}
