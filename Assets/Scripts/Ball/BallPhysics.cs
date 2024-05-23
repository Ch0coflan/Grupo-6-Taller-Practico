using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
 
   public float constanteElastica = 100f;
   public float longitudNatural = 0.5f;
   

   
   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("ObjetoCae"))
      {
         AplicarFuerzaRestauradora(collision.gameObject);
      }
   }
   private void AplicarFuerzaRestauradora(GameObject ObjetoCae)
   {
      Rigidbody rbObjetoCae = ObjetoCae.GetComponent<Rigidbody>();

      if (rbObjetoCae != null)
      {
         // es la distancia que hay entre el objeto y la superficie (distancia relativa)
         float posicionRelativa = ObjetoCae.transform.position.y - transform.position.y;

         //calcular la fuerza elastica segun la ley de hooke

         float fuerzaElastica = constanteElastica * (posicionRelativa - longitudNatural);

         //APLICAR FUERZA

         rbObjetoCae.AddForce(Vector3.up * fuerzaElastica, ForceMode.Impulse);
      }
   }
   
}
