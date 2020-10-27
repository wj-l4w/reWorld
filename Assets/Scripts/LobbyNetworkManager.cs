using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LobbyNetworkManager : NetworkManager
{
    [SerializeField] private int minPlayers = 10;
    [Scene] [SerializeField] private string menuScene = string.Empty;

    [Header("Room")]
    [SerializeField] private reWorldLobby lobbyPrefab = null;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    public List<reWorldLobby> RoomPlayers { get; } = new List<reWorldLobby>();

    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        //Get and load all gameObejct prefabs at the directory "SpawnablePrefabs"
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        //Too many people
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        //Game started/ Not in menu scene
        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<reWorldLobby>();

            RoomPlayers.Remove(player);

            NotifyPlayersReadyState();
        }

        base.OnServerDisconnect(conn);
    }
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if(SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;

            reWorldLobby playerInstance = Instantiate(lobbyPrefab);

            playerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, playerInstance.gameObject);
        }
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        //base.OnStopServer();
    }

    public void NotifyPlayersReadyState()
    {
        foreach(var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        //If not enough people
        if (numPlayers < minPlayers) { return false; }

        //Check if everyone is ready
        foreach (var player in RoomPlayers)
        {
            if(!player.IsReady) { return false; }
        }

        return true;
    }


}
