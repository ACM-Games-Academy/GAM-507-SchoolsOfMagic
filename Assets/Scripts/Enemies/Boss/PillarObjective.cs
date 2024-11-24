using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PillarObjective : MonoBehaviour
{
    private Transform player;
    [SerializeField] private TextMeshPro interactPrompt;
    private BossEnemy bossEnemy;
    private playerInput playerInput;
    public float interactRange = 5;
    private bool canInteract = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get player position and interact button
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInput = player.GetComponent<playerInput>();
        playerInput.interactPressed += OnInteractPressed;
        //Assign boss enemy script
        GameObject bossObject = GameObject.Find("Boss");
        bossEnemy = bossObject.GetComponent<BossEnemy>();
    }

    private bool hasInteracted = false;

    private void OnInteractPressed(object sender, System.EventArgs e)
    {
        if (canInteract && !hasInteracted)
        {
            hasInteracted = true; // Prevent further interactions
            bossEnemy.pillarsActive++;

            // Debug.Log("Interacted!");
            //Debug.Log("Active pillars: " + bossEnemy.pillarsActive);


        }
        else if (!canInteract)
        {
            //Debug.Log("Player is too far or has alread interacted with this pillar.");
        }
    }


    private void FixedUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= interactRange)
        {
            if (hasInteracted) { interactPrompt.text = (""); }
            else
            {
             
                interactPrompt.text = ("E");
                canInteract = true;
            }
        }
    
        else
        {
            interactPrompt.text = (" ");
            canInteract = false;
            
        }
    }



}
