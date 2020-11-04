using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
<<<<<<< HEAD
using Mirror;
<<<<<<< HEAD
<<<<<<< HEAD
=======

>>>>>>> parent of 9de681e... reverting to without networking

public class MainMenu : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 94c0a9b... fixed github issues on "TseWeoi" branch
=======

public class MainMenu : MonoBehaviour
{
>>>>>>> parent of 354de03... Merge pull request #11 from InstaKarma5-1/TseWeoiNewBitccchh
=======


public class MainMenu : MonoBehaviour
{
>>>>>>> parent of b696d40... reverting to without networking
=======
>>>>>>> parent of 9de681e... reverting to without networking
    [Header("Network")]
    [SerializeField] private NetworkManager networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 9de681e... reverting to without networking

<<<<<<< HEAD
=======
>>>>>>> parent of 15ed22f... Merge branch 'master' into KhaiXian
<<<<<<< HEAD
=======
>>>>>>> parent of 94c0a9b... fixed github issues on "TseWeoi" branch
=======
>>>>>>> parent of 354de03... Merge pull request #11 from InstaKarma5-1/TseWeoiNewBitccchh
=======
>>>>>>> parent of ca62f8e... Added ip address text field
=======
>>>>>>> parent of b696d40... reverting to without networking
=======
>>>>>>> parent of 9de681e... reverting to without networking
    public GameObject settingsPanel;
    public void StartGame()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 94c0a9b... fixed github issues on "TseWeoi" branch
=======
>>>>>>> parent of 354de03... Merge pull request #11 from InstaKarma5-1/TseWeoiNewBitccchh
=======
>>>>>>> parent of b696d40... reverting to without networking
=======
>>>>>>> parent of 9de681e... reverting to without networking
        networkManager.StartHost();

        landingPagePanel.SetActive(false);
        networkManager.ServerChangeScene(networkManager.onlineScene);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
        SceneManager.LoadScene(1);
>>>>>>> parent of 15ed22f... Merge branch 'master' into KhaiXian
=======
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.MoveGameObjectToScene(this.Player, SceneManager.GetActiveScene().buildIndex + 1);
>>>>>>> parent of 94c0a9b... fixed github issues on "TseWeoi" branch
=======
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.MoveGameObjectToScene(this.Player, SceneManager.GetActiveScene().buildIndex + 1);
>>>>>>> parent of 354de03... Merge pull request #11 from InstaKarma5-1/TseWeoiNewBitccchh
=======
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.MoveGameObjectToScene(this.Player, SceneManager.GetActiveScene().buildIndex + 1);
>>>>>>> parent of ca62f8e... Added ip address text field
=======
>>>>>>> parent of b696d40... reverting to without networking
=======
=======
        SceneManager.LoadScene(1);
>>>>>>> parent of 15ed22f... Merge branch 'master' into KhaiXian
>>>>>>> parent of 9de681e... reverting to without networking
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
