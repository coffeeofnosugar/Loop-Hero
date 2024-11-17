using System;
using DG.Tweening;
using DG.Tweening.Core;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField, BoxGroup("Conf")] private float upBarSpeed = .2f;
    [SerializeField, BoxGroup("Conf")] private float downBarSpeed = .8f;
    [SerializeField, BoxGroup("Conf")] private string stringFormat = "{0}/{1}";
    
    [Space(20)]
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform upBar;
    [SerializeField] private RectTransform downBar;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    
    private DOGetter<float> _valueGetter;
    private DOSetter<float> _valueSetter;
    private float _currentValue;
    private float _maxValue;

    private void Awake()
    {
        _valueGetter = () => _currentValue;
        _valueSetter = (newValue) =>
        {
            _currentValue = newValue;
            upBar.sizeDelta = new Vector2(newValue / _maxValue * background.rect.width, 0f);
            _textMeshPro.text = string.Format(stringFormat, newValue.ToString("F0"), _maxValue.ToString("F0"));
        };
    }

    public void UpdateBar(float currentValue, float maxValue)
    {
        _maxValue = maxValue;
        UpdateBar(currentValue);
    }

    public void UpdateBar(float currentValue)
    {
        _textMeshPro.GetComponent<RectTransform>().DOShakePosition(upBarSpeed, 5f);
        DOTween.To(_valueGetter, _valueSetter, currentValue, upBarSpeed).OnComplete(() =>
            {
                downBar.DOSizeDelta(new Vector2(currentValue / _maxValue * background.rect.width, 0f), downBarSpeed);
            });
    }
}
