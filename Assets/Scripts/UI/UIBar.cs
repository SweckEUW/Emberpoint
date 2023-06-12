using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIBar : MonoBehaviour
{

    public Slider slider;
    public GameObject blink;
    private float valueCache = 100;

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
        blink.GetComponent<Slider>().maxValue = value;
        valueCache = value;
    }

    public void SetValue(float value)
    {
        slider.value = value;
        BlinkTween(valueCache, value);
        valueCache = value;
    }
    
    public void SetValue(float value, bool isEnhanced)
    {
        slider.value = value;
        blink.GetComponent<Slider>().value = value;
    }

    private void BlinkTween(float startValue, float endValue)
    {
        LeanTween.value(blink, startValue, endValue, 0.15f)
            .setEaseInSine()
            .setOnUpdate((value) =>
            {
                blink.GetComponent<Slider>().value = value;
            })
            .setDelay(0.2f);
    }
}
