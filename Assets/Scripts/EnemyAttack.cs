using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyAttack : NetworkBehaviour
{
    public Player target;
    public float DmgCd = 0f;
    private float atkCd;
    private bool isAttacking = false;
    [SerializeField] private int DmgToGive = 10;
    // Start is called before the first frame update
    void Start()
    {
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

        attack();

    }

    /*private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Player"){
            other.gameObject.GetComponent<HealthManager>().DmgPlayer(dmgGiven);
            // reloading = true;
        }  
    }
    */
    private void OnCollisionStay2D(Collision2D other) {
        isAttacking = true;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        isAttacking = false;
    }

    private void attack()
    {
        if (isAttacking)
        {
            atkCd -= Time.deltaTime;
            if (atkCd <= 0)
            {
                rpcDmgPlayer(target.connectionToClient, DmgToGive);
                atkCd = DmgCd;
            }
        }
    }

    [TargetRpc]
    public void rpcDmgPlayer(NetworkConnection conn, int DmgToGive)
    {
        target.takeDamage(DmgToGive);
        EnemyAttack ea = GetComponent<EnemyAttack>();
        Debug.Log(ea.netId + " has hit player " + target.netId + " for " + DmgToGive + " damage");
    }


}
