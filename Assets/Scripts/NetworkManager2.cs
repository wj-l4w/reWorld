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

    public override void OnServerSceneChanged(string sceneName)
    {
        base.OnServerSceneChanged(sceneName);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Debug.Log("Server scene changed to PreGameLobby");
            SpawnStuffs();
        }
    }

    public void SpawnStuffs()
    {
        //Spawn gargoyle
        GameObject clone = Instantiate(GargoyleNPC);
        NetworkServer.Spawn(clone);

        //Spawn signboard
        GameObject signboard = Instantiate(SignBoard);
        NetworkServer.Spawn(signboard);
    }
}
