using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue;
    private bool started;

    public void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Return) )
            TriggerDialogue();
        }

    public void TriggerDialogue() {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
