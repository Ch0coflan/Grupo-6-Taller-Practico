using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    public float speed = 1.0f;  
    public float rotationSpeed = 1.0f;
    public Rigidbody rb;
    
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            // Calcula la dirección del movimiento
            Vector3 movementDirection = Vector3.forward;

            // Multiplica por la velocidad y el tiempo delta
            Vector3 movement = movementDirection * speed * Time.fixedDeltaTime;

            // Mueve el Rigidbody utilizando MovePosition
            rb.MovePosition(rb.position + movement);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Vector3 movementDirection = -Vector3.back;
            Vector3 movement = movementDirection * -speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    
}
