using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    [Header("Network")]
    [SerializeField] private NetworkManager2 networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

    public GameObject settingsPanel;
    

    public void HostLobby()
    {
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
        networkManager.ServerChangeScene(networkManager.onlineScene);
    }

    /*public void StartGame()
    {
        SceneManager.LoadScene(mainMenuScene.name);
    }*/
    
    public void ExitGame(){
        Debug.Log("Exit game");
        Application.Quit();    
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void SaveSettings()
    {
        //TODO: Save into file
        settingsPanel.SetActive(false);
    }



    
}
