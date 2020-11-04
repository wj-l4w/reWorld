using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;


public class LobbyManager : MonoBehaviour
{
    public TMP_Text ipAddressTextBox;
    public NetworkManager networkManager;

    string ipAddress;

    private void Awake()
    {
        networkManager = FindObjectOfType<NetworkManager>();
        ipAddress = networkManager.networkAddress;
        Debug.Log(ipAddress);
        ipAddressTextBox.text += ipAddress;
    }
}
