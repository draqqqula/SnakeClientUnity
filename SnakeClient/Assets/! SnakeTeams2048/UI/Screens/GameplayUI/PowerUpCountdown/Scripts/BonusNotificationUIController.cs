using Assets.UIElements.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusNotificationUIController : MonoBehaviour
{
    [field: SerializeField] private CountDownTimer Timer { get; set; }
    [field: SerializeField] private UIElement UI { get; set; }
    public void Show(TimeSpan activationTime)
    {
        Timer.StartTimer(activationTime);
        UI.Show();
    }
}
