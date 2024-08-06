using Assets.Script.Network.Party.Models;
using Assets.UIElements.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class PartyUIAgent : MonoBehaviour
{
    public UnityEvent OnSuccessfulConnection;
    public UnityEvent OnConnectionError;
    public UnityEvent OnDisconnect;
    public UnityEvent<string> OnCodeRecieved;
    public UnityEvent<PartyMember> OnPartyCreated;
    public UnityEvent<PartyMember> OnMemberJoined;
    public UnityEvent<PartyMember> OnMemberLeft;
    public UnityEvent<PartyMember> OnMemberToggledReady;
    public UnityEvent<PartyMember> OnNewLeader;
    public UnityEvent<PartyView> OnLoadedIntoParty;
    public UnityEvent OnErrorDuringJoinRequest;
    public UnityEvent OnErrorDuringCreateRequest;

    [field: SerializeField] private bool LoggingEnabled { get; set; }
    [field: SerializeField] private PartyNetworkController Controller { get; set; }

    private IPartyOperator ActiveOperator { get; set; }
    private IPartyEventSource ActiveEventSource { get; set; }
    private Guid OwnId { get; set; }

    public void Start()
    {
        OwnId = Guid.NewGuid();
    }

    public void CreateParty()
    {
        var response = Controller.RequestCreateParty(OwnId);
        response.OnSuccess += ConnectAsLeader;
        response.OnFailure += HandleErrorDuringCreateRequest;
    }

    public void JoinParty(string code)
    {
        var response = Controller.RequestJoinParty(OwnId, code);
        response.OnSuccess += ConnectAsMember;
        response.OnFailure += HandleErrorDuringJoinRequest;
    }

    private void HandleErrorDuringCreateRequest()
    {
        if (LoggingEnabled)
        {
            Debug.Log("connection error during create request");
        }
        OnErrorDuringCreateRequest?.Invoke();
    }

    private void HandleErrorDuringJoinRequest()
    {
        if (LoggingEnabled)
        {
            Debug.Log("connection error during join request");
        }
        OnErrorDuringJoinRequest?.Invoke();
    }

    private void HandleError(string error)
    {
        if (LoggingEnabled)
        {
            Debug.Log($"connection error. {error}");
        }
        OnConnectionError?.Invoke();
        OnDisconnect?.Invoke();
    }

    private void BindCommon(IPartyEventSource source)
    {
        source.OnJoined += OnJoined;
        source.OnLeft += OnLeft;
        source.OnToggleReady += OnToggleReady;
        source.OnLeader += OnLeaderChanged;
        source.OnError += HandleError;
    }

    private void Unbind(IPartyEventSource source)
    {
        source.OnJoined -= OnJoined;
        source.OnLeft -= OnLeft;
        source.OnToggleReady -= OnToggleReady;
        source.OnLeader -= OnLeaderChanged;
        source.OnError -= HandleError;
    }

    private void ConnectAsMember(IMemberDialogue dialogue)
    {
        dialogue.OnStateRecieved += OnMemberWelcome;
        BindCommon(dialogue);
        ActiveOperator = dialogue;
        ActiveEventSource = dialogue;
    }

    private void ConnectAsLeader(ILeaderDialogue dialogue)
    {
        dialogue.OnCodeRecieved += OnLeaderWelcome;
        BindCommon(dialogue);
        ActiveOperator = dialogue;
        ActiveEventSource = dialogue;
    }

    public void ToggleReady()
    {
        if (ActiveOperator is null)
        {
            return;
        }
        if (!ActiveOperator.TryToggleReady())
        {
            ActiveOperator = null;
        }
    }

    public void LeaveParty()
    {
        Controller.LeaveParty();
        OnDisconnect?.Invoke();
        Unbind(ActiveEventSource);
        ActiveOperator = null;
        ActiveEventSource = null;
    }

    private void OnMemberWelcome(PartyState state)
    {
        OnLoadedIntoParty?.Invoke(new PartyView()
        {
            State = state,
            Player = new PartyMember()
            {
                Id = OwnId
            }
        });
        OnSuccessfulConnection?.Invoke();
    }

    private void OnLeaderWelcome(string code)
    {
        if (LoggingEnabled)
        {
            Debug.Log($"PartyCode: {code}");
        }
        OnCodeRecieved?.Invoke(code);
        OnPartyCreated?.Invoke(new PartyMember() 
        { 
            Id = OwnId 
        });
        OnSuccessfulConnection?.Invoke();
    }

    private void OnToggleReady(PartyMember member)
    {
        OnMemberToggledReady?.Invoke(member);
        if (LoggingEnabled)
        {
            Debug.Log($"{member.Id.ToString()} toggled ready");
        }
    }

    private void OnJoined(PartyMember member)
    {
        OnMemberJoined?.Invoke(member);
        if (LoggingEnabled)
        {
            Debug.Log($"{member.Id.ToString()} joined");
        }
    }
    
    private void OnLeft(PartyMember member)
    {
        OnMemberLeft?.Invoke(member);
        if (LoggingEnabled)
        {
            Debug.Log($"{member.Id.ToString()} left");
        }
    }

    private void OnLeaderChanged(PartyMember member)
    {
        OnNewLeader?.Invoke(member);
        if (LoggingEnabled)
        {
            Debug.Log($"{member.Id.ToString()} is the new leader");
        }
    }
}
