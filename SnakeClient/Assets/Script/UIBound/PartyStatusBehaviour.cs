using Assets.Script.Network.Party.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyStatusBehaviour : MonoBehaviour
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

    public void ResetList()
    {
        foreach (var player in _players.Values)
        {
            Destroy(player.gameObject);
        }
        _players.Clear();
        Leader = null;
        ActualPlayer = null;
    }

    public void AddMember(PartyMember member)
    {
        _players.Add(member.Id, Instantiate(IconBase, Area, false));
        UpdateLayout();
    }

    public void RemoveMember(PartyMember member)
    {
        Destroy(_players[member.Id].gameObject);
        _players.Remove(member.Id);
        UpdateLayout();
    }

    public void ToggleReadyIndicator(PartyMember member)
    {
        _players[member.Id].IsReady = !_players[member.Id].IsReady;
    }

    private void UpdateLayout()
    {
        if (_players.Count == 0)
        {
            return;
        }
        var playerList = _players.Values.ToArray();
        var rowCount = Convert.ToInt32(Area.rect.width / HorizontalSpacing);
        var lineCount = Convert.ToInt32(Math.Ceiling(_players.Count / (double)rowCount));
        var topY = (lineCount - 1) * VerticalSpacing / 2;
        for (int line = 0; line < lineCount; line++)
        {
            var y = topY - line * VerticalSpacing;
            var onLineCount = Math.Min(_players.Count - (line * rowCount), rowCount);
            var leftX = -((onLineCount - 1) * HorizontalSpacing) / 2;
            for (int row = 0; row < onLineCount; row++)
            {
                var x = leftX + row * HorizontalSpacing;
                playerList[line * rowCount + row].transform.localPosition = new Vector3(x, y, 0);
            }
        }
    }

    public void SetLeader(PartyMember member)
    {
        if (Leader is not null)
        {
            Leader.IsLeader = false;
        }
        _players[member.Id].IsLeader = true;
        Leader = _players[member.Id];
    }

    public void SetNickname(Guid id, string name)
    {
        _players[id].Nickname = name;
    }

    public void SetPlayer(PartyMember member)
    {
        if (ActualPlayer is not null)
        {
            ActualPlayer.IsOwn = false;
        }
        _players[member.Id].IsOwn = true;
        ActualPlayer = _players[member.Id];
    }

    public void LoadState(PartyView view)
    {
        ResetList();
        foreach (var info in view.State.Members)
        {
            AddMember(info.Member);
            if (info.IsReady)
            {
                ToggleReadyIndicator(info.Member);
            }
        }
        if (view.State.Members.Count != 0)
        {
            SetLeader(view.State.Members.First().Member);
        }
        AddMember(view.Player);
        SetPlayer(view.Player);
    }
}
