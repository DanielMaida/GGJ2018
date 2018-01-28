using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    public Text dialogueText;
    public CutsceneManager cutsceneManager;
    private Queue<string> sentences;
    private AudioSource audioSource;
    public Animator animator;
    public PlayBlip playBlip;
    bool textEnd;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue) {
        animator.SetBool("isOpen", true);

        sentences = new Queue<string>();
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        StartCoroutine(Blipper());
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
        textEnd = true;
        
    }

    IEnumerator Blipper()
    {
        while (!textEnd)
        {
            playBlip.Blip();
            yield return new WaitForSeconds(0.1f);
        }
        textEnd = false;
    }
    
    
    void EndDialogue() {
        animator.SetBool("isOpen", false);
        Invoke("NextLevel", 1);
    }

    void NextLevel(){
        //coloca pra ele começar na selecao de level
        cutsceneManager.cutscenes[PlayerPrefs.GetInt("levelSelected")].SetActive(false);
        PlayerPrefs.SetInt("StartMenuAt", 2);
        if (PlayerPrefs.GetInt("levelSelected") == 3)
        {
            PlayerPrefs.SetInt("StartMenuAt", 1);
        }
        SceneManager.LoadScene(0);
    }
}
