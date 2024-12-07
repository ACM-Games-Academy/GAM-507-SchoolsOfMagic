using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public Transform projectileSpawnPoint;
    public PlayerController player;
    public Rigidbody rb;
    public float targetDistance = 3;
    public float projectileCooldown = 3;
    public float projectileTimer = 0;
    public AK.Wwise.Event flyerAttack;

    public EnemyProjectile projectilePrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        EnemyInitiate(150, false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if (Vector3.Distance(transform.position, player.transform.position) < targetDistance)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * -2, Time.deltaTime * 10);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, transform.forward * 2, Time.deltaTime * 10);
        }

        projectileTimer += Time.deltaTime;
        if (projectileTimer >= projectileCooldown)
        {
            projectileTimer = 0;
            flyerAttack.Post(this.gameObject);
            EnemyProjectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.player = player;
            projectile.transform.LookAt(player.transform);
        }
    }

    protected override void EnemyDeath()
    {
        base.EnemyDeath();

        Destroy(this.gameObject);
    }

    protected override void GiveStagger()
    {
        base.GiveStagger();
        //do nothing lol
    }
}
