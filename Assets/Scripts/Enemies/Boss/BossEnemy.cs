using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private BossStats bossStats;
    public int pillarsActive = 4;
    [SerializeField] private float turnSpeed = 0.5f;
    [SerializeField] private GameObject bossUI;

    [HideInInspector] public Transform player;
    private bool bossStarted = false;

    public bool canBossRotate = true;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        bossUI.SetActive(false);
        EnemyInitiate(bossStats.health, bossStats.isArmoured);
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
    }

    protected override void EnemyDeath()
    {
        //Prevent death before fight
        if (!bossStarted)
        {
            return;
        }

        base.EnemyDeath();
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - 40f, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 0.5f);
        bossUI.SetActive(false);
        GetComponentInChildren<Animator>().Play("Boss Idle");
    }
}
