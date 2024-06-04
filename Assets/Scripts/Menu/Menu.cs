
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;
    private GameObject _prelobbyPanel;
    private GameObject _creditsPanel;
    private GameObject _optionsPanel;

    public void ViewInitialization()
    {
        _classMenuController = FindObjectOfType<MenuController>();
    }

    public void OnClickPlayButton()
    {
        // Desactiva el panel actual
        gameObject.SetActive(false);

        // Activa el panel de Prelobby (con la etiqueta "Window2")
        _prelobbyPanel.SetActive(true);
        Debug.Log("Play button clicked");
    }

    public void OnClickCreditsButton()
    {
        // Desactiva el panel actual
        gameObject.SetActive(false);

        // Activa el panel de Créditos (con la etiqueta "Window5")
        _creditsPanel.SetActive(true);
        Debug.Log("Credits button clicked");
    }

    public void OnClickOptionsButton()
    {
        // Desactiva el panel actual
        gameObject.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window6")
        _optionsPanel.SetActive(true);
        Debug.Log("Options button clicked");
    }
}
