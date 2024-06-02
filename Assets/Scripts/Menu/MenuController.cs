
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private navegationManager _classNavegationManager;

    void Start()
    {
        _classNavegationManager = FindObjectOfType<navegationManager>();
        if (_classNavegationManager == null)
        {
            Debug.LogError("No se encontró el componente navegationManager.");
        }
    }

    public void Evento(ref int controllerParameter)
    {
        Debug.Log("Controller1: la vista me envía este número de ventana " + controllerParameter);
        if (_classNavegationManager != null)
        {
            _classNavegationManager.GoTo(ref controllerParameter);
        }
    }
}
