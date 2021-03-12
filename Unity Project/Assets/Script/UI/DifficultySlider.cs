using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySlider : MonoBehaviour
{
    [SerializeField] Text t;
    [SerializeField] Slider slider;

    public void UpdateText()
    {
        t.text = "Difficulty: " + slider.value;
    }
}
