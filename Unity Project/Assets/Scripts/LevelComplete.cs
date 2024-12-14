using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject HUDCanvas;
    public GameObject levelCompletedCanvas;
    public Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(HandleNextStep);
    }

    public void CompleteLevel()
    {
        if (HUDCanvas != null)
        {
            HUDCanvas.SetActive(false);
        }

        if (UserManager.playerLevel == 2)
        {
            LoadVictoryScene();
        }
        else
        {
            if (levelCompletedCanvas != null)
            {
                levelCompletedCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void HandleNextStep()
    {
        Time.timeScale = 1f;

        if (UserManager.playerLevel == 1)
        {
            LoadNextLevel();
        }
        else if (UserManager.playerLevel == 2)
        {
            LoadVictoryScene();
        }
    }

    private void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level2");
    }

    private void LoadVictoryScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Victory");
    }
}