using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsUIManager : Singleton<PointsUIManager>
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    private float sliderCurrentValue;
    private float sliderMaxValue;
    
    public void SetSliderValue(float value)
    {
        if (slider != null)
        {
            slider.value = value;
            sliderCurrentValue = value;
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("Slider reference is not set in PointsUIManager.");
        }
    }
    
    public void SetSliderMaxValue(float maxValue)
    {
        if (slider != null)
        {
            slider.maxValue = maxValue;
            sliderMaxValue = maxValue;
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("Slider reference is not set in PointsUIManager.");
        }
    }

    private void UpdateUI()
    {
        sliderText.text = $"{sliderCurrentValue}/{sliderMaxValue}";
    }
}
