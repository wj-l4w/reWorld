using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
    //List of enemies as follows
    //0 - Golems, 1 - Cobra, 2 - Cat, 3- Slime, 4 - Gladiator
    public List<GameObject> Enemies = new List<GameObject>();
    
    //Currently 23 spawns in total
    public List<GameObject> EnemiesSpawnPos = new List<GameObject>();
    public float spawnDelay;

    private float randomX, randomY;
    private Vector2 spawnPos;

/*    
      ---USE THIS FOR RESPAWN SYSTEM---
      --SPAWNS ENEMY ONE BY ONE WITH DELAY--
        IEnumerator SpawnGolem()
    {
        randomX = Random.Range(-1,1);
        randomY = Random.Range(-1, 1);
        spawnPos.x += randomX;
        spawnPos.y += randomY;
        Instantiate(Enemies[0], spawnPos, Quaternion.identity);

        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnGolem());
    }
*/

    public void initialSpawn(GameObject enemy, int loopCount)
    {
        NetworkManager2 nm2 = FindObjectOfType<NetworkManager2>();
        for (int i = 1; i <= loopCount; i++)
        {
            //Getting a random spawn postition from list
            int j = Random.Range(0, EnemiesSpawnPos.Count);
            spawnPos = EnemiesSpawnPos[j].transform.position;
            
            //Adding offset random x and y
            randomX = Random.Range(-1, 1);
            randomY = Random.Range(-1, 1);
            spawnPos.x += randomX;
            spawnPos.y += randomY;
            Transform pos = EnemiesSpawnPos[j].transform;
            pos.position.x.Equals(spawnPos.x);
            pos.position.y.Equals(spawnPos.y);

            //Instantiating and spawning, assigning homePos
            GameObject mobClone = Instantiate(enemy, spawnPos, Quaternion.identity);
            mobClone.GetComponent<EnemyController>().homePos = pos;
            NetworkServer.Spawn(mobClone);

            //Removing that location as it is taken already
            EnemiesSpawnPos.Remove(EnemiesSpawnPos[j]);

        }

    }
}
