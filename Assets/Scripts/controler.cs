
using UnityEngine;

public class controler : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;
    public float liftSpeed = 5f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveForward = speed * Time.deltaTime;
        rb.velocity = transform.forward * moveForward;

        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);

        float lift = Input.GetAxis("Vertical")*liftSpeed * Time.deltaTime;
        transform.Rotate(-lift, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sphere"))
        {
            Rigidbody sphereRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 direccionFuerza = (collision.transform.position - rb.position).normalized;
            sphereRb.AddForce(direccionFuerza*speed, ForceMode.Impulse);
        }
    }
}
