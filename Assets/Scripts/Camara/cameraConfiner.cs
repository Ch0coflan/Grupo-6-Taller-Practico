using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraConfiner : MonoBehaviour
{ 
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    
}
