using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float startTime;
    ///The starting time
    private float timeLeft;
    ///The current time left
    public Text startText;
    ///The text on the screen

    public PlayerOne playerOne;
    ///Need to update the number of rounds the players have and the round bars
    public PlayerTwo playerTwo;

    private int playerOneHealth;
    ///Will need to compare the player's health
    private int playerTwoHealth;

    private int playerOneRounds;
    ///Will need to update the player's rounds
    private int playerTwoRounds;

    public CPU cpu;

    private int opponent;

    void Start()
    {
        startTime = PlayerPrefs.GetFloat("Time");
        ///Gets the startTime from PlayerPrefs
        if (startTime == 0)
        ///If the startTime is 0 then the user chose infinite time
        {
            Destroy(this);
            ///Destroys this script
        }
        else
        {
            opponent = PlayerPrefs.GetInt("Opponent");
            ///The timer will also need to check who the opponent is
            timeLeft = startTime;
            int truncatedTimeLeft = (int)timeLeft;
            startText.text = truncatedTimeLeft.ToString();
            ///Our previous code for the Start method
        }
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        ///The time remaining decreases with time
        int truncatedTimeLeft = (int) timeLeft;
        ///The timeLeft is casted to become an integer
        if (truncatedTimeLeft < 0)
        ///When time is less than 0 then the round should end
        {
            playerOneHealth = playerOne.GetCurrentHealth();
            ///Need to get health to compare
            playerOneRounds = playerOne.GetPlayerOneRounds();
            ///Need to get the rounds to update
            if (opponent == 1)
            ///Needs to check who the opponent is to see what class to get the rounds and health from
            {
                playerTwoHealth = playerTwo.GetCurrentHealth();
                playerTwoRounds = playerTwo.GetPlayerTwoRounds();
            }
            else if (opponent == 2)
            {
                playerTwoHealth = cpu.GetCurrentHealth();
                playerTwoRounds = cpu.GetPlayerTwoRounds();
            }
            if (playerOneHealth > playerTwoHealth)
            ///This means player one has won the round
            {
                PlayerOneTimeWin();
            }
            else if (playerTwoHealth > playerOneHealth)
            {
                PlayerTwoTimeWin();
            }
            else
            ///If neither is greater it must mean a draw
            {
                TimeDraw();
            }
        }
        else
        {
            startText.text = truncatedTimeLeft.ToString();
        }
    }

    void PlayerOneTimeWin()
    {
        Debug.Log("Time win");
        ///To let us know a round has been won due to time
        playerOneRounds += 1;
        ///The player has won one more round
        playerOne.SetPlayerOneRounds(playerOneRounds);
        ///Setting the new value in the PlayerOne class
        playerOne.UpdateRoundBars();
        ///Will need to update the round bars
    }

    void PlayerTwoTimeWin()
    {
        Debug.Log("Time win");
        playerTwoRounds += 1;
        if (opponent == 1)
        ///Checks which class to set the rounds and update the round bars
        {
            playerTwo.SetPlayerTwoRounds(playerTwoRounds);
            playerTwo.UpdateRoundBars();
        }
        else if (opponent == 2)
        {
            cpu.SetPlayerTwoRounds(playerTwoRounds);
            cpu.UpdateRoundBars();
        }
    }

    void TimeDraw()
    {
        Debug.Log("Time draw");
        ///To let us know a round has been drawn due to time
        PlayerOneTimeWin();
        PlayerTwoTimeWin();
        ///Both players would have both won one more round
    }

    public float GetStartTime()
    {
        return startTime;
    }

    public void SetStartTime(float time)
    {
        startTime = time;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public void SetTimeLeft(float time)
    {
        timeLeft = time;
    }
}
