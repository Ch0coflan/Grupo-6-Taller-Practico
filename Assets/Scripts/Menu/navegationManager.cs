using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class navegationManager : MonoBehaviour
{


    public void GoTo(ref int parameter)
    {
        Debug.Log("NavegationManaher: el modulo1 solicita navegar a la ventana: " + parameter);
    }
}
