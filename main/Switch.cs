using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    [SerializeField] private RectTransform _handler;
    [SerializeField] private Image _background;
    private Vector2 _handlerPosition;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    [SerializeField] private float _transformSpeed;
    [SerializeField] private float _colorChangeSpeed;

    private Vector2 _targetPosition;
    private Color _targetColor;

    private bool isOn;

    private void Start()
    {
        _handlerPosition = _handler.anchoredPosition;
        _targetPosition = _handlerPosition;
        _targetColor = _background.color;
    }

    private void FixedUpdate()
    {
        _handler.anchoredPosition = Vector2.MoveTowards(_handler.anchoredPosition, _targetPosition, Time.fixedDeltaTime * _transformSpeed);
        _background.color = Color.Lerp(_background.color, _targetColor, Time.fixedDeltaTime * _colorChangeSpeed);
    }

    public void ChangeState()
    {
        isOn = !isOn;
        _targetPosition = isOn ? _handlerPosition * -1 : _handlerPosition;
        _targetColor = isOn ? _activeColor : _inactiveColor;
    }
}
