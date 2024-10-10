using Assets.Script.Commands.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using Zenject;

public class StatisticsDisplayUIController : MonoBehaviour
{
    [field: SerializeField] private LocalizeStringEvent LocalizeStringEvent { get; set; }

    [Inject]
    public void Construct(ICommandListenerFactory listenerFactory)
    {
        foreach (var key in LocalizeStringEvent.StringReference.Keys)
        {
            listenerFactory.ListenFor<int>(key).OnRecieved += newValue => 
            UpdateVariable(newValue, key, LocalizeStringEvent);
        }
    }

    public void UpdateVariable(int newValue, string key, LocalizeStringEvent lse)
    {
        var variable = (IntVariable)lse.StringReference[key];
        variable.Value = newValue;
        lse.RefreshString();
    }
}
