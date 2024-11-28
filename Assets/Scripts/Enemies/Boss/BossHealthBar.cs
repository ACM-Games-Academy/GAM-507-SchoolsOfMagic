using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private BossEnemy bossEnemy;

    private void Start()
    {
        if (bossEnemy != null)
        {
            // Subscribe to the event
            bossEnemy.OnHealthInitialized.AddListener(Initialize);
        }
    }

    public void Initialize()
    {
        // Initialize the health bar values
        healthSlider.maxValue = bossEnemy.Health;
        healthSlider.value = bossEnemy.Health;
    }

    private void Update()
    {
        healthSlider.value = bossEnemy.Health;
    }
}
