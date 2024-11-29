using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PillarObjective : SpawnEventHandler
{
    private Transform player;
    [SerializeField] private TextMeshPro interactPrompt;
    private BossEnemy bossEnemy;
    private float interactRange = 12f;         // Distance the player has to be to start the countdown
    private bool isPlayerInRange = false;
    private float timeInRange = 0f;
    private float requiredTimeToActivate = 5f; // Time the player needs to stand near the pillar
    private bool hasActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get player position
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Assign boss enemy script
        GameObject bossObject = GameObject.Find("Boss");
        bossEnemy = bossObject.GetComponent<BossEnemy>();
    }

    private void Update()
    {
        // Check player distance
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactRange)
        {
            if (!hasActivated)
            {
                isPlayerInRange = true;

                int remainingTime = Mathf.CeilToInt(Mathf.Clamp(requiredTimeToActivate - timeInRange, 0f, requiredTimeToActivate));
                interactPrompt.text = remainingTime.ToString();
            }
        }
        else
        {
            isPlayerInRange = false;
            timeInRange = 0f;     // Reset the timer if the player leaves
            if (!hasActivated)
            {
                interactPrompt.text = "X";
            }
        }

        // Count time when player is close
        if (isPlayerInRange && !hasActivated)
        {
            timeInRange += Time.deltaTime;

            if (timeInRange >= requiredTimeToActivate)
            {
                ActivatePillar();
            }
        }
    }

    private void ActivatePillar()
    {
	completion = true;
        hasActivated = true; // Prevent further activations
        bossEnemy.pillarsActive++;
        interactPrompt.text = " "; // Clear the text after activation
    }
}
