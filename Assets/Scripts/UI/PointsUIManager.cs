using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsUIManager : Singleton<PointsUIManager>
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    private float sliderCurrentValue;
    private float sliderMaxValue;
    
    public void SetSliderValue(float value)
    {
        if (slider != null)
        {
            slider.value = Mathf.Round(value);
            sliderCurrentValue = Mathf.Round(value);
            UpdateSliderUI();
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
            slider.maxValue = Mathf.Round(maxValue);
            sliderMaxValue = Mathf.Round(maxValue);
            UpdateSliderUI();
        }
        else
        {
            Debug.LogWarning("Slider reference is not set in PointsUIManager.");
        }
    }

    public void SetMultiplierValue()
    {
        UpdateMultiplierUI();
    }

    private void UpdateSliderUI()
    {
        sliderText.text = $"{sliderCurrentValue}/{sliderMaxValue}";
    }

    private void UpdateMultiplierUI()
    {
        multiplierText.text = $"{DropAreaManager.Instance.GetMultiplierValue()}";
    }
}
