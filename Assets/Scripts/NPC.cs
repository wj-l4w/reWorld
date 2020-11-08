using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : NetworkBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
    private Queue<string> sentences;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        sentences = new Queue<string>();
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
    public void RpcSelectWarriorClass()
    {
       player.playerClass = 'w';
    }

    [ClientRpc]
    public void RpcSelectMageClass()
    {
        player.playerClass = 'm';
    }
}
