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
        playerArr = GameObject.FindGameObjectsWithTag("Player");
    }

    void FixedUpdate()
    {
        GetAlivePlayers();
        checkWin();
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
            StartCoroutine(winUI.FadeIn(alivePlayerArr[0].GetComponent<Player>().netId));
        }
    }
}
