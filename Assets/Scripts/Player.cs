using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public TextMesh playerNameText;
    public GameObject playerNameObj;
    public Camera playerCam;

    [Header("Camera")]
    public float camSmoothing;
    public Vector3 camOffset;

    [Header("Tuning Stats")]
    public float moveSpeed = 1f;

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
    }

    private void Move()
    {
        //Moving Camera
        Vector3 newPosition = Vector3.Lerp(transform.position, transform.position + camOffset, camSmoothing);
        playerCam.transform.position = newPosition;
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
    }

    [Command]
    public void CmdSetupPlayer(string _name)
    {
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerNameStr = _name;
    }

}
