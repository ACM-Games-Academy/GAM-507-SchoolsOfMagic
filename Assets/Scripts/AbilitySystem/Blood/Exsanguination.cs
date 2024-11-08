using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Magic
{
    public class Exsanguination : MonoBehaviour
    {
        //this will need to send out a collider detect all enemies that are bleeding within range
        //it will then deal damage then heal the player accordingly depending on how many enemies are hit 
        [SerializeField] BloodPrimaryData data;
        
        private PlayerController controller;
        private movementController movementController;

        [SerializeField] private ParticleSystem pullEffect;

        private float cooldown;
        private float radiusRange;
        private float damage;
        private float healAmount;
        private float healRate;
        private float speedBoost;
        private float boostDuration;

        private bool healing;

        // Start is called before the first frame update
        void OnEnable()
        {
            //setting all the variables from the scriptable obj
            cooldown = data.cooldown;
            radiusRange = data.radiusRange;
            damage = data.damage;
            healAmount = data.healAmount;
            healRate = data.healRate;
            speedBoost = data.speedBoost;
            boostDuration = data.boostDuration;

            Debug.Log(healAmount + "healAmout");
            StartCoroutine(startAbility());
        }

        public void InitAbil(PlayerController Controller, movementController MovementController)
        {
            controller = Controller;
            movementController = MovementController;
        }

        private IEnumerator startAbility()
        {
            yield return new WaitForFixedUpdate();           

            //this is for the position of each enemy
            List<Vector3> enemyPos = new List<Vector3>();            

            //this is for counting how much health will be healed
            int healCounter = 0;

            //this finds all objects within a sphere
            Collider[] colliders = null;
            colliders = Physics.OverlapSphere(transform.position, radiusRange);
            

            foreach (Collider collider in colliders)
            {
                Debug.Log("Collider");

                if (collider.gameObject.TryGetComponent<TempEnemy>(out TempEnemy enemyScript) == true)
                {
                    //dealing damge to the enemy this will be changed when enemies are made
                    enemyScript.health = enemyScript.health - damage;

                    //the heal amount increases as more enmies are damaged by this ability
                    //the position of each enemy is then saved for the visual effect
                    healCounter++;
                    enemyPos.Add(enemyScript.GetComponent<Transform>().position);

                    Debug.Log("Enemy hit: " + healCounter);
                }
            }

            healAmount *= healCounter;
            Debug.Log("Healcounter: " + healCounter + " Heal Amount: " + healAmount);

            if (healAmount > 0)
            {
                StartCoroutine(movementController.addSpeedModT(speedBoost, boostDuration));
                StartCoroutine(healPlayer());
                visualEffect(enemyPos);
            }
            else
            {
                Debug.Log("NO HITS");
                Destroy(this.gameObject);               
            }
        }

        private IEnumerator healPlayer()
        {
            Debug.Log("Started healing: " + healAmount);
            //while the healamount is above 0 it will heal the player
            //the healrate is the amount it will heal in one second 

            float frameHeal;

            while (healAmount > 0)
            {
                healing = true;

                frameHeal = Time.deltaTime * healRate;
                if (frameHeal > healAmount)
                {
                    controller.AddReduceValue(PlayerController.ValueType.Health, healAmount, false);
                    healAmount = 0;
                }
                else
                {
                    controller.AddReduceValue(PlayerController.ValueType.Health, frameHeal, false);
                    healAmount -= frameHeal;
                }
                yield return new WaitForEndOfFrame();
            }

            healing = false;
        }

        private void visualEffect(List<Vector3> enemyPos)
        {
            //first im going to instantiate a particle system at each position of each enemy
            //each of these will be facing towards the player

            ParticleSystem[] particleSystems = new ParticleSystem[enemyPos.Count];

            int i = 0;
            foreach (Vector3 vec in enemyPos)
            {           
                particleSystems[i] = Instantiate(pullEffect, vec, Quaternion.LookRotation(transform.position));
                i++;
            }

            StartCoroutine(updateParticle(particleSystems));
        }

        private IEnumerator updateParticle(ParticleSystem[] particleSyss)
        {
            Debug.Log("updating particle rotation");

            while (healing == true)
            {
                foreach(ParticleSystem particle in particleSyss)
                {
                    particle.transform.LookAt(transform.position);  
                }

                yield return new WaitForEndOfFrame();
            }

            foreach (ParticleSystem particle in particleSyss)
            {
                Destroy(particle.gameObject);
            }

            Destroy(this.gameObject);

            Debug.Log("finished updating particles");
        }
    }
}
