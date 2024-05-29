using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Airplane
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallPhysics : MonoBehaviour
    {
        public float buyoancyForce = 5f;
        public float dragCoefficient = 0.5f;
        public Vector3 randomForce = new Vector3(0.1f, 0.1f, 0.1f);
        public float customGravity = -9.8f;
        public float pushForce;
        public float masPushForce;
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        public void FixedUpdate()
        {
            Vector3 gravity = Vector3.up * customGravity;
            rb.AddForce(gravity);
            Vector3 buoyancy = Vector3.up * buyoancyForce;
            rb.AddForce(buoyancy);

            Vector3 airCurrent = new Vector3(
                Random.Range(-randomForce.x, randomForce.x),
                Random.Range(-randomForce.y, randomForce.y),
                Random.Range(-randomForce.z, randomForce.z)
                );

            rb.AddForce( airCurrent );
            Vector3 drag = -rb.velocity * dragCoefficient;
            rb.AddForce(drag);
        }

    

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Vector3 hitPoint = collision.contacts[0].point;
                Vector3 pushDirection = (transform.position - hitPoint).normalized;
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                float forceMagnitude = Mathf.Clamp(playerRb.velocity.magnitude * pushForce, pushForce, masPushForce);
                rb.AddForce(pushDirection * forceMagnitude, ForceMode.Impulse);
            }
        }

    }
}
