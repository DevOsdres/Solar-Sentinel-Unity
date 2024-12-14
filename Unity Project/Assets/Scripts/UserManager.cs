using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;

public class UserManager : MonoBehaviour
{
    public TMP_InputField inputField_UserName;
    public Button buttonSave;
    //public Button buttonReturn;

    //Datos del jugador
    public static string playerName;  //Nombre del jugador
    public static int playerScore = 0;  //Puntuación inicial
    public static int playerLevel = 1;  //Nivel inicial

    void Start()
    {
        buttonSave.onClick.AddListener(SavePlayerData);
    }

    void SavePlayerData()
    {
        //Validar que el nombre no esté vacío
        if (string.IsNullOrEmpty(inputField_UserName.text))
        {
            Debug.Log("El nombre no puede estar vacío.");
            return;
        }

        //Guardar el nombre
        playerName = inputField_UserName.text;

        //Cargar nivel jugable
        SceneManager.LoadScene("Level1"); 
    }

    public static void UpdateScore(int points)
    {
        playerScore += points;
    }

    public static void UpdateLevel(int level)
    {
        playerLevel = level;
    }
}
