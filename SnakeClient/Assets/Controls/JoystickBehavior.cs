using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickBehaviour : MonoBehaviour
{
    [field: SerializeField]
    public float MaxDistance { get; private set; }

    [field: SerializeField]
    public Rect AvailableZone { get; private set; }
    private Canvas Canvas { get; set; }
    public RectTransform Head;
    public RectTransform Body;
    public Vector2 DefaultPosition { get; private set; }
    public bool Active { get; set; } = false;

    [field: SerializeField]
    public Vector2 BodyPosition { get; set; } = Vector2.zero;
    [field: SerializeField]
    public Vector2 HeadPosition { get; set; } = Vector2.zero;
    public float Direction { get; set; } = 0f;

    void Start()
    {
        Canvas = GetComponent<Canvas>();
        DefaultPosition = Body.anchoredPosition;
    }

    void Update()
    {
        UpdateGUI();
    }

    public void UpdateGUI()
    {
        if (!Active)
        {
            Body.anchoredPosition = DefaultPosition;
            Head.position = Body.position;
        }
        else
        {
            Body.anchoredPosition = BodyPosition;
            Head.anchoredPosition = HeadPosition;
        }
    }

    public void Touch(Vector2 screenPosition)
    {
        var screenZonePositon = AvailableZone.position * (Canvas.pixelRect.size / Canvas.scaleFactor);
        var screenZoneSize = AvailableZone.size * (Canvas.pixelRect.size / Canvas.scaleFactor);
        var screenZoneRect = new Rect(screenZonePositon, screenZoneSize);

        var guiPoint = screenPosition / Canvas.scaleFactor;

        if (!screenZoneRect.Contains(guiPoint) && !Active)
        {
            return;
        }

        if (!Active)
        {
            BodyPosition = guiPoint;
        }
        Active = true;

        var delta = guiPoint - BodyPosition;

        if (delta.magnitude > MaxDistance)
        {
            BodyPosition -= delta.normalized * (MaxDistance - delta.magnitude);
            delta = guiPoint - BodyPosition;
        }

        HeadPosition = BodyPosition + delta;

        if (delta != Vector2.zero)
        {
            Direction = -System.MathF.Atan2(delta.y, delta.x);
        }
    }

    public void Release()
    {
        Active = false;
    }
}
