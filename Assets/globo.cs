using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globo : MonoBehaviour
{
    public float fuerzaAscendente = 9.8f;
    public float drag = 1f;
    public float fuerzaImpulso = 10f;
   Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.mass = 1f;
        rb.drag = 0.2f;
        rb.angularDrag = 0.05f;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    private void FixedUpdate()
    {
        Vector3 fuerzaHaciaArriba = Vector3.up * fuerzaAscendente;
        rb.AddForce(fuerzaHaciaArriba);

        Vector3 velocidad = rb.velocity;
        Vector3 fuerzaResistencia = -velocidad * drag;
        rb.AddForce(fuerzaResistencia);    
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        Vector3 normal = contactPoint.normal;

        Vector3 impulse = -2 * Vector3.Dot(rb.velocity, normal)*normal;
        rb.AddForce(impulse, ForceMode.Impulse);
    }
}
