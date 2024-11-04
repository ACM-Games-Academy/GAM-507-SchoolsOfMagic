using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public bool IsArmoured;
    public bool IsStaggered;

    public float staggerDuration = 1.0f;

    public TMPro.TextMeshPro staggered;
   


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
        Debug.Log("Staggered over!");
        staggered.text = (" ");
    }
}
