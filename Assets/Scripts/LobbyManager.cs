using Mirror;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    public Player player;
    public Button warrior, mage;
    
    private void Start()
    {
        player = FindObjectOfType<Player>();
        warrior.onClick.AddListener(() => { player.CmdRequestMageClass();});
        mage.onClick.AddListener(() => { player.CmdRequestMageClass();});
    }



}
