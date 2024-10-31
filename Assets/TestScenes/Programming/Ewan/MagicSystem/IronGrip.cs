//Ewan Mason
//Script for the IronGrip ability

using System.Collections;
using UnityEngine;

namespace Magic
{
    public class IronGrip : MonoBehaviour
    {
        public float radius = 10f;
        public float power = 2f;
        public float maxAxisVelocity = 20f;
        public float explosionChargeTime = 3f;
        public float maxExplosionAxisVelocity = 40f;
        public LayerMask negativeMask;
        private bool pull = true;

        //Called by a Metal IMagic object, causes enemies within the effective area to be exploded outwards, taking damage relative to the distance from the center
        public IEnumerator Explode()
        {
            pull = false;

            // Get all objects within radius and explode them outwards
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    
                }
            }

            yield return null;
        }

        //Called each frame, detects enemies within effective area and pulls them in
        void FixedUpdate()
        {
            if (!pull) return;

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider collider in colliders)
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 dir = (transform.position - rb.position).normalized;
                    Vector3 velocity = rb.velocity + dir * power;

                    // Constrain velocity
                    velocity = new Vector3(
                        Mathf.Clamp(velocity.x, -maxAxisVelocity, maxAxisVelocity),
                        Mathf.Clamp(velocity.y, -maxAxisVelocity, maxAxisVelocity),
                        Mathf.Clamp(velocity.z, -maxAxisVelocity, maxAxisVelocity)
                    );

                    rb.velocity = velocity;
                }
            }
        }

        //Called each type Gizmos call to be redrawn, used for debugging
        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
