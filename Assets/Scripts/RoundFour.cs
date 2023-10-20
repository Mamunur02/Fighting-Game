using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundFour : MonoBehaviour
{
    public Slider slider;
    public PlayerOne playerOne;
    public PlayerTwo playerTwo;
    public CPU cpu;

    private int opponent;

    private void Start()
    {
        opponent = PlayerPrefs.GetInt("Opponent");
        slider.value = 1f;
    }

    public void RoundWinFour()
    {
        slider.value -= 1f;
        playerOne.RoundReset();
        if (opponent == 1)
        {
            playerTwo.RoundReset();
        }
        else if (opponent == 2)
        {
            cpu.RoundReset();
        }
    }
}
