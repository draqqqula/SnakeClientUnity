using Assets.Input.Writers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

public class OptionBinder : MonoBehaviour
{
    [SerializeField]
    public Button[] Buttons;

    private AbilityButtonController ButtonController;
    private OptionInputWriter OptionInputWriter;

    [Inject]
    public void Construct(OptionInputWriter optionInputWriter, AbilityButtonController abilityButtonController)
    {
        ButtonController = abilityButtonController;
        OptionInputWriter = optionInputWriter;
    }

    public void Start()
    {
        for (var i = 0; i < Buttons.Length; i++)
        {
            var number = i;
            Buttons[i].onClick.AddListener(() => SelectOption(number));
        }
    }

    public void SelectOption(int index)
    {
        OptionInputWriter.SetOption(index);
        ButtonController.SetAbilityIcon(index);
    }
}
