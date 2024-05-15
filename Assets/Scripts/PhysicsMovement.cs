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
       // Movement();
        Rotation();
    }

    /*void Movement()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            // Calcula la dirección del movimiento
            Vector3 movementDirection = Vector3.forward;

            // Multiplica por la velocidad y el tiempo delta
            Vector3 movement = movementDirection * speed * Time.fixedDeltaTime;

            // Mueve el Rigidbody utilizando MovePosition
            rb.AddForce(rb.position + movement);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Vector3 movementDirection = -Vector3.back;
            Vector3 movement = movementDirection * -speed * Time.fixedDeltaTime;
            rb.AddForce(rb.position + movement);
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            Debug.Log("giro a la derecha");
            Vector3 movementDirection = Vector3.right;
            Vector3 movement = movementDirection * speed * Time.fixedDeltaTime;
            rb.AddForce(rb.position + movement);

            
        }
        else if (horizontal < 0)
        {
            Debug.Log("giro a la izquierda");
            Vector3 movementDirection = -Vector3.left;
            Vector3 movement = movementDirection * -speed * Time.fixedDeltaTime;
            rb.AddForce(rb.position + movement);
        }
    }*/

    void Rotation()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        rb.AddTorque(Vector3.up * inputHorizontal * rotationSpeed);

        // Movimiento hacia adelante
        float inputVertical = Input.GetAxis("Vertical");
        rb.AddForce(transform.forward * inputVertical * speed);
    }

    
}
