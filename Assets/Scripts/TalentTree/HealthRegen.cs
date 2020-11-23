using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;
using System;

public class HealthRegen : Talent
{
    [SerializeField] private TalentTree talentTree;
    [SerializeField] private int totalHealAmount;
    [SerializeField] private int healAmount;
    public Player player;
    


    public override bool Click()
    {
        if (base.Click())
        {
            totalHealAmount += healAmount;
            player = NetworkIdentity.spawned[talentTree.playerId].gameObject.GetComponent<Player>();
            StartCoroutine(player.HealingOverTime(totalHealAmount));
            Debug.Log("Started healing coroutine");

            return true;
        }

        return false;
    }

    
}
