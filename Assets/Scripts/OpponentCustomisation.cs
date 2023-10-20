using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCustomisation : MonoBehaviour
{
    private int opponent;
    ///Type of opponent will be decided by an integer value

    public void CustomiseOpponent(int val)
    {
        if (val == 0)
        {
            opponent = 1;
            Debug.Log("Player");
        }
        else if (val == 1)
        {
            opponent = 1;
            Debug.Log("Player");
        }
        else if (val == 2)
        {
            opponent = 2;
            Debug.Log("CPU");
        }
        PlayerPrefsSetOpponent();
    }

    void PlayerPrefsSetOpponent()
    {
        PlayerPrefs.SetInt("Opponent", opponent);
    }
}
