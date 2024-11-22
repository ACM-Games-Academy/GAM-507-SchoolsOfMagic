using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyOld : MonoBehaviour
{
    public bool IsArmoured;
    private bool IsStaggered;
    public bool IsBleeding;

    public float staggerDuration = 1.0f;
    public float bleedDuration = 1.0f;

    //Temp UI for effects
    public TMPro.TextMeshPro staggered;
    public TMPro.TextMeshPro bleeding;

    //========== Stagger ==========
    public void Stagger()
    {
        IsStaggered = true;
        Debug.Log("Staggered!");
        StartCoroutine(StaggerCoroutine());
        staggered.text = ("Staggered!");
    }

    private IEnumerator StaggerCoroutine()
    {
        yield return new WaitForSeconds(staggerDuration);
        IsStaggered = false;
        staggered.text = (" ");
    }

    //========== Bleed ==========
    public void Bleed()
    {
        IsBleeding = true;
        StartCoroutine(BleedingCoroutine());
        bleeding.text = ("Bleeding!");
    }
    private IEnumerator BleedingCoroutine()
    {
        yield return new WaitForSeconds(bleedDuration);
        IsBleeding = false;
        bleeding.text = (" ");
    }
}
