using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private BossEnemy bossEnemy;
    public AK.Wwise.Event bossDeathSound;
    public AK.Wwise.Event bossIdleStop;
    public AK.Wwise.Event bossIdle;
    private bool hasDied = false;

    private void Start()
    {
        bossIdle.Post(this.gameObject);

        if (bossEnemy != null)
        {
            // Subscribe to the event
            //bossEnemy.OnHealthInitialized.AddListener(Initialize);
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
        if((bossEnemy.Health <= 0) && (hasDied == false))
        {
            hasDied = true;
            bossIdleStop.Post(this.gameObject);
            bossDeathSound.Post(this.gameObject);
        }
    }
}
