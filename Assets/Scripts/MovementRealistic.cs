using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRealistic : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    
    public float Throttle { get; private set; }
    public Vector3 Velocity { get; private set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    
    public void SetThrottle(float throttle)
    {
        Throttle = Mathf.Clamp(throttle, 0.5f, 1f);
    }
    
    void UpdateThrottle()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetThrottle(1f);
        }
        else
        {
            SetThrottle(0.5f);
        }
        
        Throttle = Mathf.Lerp(Throttle, 0.5f, Time.deltaTime);
    }
}
