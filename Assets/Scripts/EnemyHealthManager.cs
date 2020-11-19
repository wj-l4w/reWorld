using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HurtEnemy(int dmgToGive){
        currentHealth -= dmgToGive;
        if(currentHealth <=0 ){
            Destroy(gameObject);
        }
    }
}
