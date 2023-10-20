using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundThree : MonoBehaviour
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
        ///Gets the rounds
        if (rounds == 1 || rounds == 2)
        ///If the rounds option is 1 or 2 then the third round bar is not required
        {
            Destroy(gameObject);
        }
        else
        {
            opponent = PlayerPrefs.GetInt("Opponent");
            slider.value = 1f;
        }
    }

    public void RoundWinThree()
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
