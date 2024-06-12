using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class RespawnWindowDisplay : WindowDisplay
{
    class TimerInfo
    {
        public string Time { get; set; }

        public override string ToString()
        {
            return Time;
        }
    }
    private TimerInfo TimerInfoInstance = new TimerInfo() { Time = "0" };

    public LocalizeStringEvent LocalizeStringEvent;
    public TextMeshProUGUI TimerDisplay;
    [SerializeField]
    public GameObject Root;

    public TimeSpan TimerExpires = TimeSpan.MinValue;
    public TimeSpan Timer => TimerExpires - TimeSpan.FromSeconds(Time.realtimeSinceStartup);
    public bool TimerActive {  get; set; } = true;
    public new void Start()
    {
        var partentCanvas = GetComponentInParent<Canvas>();
        var localCanvas = GetComponentInChildren<Canvas>();
        localCanvas.worldCamera = partentCanvas.worldCamera;

        LocalizeStringEvent.StringReference.Arguments = new[] { TimerInfoInstance };

        base.Start();
    }
    public override void Hide()
    {
        TimerActive = false;
        Root.SetActive(false);
    }

    public override void Show()
    {
        TimerActive = true;
        Root.SetActive(true);
    }

    public void SetTimer(float duration)
    {
        TimerExpires = TimeSpan.FromSeconds(Time.realtimeSinceStartup + duration);
    }

    public void Update()
    {
        if (!TimerActive)
        {
            return;
        }
        TimerInfoInstance.Time = Timer.ToString("%s");
        LocalizeStringEvent.RefreshString();

        if (Timer <= TimeSpan.Zero)
        {
            Hide();
        }
    }
}
