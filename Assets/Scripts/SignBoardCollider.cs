using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SignBoardCollider : NetworkBehaviour
{
    public GameObject signBoardText;

    private void Start()
    {
        signBoardText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playa = other.GetComponent<Player>();
            playa.CmdActivateSignboard();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playa = other.GetComponent<Player>();
            playa.CmdDeactivateSignboard();
        }
    }

    [TargetRpc]
    public void rpcSignboardActivate(NetworkConnection conn)
    {
        signBoardText.SetActive(true);
    }
    [TargetRpc]
    public void rpcSignboardDeactivate(NetworkConnection conn)
    {
        signBoardText.SetActive(false);
    }

}
