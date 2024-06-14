using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 0f; // Variable que controla el tiempo restante en segundos
    private float initialTime;
    public TMP_Text textoTimer;
    public GameObject gameOverPanel; // Referencia al componente TextMeshProUGUI que muestra el tiempo restante

    private void Start()
    {
        initialTime = timer;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Disminuye el tiempo restante
        timer -= Time.deltaTime;
        
        // Calcula los minutos y segundos restantes
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        
        // Formatea el tiempo en MM:SS
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        // Actualiza el texto del timer
        textoTimer.text =  timeString;

        // Si el tiempo llega a 0, termina el juego
        if (timer <= 0f)
        {
            gameOverPanel.SetActive(true);
        }
    }
    
    
}
