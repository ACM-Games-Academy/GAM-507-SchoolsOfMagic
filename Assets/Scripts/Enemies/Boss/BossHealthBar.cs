using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private BossEnemy bossEnemy;

    private void Start()
    {
        healthSlider.value = bossEnemy.Health;
        healthSlider.maxValue = bossEnemy.Health;
    
    }
    public void Update()
    {
        healthSlider.value = bossEnemy.Health;
    }
}
