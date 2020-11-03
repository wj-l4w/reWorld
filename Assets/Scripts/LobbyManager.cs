using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        //ipAddress = networkManager.networkAddress;
        ipAddress = getIpAddress();
        ipAddressTextBox.text += ipAddress;
    }

    private string getIpAddress()
    {
        IPHostEntry host;
        string localIP = "0.0.0.0";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
