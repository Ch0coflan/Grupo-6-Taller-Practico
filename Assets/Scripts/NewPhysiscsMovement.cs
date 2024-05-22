using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPhysiscsMovement : MonoBehaviour
{
    [Header("Estadisticas del avion")]
    public float incrementoDeAceleracion = 0.1f;
    public float empujeMaximo;
    public float capacidadDeRespuesta = 10f;
    public float aceleracion = 1f;
    private float roll;
    private float pitch;
    private float yaw;
    public float modificadorDeRespuesta{
        get
        {
            return (rb.mass / 10f) * capacidadDeRespuesta;
        }
    }

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Controles()
    {
        roll = Input.GetAxis("Roll");
        pitch = Input.GetAxis("Pitch");
        yaw = Input.GetAxis("Yaw");

        if(Input.GetKey(KeyCode.Space))
        {
            aceleracion += incrementoDeAceleracion;
        }else if (Input.GetKey(KeyCode.LeftControl))
        {
            aceleracion -= incrementoDeAceleracion;
        }

        aceleracion = Mathf.Clamp(aceleracion, 0f, 100f);
    }

    private void Update()
    {
        Controles();
    }

    private void FixedUpdate()
    {
       rb.AddForce(transform.forward * empujeMaximo * aceleracion);
        rb.AddTorque(transform.up * yaw * modificadorDeRespuesta);
        rb.AddTorque(transform.right * pitch * modificadorDeRespuesta);
        rb.AddTorque(transform.forward * roll * modificadorDeRespuesta);
        //rb.velocity = Vector3.forward;
    }

  
}
