using Mirror;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Player : NetworkBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public TextMesh playerNameText;
    public GameObject playerNameObj;
    public Camera playerCam;
    public Warrior warriorScript;
    public Mage mageScript;
    public TalentTree talentTree;
    public HUD hud;
    public DeathUI deathScreen;
    public WinUI winScreen;
    public LobbyManager lobbyManager;

    [Header("Camera")]
    public float camSmoothing;
    public Vector3 camOffset;

    [Header("Stats")]
    [SyncVar]
    public float moveSpeed = 1f;
    [SyncVar]
    public int experience = 0;
    [SyncVar]
    public int level = 1;
    //m = mage, w = warrior
    [SyncVar]
    public char playerClass;
    [SyncVar]
    public bool isReady = false;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHp;
    public int maxHp = 100;
    [SyncVar]
    public bool canShoot = false;
    [SyncVar]
    public bool isDead = false;

    private Vector2 movement;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerNameStr;

    [Client]
    void Start()
    {
        playerCam = Camera.main;
    }

    [Client]
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        //This function will move EVERY player, which we dont want!
        //Only move player objects that the player has authority over (their own characters)
        if (!isLocalPlayer){
            return;
        }

        if (isDead)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (warriorScript.enabled) { warriorScript.warriorUpdate(); }
        else if (mageScript.enabled) { mageScript.mageUpdate(); }

        //Talking to NPC
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            NPC npc = GameObject.FindObjectOfType<NPC>();
            LobbyManager lobby = GameObject.FindObjectOfType<LobbyManager>();
            if (Input.GetKeyDown(KeyCode.Space) && npc.playerInRange)
            {
                if (!npc.dialogBox.activeInHierarchy) {
                    npc.dialogBox.SetActive(true);
                    npc.dialogText.text = npc.dialog;
                    lobby.player = this;
                }                
            }
        }

        //Talent tree
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                talentTree.GetComponent<Canvas>().enabled = true;
                talentTree.playerId = netIdentity.netId;            
            }
        }

    }

    private void Move()
    {
        //Moving Camera
        if (isLocalPlayer)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, transform.position + camOffset, camSmoothing);
            playerCam.transform.position = newPosition;
        }
        //Moving Player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Moving Name Tag
        /*if (!isLocalPlayer)
        {
            // make non-local players run this
            playerNameObj.transform.LookAt(Camera.main.transform);
            return;
        }*/
    }

    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerNameStr;
    }

    void OnChangeHealth(int Old, int _New)
    {
        currentHp = _New;
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector2(0, 0);

        playerNameObj.transform.localPosition = new Vector2(-0.05f, 0.25f);
        playerNameObj.transform.localScale = new Vector2(0.1f, 0.1f);

        string name = PlayerNameInput.DisplayName;
        CmdSetupPlayer(name);

        //Setting stats
        maxHp = 100;
        currentHp = maxHp;
        experience = 0;
        level = 1;

        //If player is host and client, auto ready
        if (!isClientOnly)
        {
            isReady = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            /*
            NetworkManager2 nm2 = FindObjectOfType<NetworkManager2>();
            nm2.checkPlayerClass(netIdentity.netId);*/
            //Finding components
            NetworkManager3 nm3 = FindObjectOfType<NetworkManager3>();
            nm3.checkPlayerClass(netIdentity.netId);
            talentTree = FindObjectOfType<TalentTree>();
            hud = FindObjectOfType<HUD>();
            deathScreen = FindObjectOfType<DeathUI>();
            winScreen = FindObjectOfType<WinUI>();

            talentTree.GetComponent<Canvas>().enabled = false;
            hud.SetMaxHealth(currentHp, maxHp, netId);
            hud.SetMaxExp(experience, 100, level, netId);
            

        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //Finding components
            hud = FindObjectOfType<HUD>();
            

            hud.SetMaxHealth(currentHp, maxHp, netId);
            hud.SetMaxExp(experience, 100, level, netId);

        }

        activateClassScripts();
    }



    [Command]
    public void CmdSetupPlayer(string _name)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerNameStr = _name;
    }

    [Command]
    public void CmdRequestWarriorClass()
    {
        NPC npc = FindObjectOfType<NPC>();
        npc.rpcRequestWarriorClass(this.connectionToClient, this.netIdentity.netId);
    }

    [Command]
    public void CmdRequestMageClass()
    {
        NPC npc = FindObjectOfType<NPC>();
        npc.rpcRequestMageClass(this.connectionToClient, this.netIdentity.netId);
    }

    [Command]
    public void CmdReadyUp()
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
        lobbyManager.rpcReady(netId);
    }

    [Command]
    public void CmdStartGame()
    {
        lobbyManager = FindObjectOfType<LobbyManager>();
        lobbyManager.rpcStartGame();
    }

    [Command]
    public void CmdActivateSignboard()
    {
        SignBoardCollider sbc = FindObjectOfType<SignBoardCollider>();
        sbc.rpcSignboardActivate(this.connectionToClient);
    }


    [Command]
    public void CmdDeactivateSignboard()
    {
        SignBoardCollider sbc = FindObjectOfType<SignBoardCollider>();
        sbc.rpcSignboardDeactivate(this.connectionToClient);
    }

    public void takeDamage(int dmg)
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (isServer)
            {
                currentHp -= dmg;
                hud.SetHealth(currentHp, maxHp, netId);
            }
            else
            {
                CmdTakeDamage(dmg);
            }


            if (currentHp <= 0)
            {
                die();
            }
        }

    }

    [Command]
    public void CmdTakeDamage(int dmg)
    {
        takeDamage(dmg);
    }

    [Command]
    public void CmdDealDamage(uint netid, int damage)
    {
        if (NetworkIdentity.spawned[netid].gameObject.GetComponent<EnemyController>() != null)
        {
            EnemyController enemy = NetworkIdentity.spawned[netid].gameObject.GetComponent<EnemyController>();
            enemy.RpcTakeDamage(damage);
        }
        else if(NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>() != null)
        {
            Player enemy = NetworkIdentity.spawned[netid].gameObject.GetComponent<Player>();
            enemy.takeDamage(damage);
        }
        
    }

    public void healDamage(int healing)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (isServer)
            {
                currentHp += healing;
                if(currentHp >= maxHp)
                {
                    currentHp = maxHp;
                }

                hud.SetHealth(currentHp, maxHp, netId);
            }
            else
            {
                CmdHealDamage(healing);
            }
        }

    }

    [Command]
    public void CmdHealDamage(int healing)
    {
        healDamage(healing);
    }

    private void die()
    {
        Debug.Log("Player " + netId + " has died");
        animator.SetTrigger("isDead");
        isDead = true;
        deathScreen = FindObjectOfType<DeathUI>();
        deathScreen.GetComponent<Canvas>().enabled = true;
        StartCoroutine(deathScreen.FadeIn(netId));
    }

    public IEnumerator HealingOverTime(int healing)
    {
        while (true)
        {
            if (currentHp < maxHp)
            {
                healDamage(healing);
                Debug.Log("Healed player for " + healing);
            }
            yield return new WaitForSeconds(5); // Wait 3 secs;

        }

    }

    public void activateClassScripts()
    {
        //If player is warrior activate script
        if (playerClass == 'w')
        {
            warriorScript.enabled = true;
        }
        else if (playerClass == 'm')
        {
            mageScript.enabled = true;
        }
    }

    public char getActiveScript()
    {
        if (warriorScript.isActiveAndEnabled)
        {
            return 'w';
        }
        else if (mageScript.isActiveAndEnabled)
        {
            return 'm';
        }
        else
        {
            return 'z';
        }
    }

    public void changeMoveSpeed(float speed)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (isServer)
            {
                moveSpeed += speed;
            }
            else
            {
                CmdChangeMoveSpeed(speed);
            }
        }

    }

    [Command]
    public void CmdChangeMoveSpeed(float speed)
    {
        changeMoveSpeed(speed);
    }

    public void addExp(int exp)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (isServer)
            {
                //Debug.Log("Player " + netId + " has received " + exp + " exp");
                experience += exp;
                if(experience >= 100)
                {
                    level++;
                    experience -= 100;
                    talentTree.levelUp();
                }

                hud.SetExp(experience, 100, level, netId);
            }
            else
            {
                CmdAddExp(exp);
            }
        }

    }

    [Command]
    public void CmdAddExp(int exp)
    {
        addExp(exp);
    }

    public void DeathReturnToMenu()
    {
        deathScreen.ReturnToMenu(netId);
    }
    public void WinReturnToMenu()
    {
        winScreen = FindObjectOfType<WinUI>();
        winScreen.ReturnToMenu(netId);
    }

    [Command]
    public void CmdSetHealth(int health, int maxHealth, uint netid)
    {
        hud.SetHealth(health, maxHealth, netid);
    }
    [Command]
    public void CmdSetMaxHealth(int health, int maxHealth, uint netid)
    {
        hud.SetMaxHealth(health, maxHealth, netid);
    }

    [Command]
    public void CmdSetExp(int exp, int maxExp, int level, uint netid)
    {
        hud.SetExp(exp, maxExp, level, netid);
    }
    [Command]
    public void CmdSetMaxExp(int exp, int maxExp, int level, uint netid)
    {
        hud.SetExp(exp, maxExp, level, netid);
    }

}


