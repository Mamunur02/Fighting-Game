using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void VersusButton()
    {
        SceneManager.LoadScene("VersusScene");
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
