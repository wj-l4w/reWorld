using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    private HealthManager healthMan;
    private float DmgCd = 0f;
    private bool isAttacking;
    [SerializeField] private int DmgToGive = 10;
    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(reloading){
            loadingTime -= Time.deltaTime;
            if(loadingTime<=0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }*/

        if(isAttacking){
            DmgCd -= Time.deltaTime;
            if(DmgCd <= 0){
                healthMan.DmgPlayer(DmgToGive);
                DmgCd = 0.5f;
            }
        }
        
    }
    
    /*private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player"){
            other.gameObject.GetComponent<HealthManager>().DmgPlayer(dmgGiven);
            // reloading = true;
        }  
    }
    */
    private void OnCollisionEnter2D(Collision2D other) {
        isAttacking = true;
    }
    private void OnCollisionStay2D(Collision2D other) {
        isAttacking = true;

    }
}
