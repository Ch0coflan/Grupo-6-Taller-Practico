using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionWall : MonoBehaviour
{
        public float reboundForce = 5f; // La fuerza con la que el jugador rebota
        private Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.CompareTag("Pared"))
        {
            Vector3 reboundDirection = (transform.position - collision.contacts[0].point).normalized;
            rb.AddForce(reboundDirection * reboundForce, ForceMode.Impulse);
        }
        }
    }
