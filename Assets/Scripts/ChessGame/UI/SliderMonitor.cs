using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMonitor : MonoBehaviour
{
    public Slider slider;
    public Text text;

    public void Start()
    {
        text.text = "Calculation depth: " + slider.value;
        GameManager.Instance.difficulty = (int)slider.value;
        //Adds a listener to the main slider and invokes a method when the value changes.
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        text.text = "Calculation depth: " + slider.value;
        GameManager.Instance.difficulty = (int)slider.value;
    }
}
