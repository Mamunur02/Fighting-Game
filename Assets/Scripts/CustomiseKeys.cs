using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomiseKeys : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    ///A dictionary that will have a string as a key and a KeyCode as the value stored in the dictionary
    ///The name of the dictionary is keys

    public Text playerOneLeft,
        playerOneRight,
        playerOneLightPunch,
        playerOneHeavyPunch,
        playerTwoLeft,
        playerTwoRight,
        playerTwoLightPunch,
        playerTwoHeavyPunch;
    ///The different text variables that I will need to change

    private GameObject currentKey;
    ///The key that we are currently editing

    private Color32 normal = new Color32(255, 255, 255, 255);
    ///The normal colour of the keys
    private Color32 selected = new Color32(239, 116, 36, 255);
    ///The colour of the keys when clicked

    void Start()
    {
        keys.Add("PlayerOneLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneLeft", "A")));
        ///When adding the key code it will first check PlayerPrefs
        ///The key A is the default key code
        ///It converts the string value from PlayerPrefs into a key code
        keys.Add("PlayerOneRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneRight", "D")));
        keys.Add("PlayerOneLightPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneLightPunch", "O")));
        keys.Add("PlayerOneHeavyPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerOneHeavyPunch", "P")));
        keys.Add("PlayerTwoLeft", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoLeft", "LeftArrow")));
        keys.Add("PlayerTwoRight", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoRight", "RightArrow")));
        keys.Add("PlayerTwoLightPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoLightPunch", "N")));
        keys.Add("PlayerTwoHeavyPunch", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PlayerTwoHeavyPunch", "M")));
        ///Adds different key codes to the dictionary that has different tags
        playerOneLeft.text = keys["PlayerOneLeft"].ToString();
        playerOneRight.text = keys["PlayerOneRight"].ToString();
        playerOneLightPunch.text = keys["PlayerOneLightPunch"].ToString();
        playerOneHeavyPunch.text = keys["PlayerOneHeavyPunch"].ToString();
        playerTwoLeft.text = keys["PlayerTwoLeft"].ToString();
        playerTwoRight.text = keys["PlayerTwoRight"].ToString();
        playerTwoLightPunch.text = keys["PlayerTwoLightPunch"].ToString();
        playerTwoHeavyPunch.text = keys["PlayerTwoHeavyPunch"].ToString();
        ///Gets the key code from the dictionary keys
        ///Converts the key code to a string
        ///Displays that string as text to the appropriate game object
    }

    private void OnGUI()
    ///Updates quicker than the Update method
    {
        if (currentKey != null)
        ///If we have pressed one of our keys
        {
            Event e = Event.current;
            ///Creates an event and this is equal to what we have pressed
            KeyCode input = e.keyCode;
            ///Creates a variable that is equal to the keycode
            if (e.isKey
                && input != keys["PlayerOneLeft"]
                && input != keys["PlayerOneRight"]
                && input != keys["PlayerOneLightPunch"]
                && input != keys["PlayerOneHeavyPunch"]
                && input != keys["PlayerTwoLeft"]
                && input != keys["PlayerTwoRight"]
                && input != keys["PlayerTwoLightPunch"]
                && input != keys["PlayerTwoHeavyPunch"]
                && input != KeyCode.Escape
                ///Will only change the key code if it has not been used before or is not the escape key
                )
            ///If the event is a key press we need to assign a value
            {
                keys[currentKey.name] = e.keyCode;
                ///The key that we have pressed is the key code for that game object
                ///This replaces the key code with the new one
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                ///Updates the text on screen
                currentKey.GetComponent<Image>().color = normal;
                ///Returns the colour of the key to normal
                currentKey = null;
                ///No longer editing the currentKey
                return;
                ///This is so the method finishes
            }
            if (e.isKey
                && input == keys["PlayerOneLeft"]
                || input == keys["PlayerOneRight"]
                || input == keys["PlayerOneLightPunch"]
                || input == keys["PlayerOneHeavyPunch"]
                || input == keys["PlayerTwoLeft"]
                || input == keys["PlayerTwoRight"]
                || input == keys["PlayerTwoLightPunch"]
                || input == keys["PlayerTwoHeavyPunch"]
                || input == KeyCode.Escape
                ///Cehcks if the key code is in at least one of the buttons
                )
            {
                currentKey.transform.GetChild(0).GetComponent<Text>().text = keys[currentKey.name].ToString();
                ///Keeps the text as normal
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
                return;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    ///Can change the key when the button is clicked
    {
        if (currentKey != null)
        ///This prevents multiple buttons being coloured at the same time
        {
            currentKey.GetComponent<Image>().color = normal;
            ///Changes the colour of the button to normal
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
        ///Changes the colour of the button to show we are editing it
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        ///Goes through each item in the dictionary keys
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            ///For each item it gets the key/tag of that item and uses that as a tag for PlayerPrefs
            ///For each item it gets the value and converts that as a string to be saved
        }
        PlayerPrefs.Save();
        ///Saves the customisation
    }
}
