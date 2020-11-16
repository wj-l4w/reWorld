using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : NetworkBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog = "What class will you choose?";
    public bool playerInRange;
    private Queue<string> sentences;

/*    void Start()
    {
        sentences = new Queue<string>();
        dialogBox = GameObject.Find("Dialog Box Canvas");
        dialogText = GameObject.Find("Dialog Box Canvas/Dialog box/DialogText").GetComponent<Text>();
    }
*/
    public override void OnStartClient()
    {
        base.OnStartClient();

        sentences = new Queue<string>();
        dialogBox = GameObject.Find("Dialog Box Canvas");
        dialogText = GameObject.Find("Dialog Box Canvas/Dialog box/DialogText").GetComponent<Text>();
        if (dialogBox.activeInHierarchy)
        {
            dialogBox.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Debug.Log("displays next sentence");
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
    }

    void EndDialogue()
    {

    }

    [ClientRpc]
    public void rpcRequestWarriorClass(uint id)
    {
        Player player = NetworkIdentity.spawned[id].gameObject.GetComponent<Player>();
        player.playerClass = 'w';
    }

    [ClientRpc]
    public void rpcRequestMageClass(uint id)
    {
        Player player = NetworkIdentity.spawned[id].gameObject.GetComponent<Player>();
        player.playerClass = 'm';
    }

}
