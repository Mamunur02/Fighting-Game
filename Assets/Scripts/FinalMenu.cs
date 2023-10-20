using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalMenu : MonoBehaviour
{
    public void RematchButton()
    {
        SceneManager.LoadScene("FightingScene");
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void VersusButton()
    {
        SceneManager.LoadScene("VersusScene");
    }
}
