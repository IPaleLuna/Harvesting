using System;
using Harvesting.UI.ManualConnection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ManualConnectionView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _ipInputField;
    [SerializeField]
    private TMP_InputField _portInputField;

    private TMP_InputField.OnChangeEvent _onIpChanged;
    private TMP_InputField.OnChangeEvent _onPortChanged;
    
    public TMP_InputField.OnChangeEvent OnIpChanged => _onIpChanged;
    public TMP_InputField.OnChangeEvent OnPortChanged => _onPortChanged;

    private void Start()
    {
        _onIpChanged = _ipInputField.onValueChanged;
        _onPortChanged = _portInputField.onValueChanged;
    }
}
