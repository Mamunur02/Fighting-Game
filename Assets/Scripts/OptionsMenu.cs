using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void InstructionButton()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void ControlsButton()
    {
        SceneManager.LoadScene("ControlsScene");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
