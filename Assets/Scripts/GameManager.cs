using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [SerializeField] public GameObject[] playerArr;
    [SerializeField] public List<GameObject> alivePlayerArr = new List<GameObject>();
    [SerializeField] public WinUI winUI;

    void Start()
    {
        FindPlayers();
    }

    void Update()
    {
        GetAlivePlayers();
        Invoke(nameof(checkWin), 5);
        
    }

    private void FindPlayers()
    {
        playerArr = GameObject.FindGameObjectsWithTag("Player");
    }

    public void GetAlivePlayers()
    {
        alivePlayerArr.Clear();
        playerArr = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerArr.Length; i++)
        {
            Player playa = playerArr[i].GetComponent<Player>();
            if (!playa.isDead)
            {
                alivePlayerArr.Add(playerArr[i]);
            }
        }
    }

    public void checkWin()
    {
        if (alivePlayerArr.Count == 1)
        {
            winUI.GetComponent<Canvas>().enabled = true;
        }
    }
}
