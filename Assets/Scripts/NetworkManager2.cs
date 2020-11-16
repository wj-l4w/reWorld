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

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Server scene changed to PreGameLobby");
            SpawnNPC();
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

    public void SpawnNPC()
    {
        GameObject clone = Instantiate(GargoyleNPC);
        NetworkServer.Spawn(clone);
    }
}
