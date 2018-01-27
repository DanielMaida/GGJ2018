using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour {

    public int numberOfLives;
    public GameObject[] LifeIcons;

    public void Start()
    {
        if (PlayerPrefs.GetInt("Level") == 0) {
            PlayerPrefs.SetInt("Level", 1);
        } 
    }

    public void DecreaseLife() {
        numberOfLives--;
        Destroy(LifeIcons[numberOfLives]);
        if(numberOfLives <= 0)
            SceneManager.LoadScene("main_menu");
    }

    public void CompleteLevel() {
        int level = PlayerPrefs.GetInt("Level") + 1;
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene("cutscene_" + level); /*colocar aqui a transicao da cutscene.*/
    }
}
