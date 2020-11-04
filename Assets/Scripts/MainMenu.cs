using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
<<<<<<< HEAD
    [Header("Network")]
    [SerializeField] private NetworkManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;

=======
>>>>>>> parent of 15ed22f... Merge branch 'master' into KhaiXian
    public GameObject settingsPanel;
    public void StartGame()
    {
<<<<<<< HEAD
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
        networkManager.ServerChangeScene(networkManager.onlineScene);
=======
        SceneManager.LoadScene(1);
>>>>>>> parent of 15ed22f... Merge branch 'master' into KhaiXian
    }
    
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
