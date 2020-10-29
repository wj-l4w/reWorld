using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private GameObject loadingPanel = null;
    [SerializeField] private GameObject connectFailPanel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void OnEnable()
    {
        //LobbyNetworkManager.OnClientConnected += HandleClientConnected;
        //LobbyNetworkManager.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        //LobbyNetworkManager.OnClientConnected -= HandleClientConnected;
       // LobbyNetworkManager.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;

        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        loadingPanel.SetActive(true);
        joinButton.interactable = false;
    }

    public void checkIPAddress(string ip)
    {
        //If IP is not null, then allow them to continue
        joinButton.interactable = !string.IsNullOrEmpty(ip);
    }

    private void HandleClientConnected()
    {
        joinButton.interactable = true;

        loadingPanel.SetActive(false);
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        loadingPanel.SetActive(false);
        connectFailPanel.SetActive(true);
        joinButton.interactable = true;
    }
}
