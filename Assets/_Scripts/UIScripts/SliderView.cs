using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderView : MonoBehaviour
{
    private const int TO_PERSENTS = 100;
    
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private TextMeshProUGUI _valueLabel;

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.onValueChanged.AddListener(ValueChanged);
    }

    private void ValueChanged(float value)
    {
        int valueInt = (int)(value * TO_PERSENTS);
        
        _valueLabel.text = NumToStringBuffer.GetIntToStringHash(valueInt) + '%';
    }

    public void SetValue(float value)
    {
        _slider.value = value;
        ValueChanged(value);
    }

    public void OnValueChanged(UnityAction<float> action)
    {
        _slider.onValueChanged.AddListener(action);
    }
}
