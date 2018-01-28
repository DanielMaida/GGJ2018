using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void CreditsButton() {
        SceneManager.LoadScene("credits_scene");
    }

    public void PlayButton() {
        SceneManager.LoadScene("BeatManagerSceneCreation");
    }

    public void BackButton() {
        SceneManager.LoadScene("main_menu");
    }
}
