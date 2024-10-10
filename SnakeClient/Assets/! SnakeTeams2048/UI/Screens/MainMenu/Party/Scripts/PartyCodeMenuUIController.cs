using Assets.UIElements.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PartyCodeMenuUIController : MonoBehaviour
{
    public UnityEvent<string> OnConfirmed;
    public string Code { get; set; }

    public void JoinParty()
    {
        OnConfirmed?.Invoke(Code);
    }
}
