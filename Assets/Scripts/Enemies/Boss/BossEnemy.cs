using System;
using UnityEngine;
using UnityEngine.Events;

public class BossEnemy : Enemy
{
    [SerializeField] private BossStats bossStats;
    public int pillarsActive = 0;
    [SerializeField] private float turnSpeed = 0.5f;
    [SerializeField] private GameObject bossUI;

    [Header("Wwise Music Events")]
    public AK.Wwise.Event bossMusicStart;
    public AK.Wwise.Event mainMusicStop;
    public AK.Wwise.Event bossMusicStop;
    public AK.Wwise.Event bossIdlestart;
    public AK.Wwise.Event bossIdleStop;
    public AK.Wwise.Event bossDeathSound;
    public GameObject wwiseGlobal;
    private bool bossDeathTracker = false;

    [HideInInspector] public Transform player;
    private bool bossStarted = false;

    public bool canBossRotate = true;

    private event EventHandler bossHealthChange;
    public event EventHandler bossDeath;


    // Declare the UnityEvent
    //public UnityEvent OnHealthInitialized = new UnityEvent();

    private void OnEnable()
    {
        bossIdlestart.Post(this.gameObject);
        player = GameObject.FindWithTag("Player").transform;
        bossUI.SetActive(false);

        // Initialize health 
        EnemyInitiate(bossStats.health, bossStats.isArmoured);
        //OnHealthInitialized?.Invoke(); 
    }

    private void Update()
    {
        if (pillarsActive == 4)
        {
            BossStart();
        }

        if (canBossRotate)
        {
            RotateTowardsPlayer();
        }
    }

    public void BossStart()
    {

        if (!bossStarted)
        {
            mainMusicStop.Post(wwiseGlobal);
            bossMusicStart.Post(this.gameObject);
            bossStarted = true;
            bossUI.SetActive(true);

            // Reset boss health when fight begins
            ResetBossHealth();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void ResetBossHealth()
    {
        ResetHealth(bossStats.health); // Reset to full health
        //OnHealthInitialized?.Invoke();
    }

    protected override void EnemyDeath()
    {
        base.EnemyDeath();
        //Prevent death before fight
        if (!bossStarted)
        {
            return;
        }
        if (!bossDeathTracker)
        {
            bossMusicStop.Post(this.gameObject);
            bossIdleStop.Post(this.gameObject);
            bossDeathSound.Post(this.gameObject);
            bossDeathTracker = true;
        }
        bossDeath.Invoke(this, EventArgs.Empty);
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - 40f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 0.5f);
        GetComponentInChildren<Animator>().Play("Boss Idle");
    }
}
