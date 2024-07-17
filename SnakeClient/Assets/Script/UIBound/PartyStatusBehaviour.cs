using Assets.Script.UIBound.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyStatusBehaviour : MonoBehaviour, IPartyStatusDisplay
{
    private readonly Dictionary<Guid, PlayerIconBehaviour> _players = new ();

    [field: SerializeField] private RectTransform Area { get; set; }
    [field: SerializeField] private PlayerIconBehaviour IconBase { get; set; }
    [field: SerializeField] private float HorizontalSpacing { get; set; }
    [field: SerializeField] private float VerticalSpacing { get; set; }
    private PlayerIconBehaviour Leader {  get; set; }
    private PlayerIconBehaviour ActualPlayer { get; set; }
    private Rect CachedAreaRect { get; set; }

    private void Update()
    {
        if (CachedAreaRect != Area.rect)
        {
            UpdateLayout();
            CachedAreaRect = Area.rect;
        }
    }

    public void AddPlayer(Guid id)
    {
        _players.Add(id, Instantiate(IconBase, Area, false));
        UpdateLayout();
    }

    public void RemovePlayer(Guid id)
    {
        Destroy(_players[id]);
        _players.Remove(id);
        UpdateLayout();
    }

    private void UpdateLayout()
    {
        var playerList = _players.Values.ToArray();
        var lineSize = Convert.ToInt32(Area.rect.width / HorizontalSpacing);
        var lineCount = Math.Max(_players.Count / lineSize, 1);
        var topY = -((lineCount - 1) * VerticalSpacing) / 2;
        for (int line = 0; line < lineCount; line++)
        {
            var y = topY + line * VerticalSpacing;
            var onLineCount = _players.Count % lineSize;
            var leftX = -((onLineCount - 1) * HorizontalSpacing) / 2;
            for (int row = 0; row < lineSize; row++)
            {
                var x = leftX + line * HorizontalSpacing;
                playerList[line * lineCount + row].transform.localPosition = new Vector3(x, y, 0);
            }
        }
    }

    public void SetLeader(Guid id)
    {
        Leader.IsLeader = false;
        _players[id].IsLeader = true;
        Leader = _players[id];
    }

    public void SetNickname(Guid id, string name)
    {
        _players[id].Nickname = name;
    }

    public void SetOwn(Guid id)
    {
        ActualPlayer.IsOwn = false;
        _players[id].IsOwn = true;
        ActualPlayer = _players[id];
    }
}
