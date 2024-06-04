
using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;
    public GameObject _prelobbyPanel;
    public GameObject _creditsPanel;
    public GameObject _optionsPanel;
    public GameObject _lobbyHostPanel;
    public GameObject _lobbyWestPanel;
    public GameObject _lenguagePanel;
    public GameObject _soundPanel;
    public GameObject _pplPanel;
    
    public void Start()
    {
        _prelobbyPanel.SetActive(false);
        _creditsPanel.SetActive(false);
        _optionsPanel.SetActive(false);
        _lenguagePanel.SetActive(false);
        _soundPanel.SetActive(false);
        _lobbyHostPanel.SetActive(false);
        _lobbyWestPanel.SetActive(false);
        _pplPanel.SetActive(true);
    }
    
    

    public void ViewInitialization()
    {
        _classMenuController = FindObjectOfType<MenuController>();
    }

    public void OnClickPlayButton()
    {
        // Desactiva el panel actual
        _pplPanel.SetActive(false);

        // Activa el panel de Prelobby (con la etiqueta "Window2")
        _prelobbyPanel.SetActive(true);
        Debug.Log("Play button clicked");
    }

    public void OnClickCreditsButton()
    {
        // Desactiva el panel actual
        _pplPanel.SetActive(false);

        // Activa el panel de Créditos (con la etiqueta "Window5")
        _creditsPanel.SetActive(true);
        Debug.Log("Credits button clicked");
    }

    public void OnClickOptionsButton()
    {
        // Desactiva el panel actual
        _pplPanel.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window6")
        _optionsPanel.SetActive(true);
        Debug.Log("Options button clicked");
    }

    public void GoBackOptionsButton()
    {
        // Activa el panel actual
        _optionsPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        _pplPanel.SetActive(true);
        
        Debug.Log("Go Back Options button clicked");
    }
    
    public void GoBackCreditsButton()
    {
        // Activa el panel actual
        _creditsPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        _pplPanel.SetActive(true);
        
        Debug.Log("Go Back Credits button clicked");
    }
}
