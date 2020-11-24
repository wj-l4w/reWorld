using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinUI : NetworkBehaviour
{
    [SerializeField] public CanvasGroup winUi;
    [SerializeField] public float alpha = 0f;
    [SerializeField] public float targetAlpha = 1f;
    [SerializeField] public float FadeRate = 0.05f;
    public Button BackToMenu;

    private void Start()
    {
        Player player = FindObjectOfType<Player>();
        BackToMenu.onClick.AddListener(() => { player.WinReturnToMenu(); });
    }

    public void ReturnToMenu(uint netId)
    {
        if(isServer){
            Player player = NetworkIdentity.spawned[netId].gameObject.GetComponent<Player>();
            Debug.Log("The winner is player " + player.netId);
            RpcBackToMenu(player.connectionToClient, netId);
        }
        else
        {
            CmdReturnToMenu(netId);
        }
        
    }

    [Command]
    public void CmdReturnToMenu(uint netId)
    {
        ReturnToMenu(netId);
    }

    [TargetRpc]
    public void RpcBackToMenu(NetworkConnection conn, uint netId)
    {
        Player player = NetworkIdentity.spawned[netId].gameObject.GetComponent<Player>();
        player.connectionToClient.Disconnect();
        Application.Quit();
    }
}
