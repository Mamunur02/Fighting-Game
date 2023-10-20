using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    ///Boolean variable to check if the game is paused

    public GameObject pauseMenuUI;
    ///This is the PauseMenu game object

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        ///Checks for the escape key input
        {
            if (GameIsPaused)
            ///Checks if the game is already paused
            {
                Resume();
                ///If it is already paused then by pressing escape again it leaves the pause screen
            }
            else
            {
                Pause();
                ///If not paused then it pauses the game
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        ///Gets rid of the pause screen
        Time.timeScale = 1f;
        ///Time returns to normal
        GameIsPaused = false;
        ///Our variable is set to false
        Debug.Log("Game is resumed");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        ///Activates the pause screen
        Time.timeScale = 0f;
        ///Time returns to normal
        GameIsPaused = true;
        ///Our variable is set to true
        Debug.Log("Game is paused");
    }

    public void MenuButton()
    {
        Time.timeScale = 1f;
        ///Time returns to normal
        SceneManager.LoadScene("MenuScene");
        ///Goes to the main menu
    }

    public void VersusButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VersusScene");
    }
}

