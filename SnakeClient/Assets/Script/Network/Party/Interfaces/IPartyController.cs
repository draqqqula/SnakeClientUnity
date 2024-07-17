using Assets.Script.Network.Party.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPartyController
{
    public IRequestEventSource<ILeaderDialogue> RequestCreateParty();
    public IRequestEventSource<IMemberDialogue> RequestJoinParty(string code);
}

public interface IRequestEventSource<T> : IDisposable
{
    public event Action<T> OnSuccess;
    public event Action OnFailure;
}

public interface IPartyEventSource : IDisposable
{
    public event Action<PartyMember> OnJoined;
    public event Action<PartyMember> OnLeft;
    public event Action<PartyMember> OnLeader;
    public event Action OnMatchMaking;
    public event Action<Guid> OnSessionFound;
}

public interface ILeaderDialogue : IPartyEventSource
{
    public event Action<string> OnCodeRecieved;
}

public interface IMemberDialogue : IPartyEventSource
{
    public event Action<PartyState> OnStateRecieved;
}