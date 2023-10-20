using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    ///This variable called slider will be used to use the methods of the Slider component
    public Gradient gradient;
    ///This is how the colour of the health bar changes
    public Image fill;
    ///The health that we see in the health bar is an image

    public void SetMaxHealth(int health)
    ///Function to set the value of the slider to the maximum
    {
        slider.maxValue = health;
        ///Maximum value of the slider is the maximum health of the player
        slider.value = health;
        ///Current value of the slider is the maximum health of the player
        gradient.Evaluate(1f);
        ///Colour of the image is set to the maximum gradient
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        ///Current value of the slider is the current health of the player
        fill.color = gradient.Evaluate(slider.normalizedValue);
        ///Changes the value of the gradient to change the colour
    }
}


