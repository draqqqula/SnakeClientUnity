using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicativeColorFilter : MonoBehaviour
{
    [SerializeField] public float Factor;
    [SerializeField] public Color Color;

    public SpriteRenderer TargetRenderer;

    void Start()
    {
        TargetRenderer.color = Blend(TargetRenderer.color, Color);
    }

    public Color Blend(Color color1, Color color2)
    {
        var r = BlendAspect(color1.r, color2.r);
        var g = BlendAspect(color1.g, color2.g);
        var b = BlendAspect(color1.b, color2.b);
        return new Color(r, g, b);
    }

    public float BlendAspect(float aspect1, float aspect2)
    {
        return Mathf.Pow((aspect1 + aspect2) * 0.5f, Factor);
    }
}
