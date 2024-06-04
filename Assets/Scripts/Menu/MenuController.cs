
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private navegationManager _classNavegationManager;
    private Menu _classMenu;

    public void Start()
    {
        _classNavegationManager = FindObjectOfType<navegationManager>();
        _classMenu = FindObjectOfType<Menu>();
        _classMenu.ViewInitialization();
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
