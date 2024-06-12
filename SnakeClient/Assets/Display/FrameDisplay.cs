using MessageSchemes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class FrameDisplay : MonoBehaviour
{
    [SerializeField]
    public Vector2 Scale = new Vector2(0.01f, -0.01f);

    public GameObject[] FramePrefabs;

    private Dictionary<string, GameObject> _prefabDictionary;

    private readonly Dictionary<int, Renderer[]> _cachedRenderers = new ();

    private readonly HashSet<int> _sleeping = new ();

    public Dictionary<int, GameObject> Instances = new ();

    private void Start()
    {
        _prefabDictionary = FramePrefabs.ToDictionary(it => it.name, it => it);
    }

    public void Synchronize(EventMessage message)
    {
        CreateFrames(message);

        ApplyUpdateEvents(message.PositionEventsLength, message.PositionEvents, 
            it => it.Value.Id,
            (frame, obj) => ChangeXY(EnsureAwakened(obj, frame.Value.Id).transform, frame.Value.Position));

        ApplyUpdateEvents(message.AngleEventsLength, message.AngleEvents,
            it => it.Value.Id,
            (frame, obj) => EnsureAwakened(obj, frame.Value.Id).transform.rotation = Convert(frame.Value.Angle));

        ApplyUpdateEvents(message.SizeEventsLength, message.SizeEvents,
            it => it.Value.Id,
            (frame, obj) => EnsureAwakened(obj, frame.Value.Id).transform.localScale = Convert(frame.Value.Size, AbsVector(Scale)));

        ApplyUpdateEvents(message.DisposedLength, message.Disposed,
            id => id,
            (id, obj) => { Destroy(obj); Instances.Remove(id); _sleeping.Remove(id); _cachedRenderers.Remove(id); });

        ApplyUpdateEvents(message.SleepLength, message.Sleep,
            id => id,
            (id, obj) => PutToSleep(id));

        ApplyEvents(message.TransformationsLength, message.Transformations,
            group => ApplyEvents(
                group.Value.FramesLength,
                group.Value.Frames, id => ReplaceAsset(id, group.Value.NewAsset)
            ));
    }

    private void PutToSleep(int id)
    {
        var target = Instances[id];
        {
            if (_cachedRenderers.TryGetValue(id, out var renderers))
            {
                DisableRenderers(renderers);
            }
        }
        {
            var renderers = target.GetComponents<Renderer>();
            _cachedRenderers[id] = renderers;
            DisableRenderers(renderers);
        }
        if (!_sleeping.Contains(id))
        {
            _sleeping.Add(id);
        }
    }

    private void DisableRenderers(Renderer[] renderers)
    {
        foreach (var renderer in renderers)
        {
            renderer.forceRenderingOff = true;
        }
    }

    private void EnableRenderers(Renderer[] renderers)
    {
        foreach (var renderer in renderers)
        {
            renderer.forceRenderingOff = false;
        }
    }


    private void ReplaceAsset(int id, string newAsset)
    {
        if (_prefabDictionary.TryGetValue(newAsset, out var prefab) &&
            Instances.TryGetValue(id, out var existingInstance))
        {
            var position = existingInstance.transform.position;
            var rotation = existingInstance.transform.rotation;
            var size = existingInstance.transform.localScale;

            var obj = Instantiate(prefab, position, rotation);
            obj.transform.localScale = size;
            Instances[id] = obj;
            Destroy(existingInstance);
        }
    }

    private void CreateFrames(EventMessage message)
    {
        for (int i = 0; i < message.CreatedLength; i++)
        {
            var group = message.Created(i).Value;
            if (_prefabDictionary.TryGetValue(group.Asset, out var prefab))
            {
                for (int j = 0; j < group.FramesLength; j++)
                {
                    var frame = group.Frames(j).Value;
                    var position = Convert(frame.Position, Scale);
                    var rotation = Convert(frame.Angle);
                    var size = Convert(frame.Size, AbsVector(Scale));
                    if (Instances.TryGetValue(frame.Id, out var instance))
                    {
                        ChangeXY(instance.transform, frame.Position);
                        instance.transform.rotation = rotation;
                        instance.transform.localScale = size;
                        continue;
                    }
                    var obj = Instantiate(prefab, position, rotation);
                    obj.transform.localScale = size;
                    Instances.Add(frame.Id, obj);
                }
            }
        }
    }

    private GameObject EnsureAwakened(GameObject obj, int id)
    {
        if (_sleeping.Contains(id) && _cachedRenderers.TryGetValue(id, out var renderers))
        {
            EnableRenderers(renderers);
            _sleeping.Remove(id);
        }
        return obj;
    }

    private Vector2 AbsVector(Vector2 vector)
    {
        return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
    }

    private void ChangeXY(Transform transform, Vec2 newPosition)
    {
        transform.position = new Vector3(newPosition.X * Scale.x, newPosition.Y * Scale.y, transform.position.z);
    }

    private Vector3 Convert(Vec2 vec2, Vector2 scale)
    {
        return new Vector3(vec2.X * scale.x, vec2.Y * scale.y, 0);
    }

    private Quaternion Convert(float angle)
    {
        return Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle * MathF.Sign(Scale.x) * MathF.Sign(Scale.y));
    }

    private void ApplyEvents<T>(int length, 
        Func<int,T> pickObjFunc, 
        Action<T> applyAction)
    {
        for (int i = 0; i < length; i++)
        {
            var eventObj = pickObjFunc(i);
            applyAction(eventObj);
        }
    }

    private void ApplyUpdateEvents<T>(int length,
        Func<int, T> pickObjFunc,
        Func<T, int> idPicker,
        Action<T, GameObject> applyAction)
    {
        ApplyEvents(length, pickObjFunc, 
            frame => TryModify(frame, idPicker(frame), applyAction));
    }

    private void TryModify<T>(T frame, int id, Action<T, GameObject> applyAction)
    {
        if (Instances.TryGetValue(id, out var obj))
        {
            applyAction(frame, obj);
        }
    }
}
