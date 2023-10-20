using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundCustomisation : MonoBehaviour
{
    private int rounds;
    ///Stores number of rounds

    public void CustomiseRounds(int val)
    {
        if (val == 0)
        ///Means they have chosen the first optiono
        {
            rounds = 1;
            Debug.Log("1 round");
        }
        else if (val == 1)
        {
            rounds = 2;
            Debug.Log("2 rounds");
        }
        else if (val == 2)
        {
            rounds = 3;
            Debug.Log("3 rounds");
        }
        PlayerPrefsSetRounds();
    }
     
    void PlayerPrefsSetRounds()
    {
        PlayerPrefs.SetInt("Rounds", rounds);
        ///Sets the number of rounds in PlayerPrefs as an integer with the string key Rounds
    }
}
