using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Mirror;

public class DmgBuff : Talent
{
    [SerializeField] private TalentTree talentTree;
    public Player player;


    public override bool Click()
    {
        if (base.Click())
        {
           
            player = NetworkIdentity.spawned[talentTree.playerId].gameObject.GetComponent<Player>();
            
            if (player.getActiveScript() ==  'w')
            {
                Warrior warriorScript = player.GetComponent<Warrior>();
                warriorScript.damage += 10;
                Debug.Log("Damage is " + warriorScript.damage);
            }
            else if (player.getActiveScript() == 'm')
            {
                Mage mageScript = player.GetComponent<Mage>();
                mageScript.fireballLevel++;
            }
            else{
                Debug.Log("Trying to buff damage of player with no active class script");
            }

            return true;
        }

        return false;
    }
}
