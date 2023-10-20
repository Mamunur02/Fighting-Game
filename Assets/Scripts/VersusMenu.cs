using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersusMenu : MonoBehaviour
{
    public void FightButton()
    {
        SceneManager.LoadScene("FightingScene");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
