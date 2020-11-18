using Mirror;
using System;
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
    public TMP_Text IPAddressTextBox;

    [Header("Camera")]
    public float camSmoothing;
    public Vector3 camOffset;

    [Header("Stats")]
    public float moveSpeed = 1f;
    public int maxHp = 100;
    //m = mage, w = warrior
    [SyncVar]
    public char playerClass = 'w';
    [SyncVar]
    public bool isReady = false;
    [SyncVar]
    public int currentHp;
    [SyncVar]
    public bool canShoot = false;

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

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

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

    public override void OnStartLocalPlayer()
    {
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector2(0, 0);

        playerNameObj.transform.localPosition = new Vector2 (-0.05f, 0.25f);
        playerNameObj.transform.localScale = new Vector2(0.1f, 0.1f);

        string name = PlayerNameInput.DisplayName;
        CmdSetupPlayer(name);

        //If player is host and client
        if (!isClientOnly)
        {
            isReady = true;
        }
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
        npc.rpcRequestWarriorClass(this.netIdentity.netId);
    }

    [Command]
    public void CmdRequestMageClass()
    {
        NPC npc = FindObjectOfType<NPC>();
        npc.rpcRequestMageClass(this.netIdentity.netId);
    }

    [Command]
    public void CmdReadyUp()
    {
        LobbyManager lobbyManager = FindObjectOfType<LobbyManager>();
        lobbyManager.rpcReady(this.netIdentity.netId);
    }

    [Command]
    public void CmdStartGame()
    {
        LobbyManager lobbyManager = FindObjectOfType<LobbyManager>();
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
}
