using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Linq;

public class NetworkManager2 : NetworkManager
{
    [Header("Game Objects")]
    public GameObject GargoyleNPC;
    public GameObject SignBoard;
    public EnemySpawner es;


    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Server scene changed to PreGameLobby");
            SpawnStuffs();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Debug.Log("Server scene changed to PreGameLobby");
            es = FindObjectOfType<EnemySpawner>();
            SpawnMobs();
        }
    }

/*    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        NetworkServer.AddPlayerForConnection(conn, player);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            LobbyManager lobby = FindObjectOfType<LobbyManager>();
            lobby.player.Append<GameObject>(player.GetComponent<Player>);
        }
    }*/

    public void SpawnStuffs()
    {
        //Spawn gargoyle
        GameObject clone = Instantiate(GargoyleNPC);
        NetworkServer.Spawn(clone);

        //Spawn signboard
        GameObject signboard = Instantiate(SignBoard);
        NetworkServer.Spawn(signboard);
    }

    public void SpawnMobs()
    {
        es.initialSpawn(es.Enemies[0],8);
        es.initialSpawn(es.Enemies[1], 4);
        es.initialSpawn(es.Enemies[2], 1);
        es.initialSpawn(es.Enemies[3], 8);
        es.initialSpawn(es.Enemies[4], 4);
    }
}
