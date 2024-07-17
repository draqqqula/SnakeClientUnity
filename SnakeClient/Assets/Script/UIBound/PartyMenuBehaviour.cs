using Assets.Script.Network.Party.Models;
using Assets.Script.UIBound.Interfaces;
using Assets.UIElements.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PartyMenuBehaviour : UIElement
{
    [field: SerializeField] private PartyNetworkController Controller { get; set; }
    [field: SerializeField] private IPartyStatusDisplay StatusDisplay { get; set; }
    [field: SerializeField] private PartyInfoBehaviour PartyInfo {  get; set; }

    public void CreateParty()
    {
        var response = Controller.RequestCreateParty();
        response.OnSuccess += ConnectAsLeader;
        response.OnFailure += () => Debug.Log("connection error");
    }

    private void ConnectAsLeader(ILeaderDialogue dialogue)
    {
        dialogue.OnCodeRecieved += (code) => Debug.Log(code);
        dialogue.OnJoined += (client) => Debug.Log($"{client.Id.ToString()} joined");
        dialogue.OnLeft += (client) => Debug.Log($"{client.Id.ToString()} left");
        dialogue.OnLeader += OnLeaderChanged;
    }

    private void OnCodeRecieved(string code)
    {
        Debug.Log($"PartyCode: {code}");
        PartyInfo.PartyCode = code;
    }

    private void OnJoined(PartyMember member)
    {
        StatusDisplay.AddPlayer(member.Id);
        Debug.Log($"{member.Id.ToString()} joined");
    }
    
    private void OnLeaves(PartyMember member)
    {
        StatusDisplay.RemovePlayer(member.Id);
        Debug.Log($"{member.Id.ToString()} left");
    }

    private void OnLeaderChanged(PartyMember member)
    {
        StatusDisplay.SetLeader(member.Id);
        Debug.Log($"{member.Id.ToString()} is the new leader");
    }
}
