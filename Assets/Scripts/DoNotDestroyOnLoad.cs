using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    static bool instance = false;
    ///Static so the value does not get wiped

    private void Awake()
    ///Called when the script is being loaded
    {
        if (instance == false)
        ///Means game object hasn't been created yet
        {
            DontDestroyOnLoad(transform.gameObject);
            ///The game object wouldn't be destroyed when a new scene occurs
            instance = true;
            ///To show one has now been created
        }
        else
        {
            Destroy(gameObject);
            ///There was already one so destroy new game object
        }
    }
}
