using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleGroupScript : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<OptionalIntDataHolder> OnModeChange = new();


    private List<Toggle> _toggles;

    private void Start()
    {
        _toggles = new(GetComponentsInChildren<Toggle>());

        _toggles.ForEach(item =>
        {
            item.onValueChanged.AddListener(isOn =>
            {
                if (isOn) OnModeChange.Invoke(item.GetComponent<OptionalIntDataHolder>());
            });
        });
    }
}