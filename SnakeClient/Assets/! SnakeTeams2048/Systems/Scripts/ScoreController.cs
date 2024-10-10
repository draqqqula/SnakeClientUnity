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

public class ScoreController : MonoBehaviour
{
    [field: SerializeField] private TeamScoreUIController[] ScoreDisplays {  get; set; }
    private Dictionary<TeamColor, TeamScoreUIController> AvailableDisplays { get; set; }
    private Dictionary<TeamColor, int> Scores { get; set; } = new ();

    private void Reset()
    {
        ScoreDisplays = GetComponentsInChildren<TeamScoreUIController>();
    }

    public void Awake()
    {
        AvailableDisplays = ScoreDisplays.ToDictionary(it => it.Team, it => it);
    }

    public void ChangeValue(TeamColor team, int value)
    {
        Scores[team] = value;

        if (AvailableDisplays.TryGetValue(team, out var display))
        {
            display.SetScore(value);

            var maxScore = Scores.Values.Max();
            if (value != maxScore || value == 0)
            {
                return;
            }

            display.SetLeader(true);
            foreach (var other in AvailableDisplays)
            {
                if (Scores.TryGetValue(other.Key, out var otherScore) && otherScore < maxScore)
                {
                    other.Value.SetLeader(false);
                }
            }
        }
    }
}
