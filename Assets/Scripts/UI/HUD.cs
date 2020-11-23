using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HUD : NetworkBehaviour
{
    public Slider HealthSlider;
    public Slider ExpSlider;
    public GameObject HealthBar;
    public GameObject ExpBar;
    public Text HealthText;
    public Text ExpText;
    public Text LevelText;

    private void Start()
    {
        HealthSlider = HealthBar.GetComponent<Slider>();
        ExpSlider = ExpBar.GetComponent<Slider>();
    }

    public void SetHealth(int health, int maxHealth, uint netid)
    {
        if (isServer)
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            RpcSetHealth(player.connectionToClient, health, maxHealth);
        }
        else
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            player.CmdSetHealth(health, maxHealth, netid);
        }
        
    }

    public void SetMaxHealth(int health, int maxHealth, uint netid)
    {
        if (isServer)
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            RpcSetMaxHealth(player.connectionToClient, health, maxHealth);
        }
        else
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            player.CmdSetMaxHealth(health, maxHealth, netid);
        }

    }


    [TargetRpc]
    public void RpcSetHealth(NetworkConnection conn, int health, int maxHealth)
    {
        HealthSlider.value = health;
        HealthText.text = string.Format("{0}/{1}", health, maxHealth);
    }
    [TargetRpc]
    public void RpcSetMaxHealth(NetworkConnection conn, int health, int maxHealth)
    {
        Debug.Log("RpcSetMaxHealth called with health = " + health + " and maxHealth = " + maxHealth);
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = health;
        HealthText.text = string.Format("{0}/{1}", health, maxHealth);
    }


    public void SetExp(int exp, int maxExp, int level, uint netid)
    {
        if (isServer)
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            RpcSetExp(player.connectionToClient, exp, level, maxExp);
        }
        else
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            player.CmdSetExp(exp, maxExp, level, netid);
        }

    }

    public void SetMaxExp(int exp, int maxExp, int level, uint netid)
    {
        if (isServer)
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            RpcSetMaxExp(player.connectionToClient, exp, level, maxExp);
        }
        else
        {
            Player player = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            player.CmdSetMaxExp(exp, maxExp, level, netid);
        }

    }

    [Command]
    public void CmdSetExp(int exp, int maxExp, int level, uint netid)
    {
        SetExp(exp, maxExp, level, netid);
    }
    [Command]
    public void CmdSetMaxExp(int exp, int maxExp, int level, uint netid)
    {
        SetExp(exp, maxExp, level, netid);
    }
    [TargetRpc]
    public void RpcSetExp(NetworkConnection conn, int exp, int level, int maxExp)
    {
        ExpSlider.value = exp;
        ExpText.text = string.Format("{0}/{1}", exp, maxExp);
        LevelText.text = level.ToString();
    }
    [TargetRpc]
    public void RpcSetMaxExp(NetworkConnection conn, int exp, int level, int maxExp)
    {
        ExpSlider.maxValue = maxExp;
        ExpSlider.value = exp;
        ExpText.text = string.Format("{0}/{1}", exp, maxExp);
        LevelText.text = level.ToString();
    }


}
