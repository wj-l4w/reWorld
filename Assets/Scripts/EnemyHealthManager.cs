using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyHealthManager : NetworkBehaviour
{
    public int currentHealth;
    public int maxHealth;

    [ClientRpc]
    public void cmdHurtEnemy(int dmgToGive){
        currentHealth -= dmgToGive;
        if(currentHealth <=0 ){
            //Despawn if die
            Destroy(gameObject);
            NetworkServer.Destroy(gameObject);

            //Give rewards
        }
    }
}
