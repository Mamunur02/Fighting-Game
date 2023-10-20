using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    public Slider slider;
    ///So I can change the value of the slider

    public PlayerOne playerOne;
    public PlayerTwo playerTwo;

    public CPU cpu;

    private int opponent;

    private void Start()
    {
        opponent = PlayerPrefs.GetInt("Opponent");
        slider.value = 1f;
        ///When the round bar is initialised it would be set so no one has won a round
    }

    public void RoundWin()
    { 
        slider.value -= 1f;
        playerOne.RoundReset();
        ///Decreases the slider value to 0 to reveal the round winning colour
        if (opponent == 1)
        {
            playerTwo.RoundReset();
        }
        else if (opponent == 2)
        {
            cpu.RoundReset();
        }
        ///To create a new round
    }
}
