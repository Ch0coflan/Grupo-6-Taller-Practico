
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;
    

    public void ViewInitialitation()
    {
        _classMenuController = FindObjectOfType<MenuController>();
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
