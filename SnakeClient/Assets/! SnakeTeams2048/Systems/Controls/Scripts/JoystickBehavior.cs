using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickBehaviour : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private Rect _availableZone;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _head;
    [SerializeField] private RectTransform _body;
    private Vector2 _defaultPosition;
    private bool _isDragged = false;
    private Vector2 _cachedBodyPosition = Vector2.zero;
    private Vector2 _cachedHeadPosition = Vector2.zero;
    public float Direction { get; private set; } = 0f;

    void Start()
    {
        _defaultPosition = _body.anchoredPosition;
    }

    void Update()
    {
        UpdateGUI();
    }

    private void Reset()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void UpdateGUI()
    {
        if (!_isDragged)
        {
            _body.anchoredPosition = _defaultPosition;
            _head.position = _body.position;
        }
        else
        {
            _body.anchoredPosition = _cachedBodyPosition;
            _head.anchoredPosition = _cachedHeadPosition;
        }
    }

    public void Hold(Vector2 screenPosition)
    {
        var screenZonePositon = _availableZone.position * (_canvas.pixelRect.size / _canvas.scaleFactor);
        var screenZoneSize = _availableZone.size * (_canvas.pixelRect.size / _canvas.scaleFactor);
        var screenZoneRect = new Rect(screenZonePositon, screenZoneSize);

        var guiPoint = screenPosition / _canvas.scaleFactor;

        if (!screenZoneRect.Contains(guiPoint) && !_isDragged)
        {
            return;
        }

        if (!_isDragged)
        {
            _cachedBodyPosition = guiPoint;
        }
        _isDragged = true;

        var delta = guiPoint - _cachedBodyPosition;

        if (delta.magnitude > _maxDistance)
        {
            _cachedBodyPosition -= delta.normalized * (_maxDistance - delta.magnitude);
            delta = guiPoint - _cachedBodyPosition;
        }

        _cachedHeadPosition = _cachedBodyPosition + delta;

        if (delta != Vector2.zero)
        {
            Direction = -System.MathF.Atan2(delta.y, delta.x);
        }
    }

    public void Release()
    {
        _isDragged = false;
    }
}
