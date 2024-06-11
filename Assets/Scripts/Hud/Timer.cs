using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer = 0f; // Variable que controla el tiempo restante en segundos
    private float initialTime;
    public TMP_Text textoTimer; // Referencia al componente TextMeshProUGUI que muestra el tiempo restante

    private void Start()
    {
        initialTime = timer;
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
            Time.timeScale = 0f;
        }
    }
    
    public void RestartTimer()
    {
        Time.timeScale = 1f; // Reanuda el tiempo
        timer = initialTime;
        
        // Calcula los minutos y segundos iniciales
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        
        // Formatea el tiempo en MM:SS
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        textoTimer.text =  timeString;
    }
}
