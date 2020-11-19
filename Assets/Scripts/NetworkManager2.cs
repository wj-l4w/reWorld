using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using System.Linq;
using System;

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
            Debug.Log("Server scene changed to Map 2");
            es = FindObjectOfType<EnemySpawner>();
            spawnMobs();

        }
    }

    private void spawnMobs()
    {
        es.initialSpawn(es.Enemies[0], 6);
        es.initialSpawn(es.Enemies[1], 4);
        es.initialSpawn(es.Enemies[2], 1);
        es.initialSpawn(es.Enemies[3], 6);
        es.initialSpawn(es.Enemies[4], 4);
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
