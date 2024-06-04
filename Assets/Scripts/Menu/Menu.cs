

using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;
    public GameObject prelobbyPanel;
    public GameObject creditsPanel;
    public GameObject optionsPanel;
    public GameObject lobbyHostPanel;
    public GameObject lobbyWestPanel;
    public GameObject lenguagePanel;
    public GameObject soundPanel;
    public GameObject pplPanel;
    
    public void Start()
    {
        prelobbyPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        lenguagePanel.SetActive(false);
        soundPanel.SetActive(false);
        lobbyHostPanel.SetActive(false);
        lobbyWestPanel.SetActive(false);
        pplPanel.SetActive(true);
    }
    
    

    public void ViewInitialization()
    {
        _classMenuController = FindObjectOfType<MenuController>();
    }

    public void OnClickPlayButton()
    {
        // Desactiva el panel actual
        pplPanel.SetActive(false);

        // Activa el panel de Prelobby (con la etiqueta "Window2")
        prelobbyPanel.SetActive(true);
        Debug.Log("Play button clicked");
    }

    public void OnClickCreditsButton()
    {
        // Desactiva el panel actual
        pplPanel.SetActive(false);

        // Activa el panel de Créditos (con la etiqueta "Window5")
        creditsPanel.SetActive(true);
        Debug.Log("Credits button clicked");
    }

    public void OnClickOptionsButton()
    {
        // Desactiva el panel actual
        pplPanel.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window6")
        optionsPanel.SetActive(true);
        Debug.Log("Options button clicked");
    }

    public void GoBackPreLobbyButton()
    {
        // Activa el panel actual
        prelobbyPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        pplPanel.SetActive(true);
        
        Debug.Log("Go Back Credits button clicked");
    }
    public void GoBackOptionsButton()
    {
        // Activa el panel actual
        optionsPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        pplPanel.SetActive(true);
        
        Debug.Log("Go Back Options button clicked");
    }
    
    public void GoBackCreditsButton()
    {
        // Activa el panel actual
        creditsPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        pplPanel.SetActive(true);
        
        Debug.Log("Go Back Credits button clicked");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
