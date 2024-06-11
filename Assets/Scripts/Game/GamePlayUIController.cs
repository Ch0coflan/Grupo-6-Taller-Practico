using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Usar TextMeshPro

public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController Instance;

    [SerializeField] private TMP_Text scoreTextA; // Referencia al texto para mostrar el puntaje de A
    [SerializeField] private TMP_Text scoreTextB; // Referencia al texto para mostrar el puntaje de B

    private int scoreA = 0; // Variable para almacenar el puntaje de A
    private int scoreB = 0; // Variable para almacenar el puntaje de B

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Método para sumar puntos a A
    public void AddScoreA(int points)
    {
        scoreA += points;
        UpdateScoreUIA();
    }

    // Método para sumar puntos a B
    public void AddScoreB(int points)
    {
        scoreB += points;
        UpdateScoreUIB();
    }

    // Método para actualizar el texto del puntaje de A en el HUD
    private void UpdateScoreUIA()
    {
        scoreTextA.text = "Score A: " + scoreA;
    }

    // Método para actualizar el texto del puntaje de B en el HUD
    private void UpdateScoreUIB()
    {
        scoreTextB.text = "Score B: " + scoreB;
    }
}
