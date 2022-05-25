using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers = 4;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("SINGLETON - Trying to make another instance of singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    // private void OnEnable()
    // {
    //     if(Instance != null)
    //     {
    //         Debug.Log("SINGLETON - Trying to make another instance of singleton");
    //     }
    //     else
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(Instance);
    //         playerConfigs = new List<PlayerConfiguration>();
    //     }
    // }

    //public void SetPlayerColor

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if( playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene("NewMain");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player Joined " + pi.playerIndex);
        pi.transform.SetParent(transform);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }

    public void HandlePlayerLeave(PlayerInput pi)
    {
        Debug.Log("Player Left " + pi.playerIndex);
        pi.transform.SetParent(transform);
        if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
}
