using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public TimeSpan TimerExpires = TimeSpan.Zero;
    public TimeSpan CurrentTime => TimerExpires - TimeSpan.FromSeconds(Time.realtimeSinceStartup);
    private TextMeshPro Text;

    void Start()
    {
        Text = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        Text.text = CurrentTime.ToString("mm\\:ss");
    }
}
