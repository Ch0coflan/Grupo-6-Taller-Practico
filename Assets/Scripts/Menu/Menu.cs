
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;

    void Start()
    {
        _classMenuController = FindObjectOfType<MenuController>();
        if (_classMenuController == null)
        {
            Debug.LogError("No se encontró el componente MenuController.");
        }
    }

    public void OnClick_buttons(int viewParameter)
    {
        Debug.Log("View1: número de ventana es " + viewParameter);
        if (_classMenuController != null)
        {
            _classMenuController.Evento(ref viewParameter);
        }
    }
}
