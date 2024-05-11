using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform target; // El avión
    public Vector3 offset = new Vector3(50f, 50f, -50f); // Desfase de la cámara desde el avión

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posición objetivo de la cámara arriba y detrás del avión
            Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;

            // Establece la posición de la cámara
            transform.position = desiredPosition;

            // Hace que la cámara mire hacia adelante desde la posición del avión
            transform.LookAt(target);
        }
    }
}
