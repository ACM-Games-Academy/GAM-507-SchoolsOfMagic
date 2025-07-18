using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusAbility : MonoBehaviour
{
    public PlayerController controller;
    public float lifetime = 1;
    public AnimationCurve sizeCurve;

    [Header("Cactus Sound")]
    public AK.Wwise.Event cactusSlice;

    // Start is called before the first frame update
    void Start()
    {
        DamageEnemies();
    }

    private void OnEnable()
    {
        cactusSlice.Post(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        float size = sizeCurve.Evaluate(1 - lifetime);
        transform.localScale = new Vector3(size, size, size);
    }

    public void DamageEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) < 5)
            {
                enemy.GiveDamage(50, false);
                enemy.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 5);
            }
        }
    }
}
