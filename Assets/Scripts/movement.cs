using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float speed = 500f;
    public float maxspeed = 500f;
    public float minspeed = 5f;
    public float rootspeed1 = 10f;
    public float rootspeed2 = 20f;

    private Rigidbody rb;

    void Start()
    {
        // Obtener el componente Rigidbody del objeto
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Manejar el movimiento hacia adelante basado en la velocidad
        rb.velocity = transform.forward * speed * Time.deltaTime;

        // Incrementar velocidad si se presiona la tecla D
        if (Input.GetKeyDown(KeyCode.D) && speed <= maxspeed)
        {
            speed += 2;
        }

        // Decrementar velocidad si se presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E) && speed >= minspeed)
        {
            speed -= 2;
        }

        // Rotar hacia la izquierda si se presiona la tecla LeftArrow o A
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ApplyTorque(Vector3.down * rootspeed1);
        }

        // Rotar hacia la derecha si se presiona la tecla RightArrow o D
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ApplyTorque(Vector3.up * rootspeed1);
        }

        // Rotar hacia abajo si se presiona la tecla DownArrow o S
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            ApplyTorque(Vector3.left * rootspeed1);
        }

        // Rotar hacia arriba si se presiona la tecla UpArrow o W
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            ApplyTorque(Vector3.right * rootspeed1);
        }
    }

    // Método para aplicar fuerzas de torque al Rigidbody
    void ApplyTorque(Vector3 torque)
    {
        rb.AddTorque(torque * Time.deltaTime, ForceMode.VelocityChange);
    }
}