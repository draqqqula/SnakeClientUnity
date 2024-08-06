using Assets.Script.Core;
using System;
using TMPro;
using UnityEngine;

public class TeamScoreDisplay : MonoBehaviour
{
    [field: SerializeField] public TeamColor Team { get; set; }
    [field: SerializeField] private SpriteRenderer Sprite { get; set; }
    [field: SerializeField] private TMP_Text TextDisplay { get; set; }
    [field: SerializeField] private float LeadershipLengthMultiplier { get; set; } = 1.2f;
    [field: SerializeField] private float ShiftingDuration { get; set; } = 0.3f;
    [field: SerializeField] private float CountingDuration { get; set; } = 0.2f;
    private DynamicCounter ShiftingHandler { get; set; }
    private DynamicCounter CountingHandler { get; set; }
    private float DefaultLength { get; set; }
    private bool IsLeader { get; set; } = false;
    private float LeaderLength => LeadershipLengthMultiplier * DefaultLength;
    private float TargetLength => IsLeader ? LeaderLength : DefaultLength;
    public float Score { get; private set; }

    private void Reset()
    {
        Sprite = GetComponent<SpriteRenderer>();
        TextDisplay = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        DefaultLength = Sprite.size.y;
        ShiftingHandler = new DynamicCounter(value => Sprite.size = new Vector2(Sprite.size.x, value), () => Sprite.size.y);
        CountingHandler = new DynamicCounter(value => { Score = value; TextDisplay.text = Mathf.RoundToInt(Score).ToString(); }, () => Score); 
    }

    public void SetScore(int amount)
    {
        if (Score == amount)
        {
            return;
        }
        CountingHandler.Start(CountingDuration, amount);
    }

    public void SetLeader(bool flag)
    {
        if (IsLeader == flag)
        {
            return;
        }
        IsLeader = flag;
        ShiftingHandler.Start(ShiftingDuration, TargetLength);
    }

    private void Update()
    {
        ShiftingHandler.Update();
        CountingHandler.Update();
    }
}
