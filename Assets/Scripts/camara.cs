using UnityEngine;

public class camara : MonoBehaviour
{
    public Transform target; // El avi�n
    public Vector3 offset = new Vector3(50f, 50f, -50f); // Desfase de la c�mara desde el avi�n

    private void LateUpdate()
    {
        if (target != null)
        {
            // Calcula la posici�n objetivo de la c�mara arriba y detr�s del avi�n
            Vector3 desiredPosition = target.position - target.forward * offset.z + target.up * offset.y;

            // Establece la posici�n de la c�mara
            transform.position = desiredPosition;

            // Hace que la c�mara mire hacia adelante desde la posici�n del avi�n
            transform.LookAt(target);
        }
    }
}
