using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {

    static int numberOfLives;

    void Start()
    {
        numberOfLives = 3;
    }

    public static void DecreaseLife() {
        numberOfLives--;
        if(numberOfLives <= 0)
            SceneManager.LoadScene("main_menu");
    }
}
