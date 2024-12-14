using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton
    public int score = 0; // Puntaje actual
    public int winCondition = 30; // Enemigos necesarios para ganar

    private GameHUD gameHUD;
    public LevelComplete levelComplete;  // Nueva referencia al script LevelComplete

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameHUD = FindObjectOfType<GameHUD>();
        gameHUD.UpdateScore(score); // Actualizar el puntaje inicial en el HUD
        levelComplete = FindObjectOfType<LevelComplete>(); // Obtener el componente LevelComplete
    }

    public void AddScore(int points)
    {
        score += points;
        gameHUD.UpdateScore(score); // Actualizar puntaje en el HUD

        if (score >= winCondition)
        {
            WinLevel();
        }
    }

    void WinLevel()
    {
        Debug.Log("¡Nivel Completado!");

        // Detener el tiempo
        Time.timeScale = 0f;

        // Llamar a CompleteLevel para desactivar el HUD y activar el LevelCompletedCanvas
        if (levelComplete != null)
        {
            levelComplete.CompleteLevel();
        }
    }
}