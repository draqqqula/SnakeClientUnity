using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public enum TeamColor
{
    Red,
    Blue,
    Green,
    Yellow,
}

public class ScoreController : WindowDisplay
{
    [SerializeField]
    private GameObject Root;

    public TextMeshPro[] TextObjects;

    public void ChangeValue(TeamColor team, int value)
    {
        var targetObject = TextObjects.FirstOrDefault(it => it.tag == team.ToString());
        if (targetObject is null)
        {
            return;
        }
        targetObject.text = value.ToString();
    }

    public override void Hide()
    {
        Root?.SetActive(false);
        base.Hide();
    }

    public override void Show()
    {
        Root?.SetActive(true);
        base.Show();
    }
}
