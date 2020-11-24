﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathUI : NetworkBehaviour
{
    [SerializeField] public CanvasGroup deathUi;
    [SerializeField] public float alpha = 0f;
    [SerializeField] public float targetAlpha = 1f;
    [SerializeField] public float FadeRate = 0.05f;
    public Button BackToMenu;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        BackToMenu.onClick.AddListener(() => { player.DeathReturnToMenu(); });
    }

    public void ReturnToMenu(uint netId)
    {
            CmdReturnToMenu(netId);
    }

    [Command]
    public void CmdReturnToMenu(uint netId)
    {
        Player player = NetworkIdentity.spawned[netId].gameObject.GetComponent<Player>();
        RpcBackToMenu(player.connectionToClient, netId);
    }

    [TargetRpc]
    public void RpcBackToMenu(NetworkConnection conn, uint netId)
    {
        Player player = NetworkIdentity.spawned[netId].gameObject.GetComponent<Player>();
        player.connectionToClient.Disconnect();
        Application.Quit();
    }

}
