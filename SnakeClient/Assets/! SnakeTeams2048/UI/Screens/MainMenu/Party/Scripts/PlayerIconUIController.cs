using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject.ReflectionBaking;

public class PlayerIconUIController : MonoBehaviour
{
    [SerializeField]
    private bool _isOwn;
    [SerializeField]
    private bool _isLeader;
    [SerializeField]
    private bool _isReady;

    [SerializeField]
    private Color OwnHaloColor;
    [SerializeField]
    private Color DefaultHaloColor;

    [field: SerializeField] private SpriteRenderer Halo { get; set; }
    [field: SerializeField] private SpriteRenderer Crown { get; set; }
    [field: SerializeField] private TMP_Text NicknameDisplay { get; set; }
    [field: SerializeField] private TMP_Text ReadyStatusDisplay { get; set; }
    public bool IsOwn
    {
        get
        {
            return _isOwn;
        }
        set
        {
            if (value)
            {
                AddOwnStatus();
            }
            else
            {
                RemoveOwnStatus();
            }
            _isOwn = value;
        }
    }
    public bool IsLeader
    {
        get
        {
            return _isLeader;
        }
        set
        {
            if (value)
            {
                AddLeaderStatus();
            }
            else
            {
                RemoveLeaderStatus();
            }
            _isLeader = value;
        }
    }

    public bool IsReady
    {
        get
        {
            return _isReady;
        }
        set
        {
            ReadyStatusDisplay.gameObject.SetActive(value);
            _isReady = value;
        }
    }

    public string Nickname
    {
        get 
        { 
            return NicknameDisplay.text; 
        }
        set
        {
            NicknameDisplay.text = value;
        }
    }

    void Start()
    {
        IsOwn = _isOwn;
        IsLeader = _isLeader;
        IsReady = _isReady;
    }

    private void AddLeaderStatus()
    {
        Crown.forceRenderingOff = false;
    }

    private void RemoveLeaderStatus()
    {
        Crown.forceRenderingOff = true;
    }

    private void AddOwnStatus()
    {
        Halo.color = OwnHaloColor;
    }

    private void RemoveOwnStatus()
    {
        Halo.color = DefaultHaloColor;
    }
}
