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


public class WebSocketController : MonoBehaviour
{
    [SerializeField]
    public bool AutoCreateSession;

    [field: SerializeField]
    public string ConnectionString { get; private set; }
    [field: SerializeField]
    public string LaunchString { get; private set; }
    [field: SerializeField]
    public string SessionId { get; private set; }
    private WebSocket WebSocket { get; set; }

    public TextMeshProUGUI IdDisplay;

    public UnityWebRequest CreateSessionRequest;

    public IMessageReader Reader;

    public IInputWriter[] Writers;

    private bool SessionFound = false;

    [Inject]
    public void Construct(IMessageReader reader, IInputWriter[] writers)
    {
        Reader = reader;
        Writers = writers;
    }

    async void Start()
    {
        if (AutoCreateSession)
        {
            CreateSessionRequest = UnityWebRequest.Get(LaunchString);
            CreateSessionRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
            CreateSessionRequest.SetRequestHeader("Access-Control-Allow-Methods", "*");
            CreateSessionRequest.SetRequestHeader("Access-Control-Allow-Headers", "*");
            CreateSessionRequest.SendWebRequest().completed += async _ => 
            {
                var regex = new Regex("[^\"]+");
                SessionId = regex.Match(CreateSessionRequest.downloadHandler.text).Value;
                await EstablishConnectionAsync();
            };
            return;
        }
        await EstablishConnectionAsync();
    }

    private async Task EstablishConnectionAsync()
    {
        if (IdDisplay != null)
        {
            IdDisplay.SetText(SessionId);
        }
        SessionFound = true;
        WebSocket = new WebSocket(string.Concat(ConnectionString, SessionId));
        WebSocket.OnError += (err) => Debug.Log(err);
        WebSocket.OnMessage += OnMessage;
        await WebSocket.Connect();
    }

    async void Update()
    {
        if (!SessionFound)
        {
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