using Assets.UIElements.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PartyInfoUIController : MonoBehaviour
{
    [field: SerializeField] private TMP_Text TextDisplay {  get; set; }

    public string PartyCode
    {
        get
        {
            return TextDisplay.text;
        }
        set
        {
            TextDisplay.text = value;
        }
    }
}
