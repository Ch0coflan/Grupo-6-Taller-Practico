using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private MenuController _classMenuController;
    public GameObject prelobbyPanel;
    public GameObject creditsPanel;
    public GameObject optionsPanel;
    public GameObject lobbyPanel;
    public GameObject lenguagePanel;
    public GameObject soundPanel;
    public GameObject pplPanel;
    public Image credits;
    [SerializeField] private GameObject selectedPlane = null;
    public Button startButton;
    public List<Button> buttons;

    public ParticleSystem buttonClickParticles; // Asigna esto en el inspector de Unity
    


    public void Start()
    {
        AudioManager.Instance.PlayMusic("Inicio");

        SetUpSoundButton();
        
        if (!selectedPlane)
        {
            // Mostrar un mensaje al jugador para seleccionar un avión antes de comenzar
            return;
        }
        
        prelobbyPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        lenguagePanel.SetActive(false);
        soundPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        pplPanel.SetActive(true);

        startButton.interactable = false;
    }

    private void SetUpSoundButton()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(
                () =>
                {
                    AudioManager.Instance.PlaySfx("ClickButton");
                }
            );
        }
    }

    private void Update()
    {
        var color = credits.color;
        color.a = 255f;
        credits.color = color;

        credits.color = Color.white;
    }

    public void SelectPlane(GameObject plane)
    {
        Debug.Log("SelectPlane called. Plane: " + plane + ", Start Button: " + startButton);
        selectedPlane = plane;
        if (startButton)
        {
            // Habilita el botón de inicio cuando se selecciona un avión
            startButton.interactable = true;
        }
        else
        {
            Debug.LogError("Start button is null");
        }
    }
    
    public void ViewInitialization()
    {
        _classMenuController = FindObjectOfType<MenuController>();
    }

    public void OnClickPlayButton()
    {
        // if (!selectedPlane)
        // {
        //     // Mostrar un mensaje al jugador para seleccionar un avión antes de comenzar
        //     return;
        // }
        
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

    public void OnClickCreateEnterCodeButton()
    {
        // Desactiva el panel actual
        prelobbyPanel.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window6")
        lobbyPanel.SetActive(true);
        Debug.Log("Create Code button clicked");
    }

    public void OnClickLenguageButton()
    {
        // Desactiva el panel actual
        optionsPanel.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window6")
        lenguagePanel.SetActive(true);
        Debug.Log("Lenguage button clicked");
    }

    public void OnClickSoundButton()
    {
        // Desactiva el panel actual
        optionsPanel.SetActive(false);

        // Activa el panel de Opciones (con la etiqueta "Window6")
        soundPanel.SetActive(true);
        Debug.Log("Sound button clicked");
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

    public void GoBackLenguageButton()
    {
        // Activa el panel actual
        lenguagePanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        optionsPanel.SetActive(true);
        
        Debug.Log("Go Back Lenguage button clicked");
    }

    public void GoBackSoundButton()
    {
        // Activa el panel actual
        soundPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        optionsPanel.SetActive(true);
        
        Debug.Log("Go Back Sound button clicked");
    }

    public void GoBackLobbyButton()
    {
        // Activa el panel actual
        lobbyPanel.SetActive(false);
        
        // Desactiva el panel de Opciones (con la etiqueta "Window6")
        prelobbyPanel.SetActive(true);
        
        Debug.Log("Go Back Lobby button clicked");
    }
    
    public void QuitButton()
    {
        Application.Quit();
    }
}
