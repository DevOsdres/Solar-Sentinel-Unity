using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [Header("Vida")]
    public Image[] livesIcons;
    private int currentLives = 3;
    private int playerScore = 0;

    [Header("Datos del jugador")]
    public TMP_Text playerNameText;
    public TMP_Text playerScoreText;
    public TMP_Text playerLevelText;
    public TMP_Text activeWeaponText;

    void Start()
    {
        // Configurar HUD inicial
        playerNameText.text = "Player: " + UserManager.playerName;
        playerScoreText.text = "Score: " + UserManager.playerScore;
        playerLevelText.text = "Level: " + UserManager.playerLevel;
        activeWeaponText.text = "Weapon: Blaster";
    }

    //Método para actualizar la vida
    public void UpdateLives(int lives)
    {
        currentLives = lives;
        for (int i = 0; i < livesIcons.Length; i++)
        {
            livesIcons[i].enabled = i < currentLives;
        }
    }

    //Método para actualizar el puntaje
    public void UpdateScore(int newScore)
    {
        UserManager.playerScore = newScore;
        playerScoreText.text = "Score: " + UserManager.playerScore;
    }

    //Método para actualizar el arma activa
    public void UpdateActiveWeapon(string weaponName)
    {
        activeWeaponText.text = "Weapon: " + weaponName;
    }

    //Método para actualizar el nivel
    public void UpdateLevel(int level)
    {
        UserManager.playerLevel = level;
        playerLevelText.text = "Level: " + UserManager.playerLevel;
    }
}