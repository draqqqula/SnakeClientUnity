using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class MinimapController : MonoBehaviour
{
    [SerializeField]
    public Rect MapArea;

    public FrameDisplay Display;

    private Dictionary<MinimapDisplayable, GameObject> Pinned = new ();
    private Dictionary<MinimapDisplayable, int> IdStorage = new();

    private List<int> Pending = new ();

    private RectTransform _mapTransform;

    void Start()
    {
        _mapTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        DispatchPending();
        UpdateIcons();
    }

    private void DispatchPending()
    {
        foreach (var id in Pending.ToArray())
        {
            if (Display.Instances.TryGetValue(id, out var frame) &&
            frame.TryGetComponent<MinimapDisplayable>(out var displayable))
            {
                Pinned.TryAdd(displayable, Instantiate(displayable.IconPrefab, _mapTransform));
                IdStorage.TryAdd(displayable, id);
                Pending.Remove(id);
            }
        }
    }

    private void UpdateIcons()
    {
        foreach (var icon in Pinned.ToArray())
        {
            if (!icon.Key.IsDestroyed())
            {
                ProjectPosition(icon.Key.transform, icon.Value.transform);

                if (icon.Key.ApplyScale)
                {
                    ProjectScale(icon.Key.transform, icon.Value.transform);
                }
                if (icon.Key.ApplyRotation)
                {
                    icon.Key.transform.localRotation = icon.Value.transform.rotation;
                }
            }
            else if (IdStorage.TryGetValue(icon.Key, out var id) && Display.Instances.TryGetValue(id, out var frame) &&
            frame.TryGetComponent<MinimapDisplayable>(out var displayable))
            {
                Destroy(icon.Value);
                Pinned.Remove(icon.Key);
                IdStorage.Remove(icon.Key);
                Pinned.TryAdd(displayable, Instantiate(displayable.IconPrefab, _mapTransform));
                IdStorage.TryAdd(displayable, id);
            }
            else
            {
                Destroy(icon.Value);
                Pinned.Remove(icon.Key);
                IdStorage.Remove(icon.Key);
            }
        }
    }

    public void Pin(int id)
    {
        Pending.Add(id);
    }

    private void ProjectScale(Transform frame, Transform icon)
    {
        icon.localScale = (XY(frame.localScale) / MapArea.size) * _mapTransform.rect.size;
    }

    private void ProjectPosition(Transform frame, Transform icon)
    {
        icon.localPosition = CalculateProjection(frame.position);
    }

    private Vector3 CalculateProjection(Vector3 original)
    {
        var xy = XY(original);
        var unboundPosition = (xy - MapArea.center) / MapArea.size;

        var boundX = Mathf.Clamp(unboundPosition.x, -0.5f, 0.5f);
        var boundY = Mathf.Clamp(unboundPosition.y, -0.5f, 0.5f);
        var boundPosition = new Vector2(boundX, boundY);

        var projectedPosition = _mapTransform.rect.center + boundPosition * _mapTransform.rect.size;
        return projectedPosition;
    }

    private Vector2 XY(Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
}
