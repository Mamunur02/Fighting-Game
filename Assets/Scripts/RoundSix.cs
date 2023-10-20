using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSix : MonoBehaviour
{
    public Slider slider;
    public PlayerOne playerOne;
    public PlayerTwo playerTwo;
    public CPU cpu;

    private int rounds;
    private int opponent;

    private void Start()
    {
        rounds = PlayerPrefs.GetInt("Rounds");
        if (rounds == 1 || rounds == 2)
        {
            Destroy(gameObject);
        }
        else
        {
            opponent = PlayerPrefs.GetInt("Opponent");
            slider.value = 1f;
        }
    }

    public void RoundWinSix()
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
