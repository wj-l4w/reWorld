using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue){
        Debug.Log("Starting conversation");

        sentences.Clear();

        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        Debug.Log("displays next sentence");
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
    }

    void EndDialogue(){
        
    }

    public void ChooseWarriorClass(){
        Debug.Log("Player has chosen warrior");
    }
    public void ChooseMageClass(){
        Debug.Log("Player has chosen mage");
    }
}
