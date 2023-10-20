using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTwo : MonoBehaviour
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
        ///Gets the number of rounds the game has
        if (rounds == 1)
        ///If its one round then the second round bar is not required
        {
            Destroy(gameObject);
            ///Destroys the game object
        }
        else
        {
            opponent = PlayerPrefs.GetInt("Opponent");
            slider.value = 1f;
            ///Previous initialisation code
        }
    }

    public void RoundWinTwo()
    {
        slider.value -= 1f;
        playerOne.RoundReset();
        if (opponent == 1)
        ///Checks the opponent to see what class it should call the RoundReset method
        {
            playerTwo.RoundReset();
        }
        else if (opponent == 2)
        {
            cpu.RoundReset();
        }
    }
}
