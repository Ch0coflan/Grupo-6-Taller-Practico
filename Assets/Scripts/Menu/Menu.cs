
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;
    private GameObject _prelobbyPanel;
    private GameObject _creditsPanel;
    private GameObject _optionsPanel;
    

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
    
    private void Start()
    {
        // Busca los paneles por su etiqueta y asígnalos a las variables
        _prelobbyPanel = GameObject.FindWithTag("Window2");
        _creditsPanel = GameObject.FindWithTag("Window5");
        _optionsPanel = GameObject.FindWithTag("Window6");

        // Verifica si se encontraron los paneles
        if (_prelobbyPanel == null || _creditsPanel == null || _optionsPanel == null)
        {
            Debug.LogError("Algunos paneles no se encontraron o no tienen la etiqueta correcta.");
        }
    }
    
    public void OnClickPlayButton()
    {
        // Desactiva el panel actual
        gameObject.SetActive(false);

        // Activa el panel de Prelobby (con la etiqueta "Window2")
        GameObject.FindWithTag("Window2").SetActive(true);
        Debug.Log("Play button clicked");
    }

    public void OnClickCreditsButton()
    {
        // Desactiva el panel actual
        gameObject.SetActive(false);

        // Activa el panel de Créditos (con la etiqueta "Window3")
        GameObject.FindWithTag("Window5").SetActive(true);
        Debug.Log("Credits button clicked");
    }

    public void OnClickOptionsButton()
    {
        // Desactiva el panel actual
        gameObject.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window4")
        GameObject.FindWithTag("Window6").SetActive(true);
        Debug.Log("Options button clicked");
    }
}
