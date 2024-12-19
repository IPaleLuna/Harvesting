using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

public class CountdownView : NetworkBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdownText;
    
    [SerializeField]
    private float _fadeDuration = 0.5f;
    
    [Header("Colors")]
    [SerializeField]
    private Color originalColor;
    [SerializeField]
    private Color targetColor;
    
    [Header("Scale")]
    [SerializeField]
    private Vector3 _originalScale;
    [SerializeField]
    private Vector3 _targetScale;
    
    private readonly TextFade _textFade = new();
    private readonly TextScaleChanger _textScaleChanger = new();

    [ClientRpc]
    public void SetCountdownTextClientRpc(string text)
    {
        countdownText.color = originalColor;
        countdownText.transform.localScale = _originalScale;
        
        countdownText.text = text;
        
        _textScaleChanger.AnimScale(countdownText.transform, _fadeDuration, _targetScale);
        _textFade.AnimText(countdownText, _fadeDuration, targetColor);
    }
}
