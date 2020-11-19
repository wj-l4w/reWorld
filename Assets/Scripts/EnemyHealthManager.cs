using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyHealthManager : NetworkBehaviour
{
    [SyncVar]
    public int currentHealth;
    [SyncVar]
    public int maxHealth;

    [Command]
    public void takeDamage(int dmgToGive)
    {
        currentHealth -= dmgToGive;
        if (currentHealth <= 0)
        {
            //If die
            //Destroy(gameObject);
            NetworkServer.Destroy(gameObject);
        }
    }

}
