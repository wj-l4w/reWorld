using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthManager : NetworkBehaviour
{
    public int currentHealth;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [TargetRpc]
    public void DmgPlayer(int DmgToGive){
        currentHealth -= DmgToGive;
        if(currentHealth <= 0){
            gameObject.SetActive(false);
        }
    }
}
