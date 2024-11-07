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
        
        playerController controller;

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
            controller = GetComponentInParent<playerController>();

            //this is for the amount the player will be healed and the position of each enemy
            float healAmount = 0;
            List<Vector3> enemyPos = new List<Vector3>();

            //setting all the variables from the scriptable obj
            cooldown = data.cooldown;
            radiusRange = data.radiusRange;
            damage = data.damage;
            healAmount = data.healAmount;
            healRate = data.healRate;
            speedBoost = data.speedBoost;
            boostDuration = data.boostDuration;

            //this finds all objects within a sphere
            Collider[] colliders = Physics.OverlapSphere(transform.position, radiusRange);

            //fining all the enemies within the sphere
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<TempEnemy>(out TempEnemy enemyScript) == true)
                {
                    //dealing damge to the enemy this will be changed when enemies are made
                    enemyScript.health = enemyScript.health - damage;

                    //the heal amount increases as more enmies are damaged by this ability
                    //the position of each enemy is then saved for the visual effect
                    healAmount++;
                    enemyPos.Add(enemyScript.GetComponent<Transform>().position);   
                }
            }

            if (healAmount > 0)
            {
                
            }

            Debug.Log("Enemies hit: " +  healAmount);

            StartCoroutine(healPlayer());
            visualEffect(enemyPos);
        }

        private IEnumerator healPlayer()
        {
            //while the healamount is above 0 it will heal the player
            //the healrate is the amount it will heal in one second 

            float frameHeal;

            while (healAmount > 0)
            {
                healing = true;

                frameHeal = Time.deltaTime * healRate;
                if (frameHeal > healAmount)
                {
                    controller.AddReduceValue(playerController.ValueType.Health, healAmount, false);
                    healAmount = 0;
                }
                else
                {
                    controller.AddReduceValue(playerController.ValueType.Health, healAmount, false);
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
            while (healing == true)
            {
                foreach(ParticleSystem particle in particleSyss)
                {
                    particle.transform.rotation = Quaternion.LookRotation(transform.position);
                }

                yield return new WaitForEndOfFrame();
            }

            foreach (ParticleSystem particle in particleSyss)
            {
                Destroy(particle.gameObject);
            }

            Destroy(this.gameObject);
        }
    }
}
