using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iman : MonoBehaviour
{
    public Transform triggerGoal; // Aseg�rate de asignar este campo en el inspector de Unity con el objeto triggerGoal
    public float fuerzaDeAtraccion = 5f;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pelota")) // Comprueba si el objeto tiene el tag "pelota"
        {
            Vector3 direccion = triggerGoal.position - other.transform.position; // Calcula la direcci�n hacia el triggerGoal
            other.GetComponent<Rigidbody>().AddForce(direccion * fuerzaDeAtraccion); // Aplica una fuerza en la direcci�n del triggerGoal
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pelota")) // Comprueba si el objeto tiene el tag "pelota"
        {
            Debug.Log("Pelota detectada!"); // Env�a un mensaje a la consola
        }
    }
}
