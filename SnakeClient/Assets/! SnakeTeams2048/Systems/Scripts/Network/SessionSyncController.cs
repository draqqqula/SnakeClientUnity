using UnityEngine;
using NativeWebSocket;
using System;
using Assets.State;
using Zenject;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Assets.Network;
using System.IO;
using TMPro;
using Assets.UIElements.Scripts;
using System.Linq;


public class WebSocketController : MonoBehaviour
{
    [SerializeField]
    private string RootAuthority;

    [SerializeField]
    private string JoinPath;

    [SerializeField]
    private string SearchPath;

    [SerializeField]
    private bool UseSpecifiedSession;

    [SerializeField]
    private string SpecifiedSession;

    [SerializeField]
    public TextMeshProUGUI IdDisplay;

    [SerializeField]
    private WindowDisplay ReconnectButton;

    private IMessageReader Reader;

    private IInputWriter[] Writers;

    private bool RequestedReconnect = false;

    private bool SessionFound = false;

    private WebSocket WebSocket { get; set; }

    private string BuildJoinUri(string authority, string sessionId)
    {
        return $"wss://{authority}{JoinPath}?sessionId={sessionId}";
    }

    private string BuildSearchUri(string authority)
    {
        return $"https://{authority}{SearchPath}";
    }

    [Inject]
    public void Construct(IMessageReader reader, IInputWriter[] writers)
    {
        Reader = reader;
        Writers = writers;
    }

    async void Start()
    {
        await TryConnect();
    }

    public void Reconnect()
    {
        Debug.Log("reconnecting...");
        RequestedReconnect = true;
    }

    private async Task TryConnect()
    {
        ReconnectButton?.Hide();
        if (UseSpecifiedSession)
        {
            await JoinSessionAsync(BuildJoinUri(RootAuthority, SpecifiedSession));
        }
        else
        {
            await SearchAndConnect(BuildSearchUri(RootAuthority));
        }
    }

    private async Task SearchAndConnect(string uri)
    {
        Debug.Log($"sending matchmaking request to {uri}");
        var launchRequest = UnityWebRequest.Get(uri);
        launchRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
        launchRequest.SetRequestHeader("Access-Control-Allow-Methods", "*");
        launchRequest.SetRequestHeader("Access-Control-Allow-Headers", "*");
        launchRequest.SendWebRequest().completed += async result =>
        {
            await OnLaunchCompleted(launchRequest);
        };
    }

    private async Task OnLaunchCompleted(UnityWebRequest launchRequest)
    {
        var regex = new Regex("[^\"]+");
        var sessionId = regex.Match(launchRequest.downloadHandler.text).Value;
        await JoinSessionAsync(BuildJoinUri(launchRequest.uri.Authority, sessionId));
    }

    private async Task JoinSessionAsync(string uri)
    {
        Debug.Log($"connecting to {uri}");
        if (IdDisplay != null)
        {
            IdDisplay.SetText(SpecifiedSession);
        }
        SessionFound = true;
        WebSocket = new WebSocket(uri);
        WebSocket.OnError += (err) => { Debug.LogError(err); ReconnectButton?.Show(); };
        WebSocket.OnMessage += OnMessage;
        await WebSocket.Connect();
    }

    async void Update()
    {
        if (!SessionFound)
        {
            if (RequestedReconnect)
            {
                await TryConnect();
                RequestedReconnect = false;
            }
            return;
        }
        var data = RunWriters();
        if (WebSocket.State == WebSocketState.Open && data.Length != 0)
        {
            await WebSocket.Send(data);
        }
        #if !UNITY_WEBGL || UNITY_EDITOR
            WebSocket.DispatchMessageQueue();
        #endif
    }

    private byte[] RunWriters()
    {
        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        foreach (var service in Writers)
        {
            service.Write(writer);
        }
        return stream.ToArray();
    }

    public void OnMessage(byte[] buffer)
    {
        Reader.Read(buffer);
    }
}