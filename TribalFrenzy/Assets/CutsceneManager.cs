using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public GameObject[] cutscenes;

    public void Start()
    {
        int level = PlayerPrefs.GetInt("levelSelected", 0);
        switch (level)
        {
            case 0:
                cutscenes[0].SetActive(true);
                break;
            case 1:
                cutscenes[1].SetActive(true);
                break;
            case 2:
                cutscenes[2].SetActive(true);
                break;
            case 3:
                cutscenes[3].SetActive(true);
                break;
        }
    }
}
