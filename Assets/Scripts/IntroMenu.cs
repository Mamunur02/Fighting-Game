using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("MenuScene");
        ///Loads the new scene
    }

    public void QuitButton()
    {
        Application.Quit();
        ///Closes the application
    }
}

