using Assets.UIElements.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCardUIController : MonoBehaviour
{
    public enum AbilityCardState
    {
        Blocked,
        Available,
        Selected
    }

    [field: SerializeField]
    public AbilityCardState State {  get; private set; }

    [SerializeField]
    private UIElement SelectButton;

    [SerializeField]
    private UIElement SelectedText;

    [SerializeField]
    private Material Selected;

    [SerializeField]
    private Material Available;

    [SerializeField]
    private Material Blocked;

    [SerializeField]
    private SpriteRenderer CardBackground;

    private Canvas Canvas;

    private void Start()
    {
        Canvas = GetComponentInChildren<Canvas>();
        Canvas.worldCamera = Camera.main;
        ChangeState(State);
    }

    public void ChangeState(AbilityCardState state)
    {
        State = state;
        switch (state)
        {
            case AbilityCardState.Available:
                CardBackground.material = Available; SelectButton.Show(); SelectedText.Hide(); break;
            case AbilityCardState.Blocked:
                CardBackground.material = Blocked; SelectButton.Hide(); SelectedText.Hide(); break;
            case AbilityCardState.Selected:
                CardBackground.material = Selected; SelectButton.Hide(); SelectedText.Show(); break;
        }
    }

    public void SetSeleceted()
    {
        ChangeState(AbilityCardState.Selected);
    }

    public void SetAvailable()
    {
        ChangeState(AbilityCardState.Available);
    }

    public void SetBlocked()
    {
        ChangeState(AbilityCardState.Blocked);
    }

    public void Deselect()
    {
        if (State == AbilityCardState.Selected)
        {
            ChangeState(AbilityCardState.Available);
        }
    }
}
