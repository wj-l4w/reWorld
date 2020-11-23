using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;
using System;

public class MoveSpeedBuff : Talent
{
    [SerializeField] private TalentTree talentTree;
    [SerializeField] private float moveSpeedAmount = 0f;
    [SerializeField] private float moveSpeedBuff = 0.2f;
    public Player player;
    


    public override bool Click()
    {
        if (base.Click())
        {
            moveSpeedAmount += moveSpeedBuff;
            player = NetworkIdentity.spawned[talentTree.playerId].gameObject.GetComponent<Player>();
            player.changeMoveSpeed(moveSpeedAmount);
            Debug.Log("Increased movement speed by " + moveSpeedAmount);

            return true;
        }

        return false;
    }

    
}
