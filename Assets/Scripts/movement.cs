using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 500f;
    public float rotationSpeed = 100f;

    private void Update()
    {   
        // Movimiento lateral y rotaci�n
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        // Movimiento hacia adelante constante
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);


        // Rotaci�n horizontal (izquierda y derecha)
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);

        // Rotaci�n vertical (arriba y abajo)
        transform.Rotate(Vector3.right, -verticalInput * rotationSpeed * Time.deltaTime);

        // Movimiento con las teclas de flecha
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Aumentar la velocidad hacia adelante
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Disminuir la velocidad hacia adelante (o mover hacia atr�s si es necesario)
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Rotar hacia la izquierda
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Rotar hacia la derecha
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
