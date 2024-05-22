using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
   public float initialUpwardForce = 20.0f; // Fuerza inicial hacia arriba
   public float transitionTime = 2.0f; // Tiempo para cambiar al comportamiento del globo
   public float customGravity = -9.8f; // Gravedad personalizada más débil
   public float airResistance = 2.0f; // Resistencia del aire alta

   private Rigidbody rb;
   private bool initialForceApplied = false;

   void Start()
   {
      rb = GetComponent<Rigidbody>();
      rb.useGravity = false;
      rb.drag = airResistance;
   }

   void FixedUpdate()
   {
      if (!initialForceApplied)
      {
         // Aplicar la gravedad personalizada
         rb.AddForce(new Vector3(0, customGravity, 0), ForceMode.Acceleration);
      }
   }

   // Llamar a este método cuando el avión golpee la pelota
   public void ApplyImpactForce(Vector3 direction, float force)
   {
      // Aplica una fuerza inicial hacia arriba más fuerte
      rb.AddForce(Vector3.up * initialUpwardForce, ForceMode.Impulse);
      // Aplica la fuerza de impacto en la dirección especificada
      rb.AddForce(direction * force, ForceMode.Impulse);
      // Comienza la corutina para cambiar al comportamiento del globo
      StartCoroutine(TransitionToBalloonBehavior());
   }

   private IEnumerator TransitionToBalloonBehavior()
   {
      // Marcar que la fuerza inicial ha sido aplicada
      initialForceApplied = true;
      // Esperar el tiempo de transición
      yield return new WaitForSeconds(transitionTime);
      // Cambiar al comportamiento del globo
      initialForceApplied = false;
   }
   
}
