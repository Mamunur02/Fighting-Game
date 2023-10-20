using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCustomisation : MonoBehaviour
{
    private float startTime;

    public void CustomiseTime(int val)
    ///The value is given when the user clicks on an option
    {
        if (val == 0)
        ///First value in the drop box is 0
        {
            startTime = 30f;
            ///Start time should be 30 seconds
            Debug.Log("30 seconds");
            ///Lets us know what option we picked in the console
        }
        else if (val == 1)
        {
            startTime = 60f;
            Debug.Log("60 seconds");
        }
        else if (val == 2)
        {
            startTime = 0f;
            Debug.Log("Infinite");
        }
        PlayerPrefsSetTime();
        ///Calls the PlayerPrefsSetTime method
    }

    void PlayerPrefsSetTime()
    {
        PlayerPrefs.SetFloat("Time", startTime);
        ///Sets startTime as a float in PlayerPrefs with the key Time
    }
}
