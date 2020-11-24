using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Fireball : NetworkBehaviour
{
    [Header("Stats")]
    [SyncVar]
    public int damage;
    [SyncVar]
    public float speed;
    [SyncVar]
    public float lifeTime;

    [Header("Other Stuffs")]
    [SyncVar]
    public uint playerId;


    private void Start()
    {
        Invoke(nameof(DestroyProjectile), lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<AudioManager>().Play("FireballOnHit");
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>() != null)
            {
                Player enemy = collision.GetComponent<Player>();
                //If not self, deal damage to player
                Debug.Log("Enemy netId is " + enemy.netId);
                Debug.Log("Player netId is " + playerId);
                if (enemy.netId != playerId)
                {
                    enemy.CmdDealDamage(enemy.netId, damage);
                    NetworkServer.Destroy(gameObject);
                }
            }
           

        }
        //If enemy (mobs)
        else if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<EnemyController>() != null)
            {
                EnemyController enemy = collision.GetComponent<EnemyController>();
                Player player = NetworkIdentity.spawned[playerId].gameObject.GetComponent<Player>();
                player.CmdDealDamage(enemy.netId, damage);
            }
        NetworkServer.Destroy(gameObject);
        }
        else if(collision.CompareTag("Collider"))
        {
            NetworkServer.Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void DestroyProjectile()
    {
        //Instantiate(transform.position, Quaternion.identity);
        NetworkServer.Destroy(gameObject);
    }
}
