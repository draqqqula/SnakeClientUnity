using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class PlayerController : MonoBehaviour
{
    public string PlayerName {  get; private set; }

    private YandexGame _integration;
    void Start()
    {
        _integration = GameObject
            .FindGameObjectsWithTag("Integration")
            .FirstOrDefault()
            .GetComponent<YandexGame>();
        PlayerName = _integration.infoYG.name;
    }

    public void StartMatchmaking()
    {
        Debug.Log("Matchmaking started");
    }
}
