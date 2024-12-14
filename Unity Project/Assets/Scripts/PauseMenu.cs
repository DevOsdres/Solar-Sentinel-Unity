using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Canvas References")]
    public GameObject pauseMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject controlsCanvas;
    public GameObject gameOverCanvas;
    public GameObject levelCompletedCanvas;

    private bool isPaused = false;

    void Update()
    {
        /*if (levelCompletedCanvas.activeSelf)
        {
            return;
        }*/

        // Solo permitir abrir el menú de pausa si no estamos en el Game Over
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverCanvas.activeSelf && !levelCompletedCanvas.activeSelf)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuCanvas.SetActive(true);
        //optionsCanvas.SetActive(false);
        //controlsCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}