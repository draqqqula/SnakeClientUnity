using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class PlayerController : MonoBehaviour
{
    public string PlayerName {  get; private set; }

    private YandexGame _integration;

    public void StartMatchmaking()
    {
        Debug.Log("Matchmaking started");
        SceneManager.LoadScene("SessionScene", LoadSceneMode.Single);
    }
}
