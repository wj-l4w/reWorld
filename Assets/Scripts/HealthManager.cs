using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
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

    public void DmgPlayer(int DmgToGive){
        currentHealth -= DmgToGive;
        if(currentHealth <= 0){
            gameObject.SetActive(false);
        }
    }
}
