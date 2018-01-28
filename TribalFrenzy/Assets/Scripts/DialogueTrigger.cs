using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public Dialogue dialogue0;
    public Dialogue dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;

    private bool started;

    public void Start()
    {
        int level = PlayerPrefs.GetInt("levelSelected", 0);
        DialogueManager dialogManager = FindObjectOfType<DialogueManager>();

        switch (level)
        {
            case 0:
                dialogManager.StartDialogue(dialogue0);
                break;
            case 1:
                dialogManager.StartDialogue(dialogue1);
                break;
            case 2:
                dialogManager.StartDialogue(dialogue2);
                break;
            case 3:
                dialogManager.StartDialogue(dialogue3);
                break;
        }

        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue0);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Return) )
            TriggerDialogue();
        }

    public void TriggerDialogue() {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }
}
