using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIBar_Fill : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponentInParent<Slider>();
    }

    public void SetMaxValue(float val)
    {
        slider.maxValue = val;
        slider.value = val;
    }

    public void SetValue(int newVal)
    {
        slider.value = newVal;
    }

    public void SetValue(float newVal)
    {
        slider.value = newVal;
    }

}
