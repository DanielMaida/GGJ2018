using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public enum MenuState{
        start,
        credits,
        levelSelect
    }

    private MenuState myState = MenuState.start;

    public GameObject panel_Start;
    public GameObject panel_Credits;
    public GameObject panel_LevelSelect;

    public Button btn_level2;
    public Button btn_level3;
    public Button btn_level4;

    public Button btn_resetLvs;

    public GameObject[] catList;

    private void Awake()
    {

        int firstMenu = PlayerPrefs.GetInt("StartMenuAt", 0);
        switch (firstMenu)
        {
            case 0:
                myState = MenuState.start;
                break;
            case 1:
                PlayerPrefs.SetInt("StartMenuAt", 0);
                myState = MenuState.credits;
                break;
            case 2:
                PlayerPrefs.SetInt("StartMenuAt", 0);
                myState = MenuState.levelSelect;
                break;
        }


        verifyState();
    }

    private void verifyState()
    {
        disableAllMenus();

        switch (myState)
        {
            case MenuState.start:
                panel_Start.SetActive(true);

                if (PlayerPrefs.GetInt("level3Finished", 0) != 0)
                {
                    //Ai o player Zerou o jogo ...
                    btn_resetLvs.gameObject.SetActive(true);
                }
                else
                {
                    btn_resetLvs.gameObject.SetActive(false);
                }

                int resetCount = PlayerPrefs.GetInt("resetCount", 0);

                //Nao sei se pode fazer isso dentro de um for
                for(int x = 0; x < resetCount && x < catList.Length; x++)
                {
                    catList[x].SetActive(true);
                }

                break;
            case MenuState.credits:
                panel_Credits.SetActive(true);
                break;
            case MenuState.levelSelect:
                panel_LevelSelect.SetActive(true);

                if (PlayerPrefs.GetInt("level0Finished", 0) != 0)
                {
                    btn_level2.interactable = true;
                }

                if (PlayerPrefs.GetInt("level1Finished", 0) != 0)
                {
                    btn_level3.interactable = true;
                }

                if (PlayerPrefs.GetInt("level2Finished", 0) != 0)
                {
                    btn_level4.interactable = true;
                }

                if (PlayerPrefs.GetInt("level3Finished", 0) != 0)
                {
                    //Ai o player Zerou o jogo ...
                }

                break;
        }
    }

    private void disableAllMenus()
    {
        panel_Start.SetActive(false);
        panel_LevelSelect.SetActive(false);
        panel_Credits.SetActive(false);
    }

    //MENU START

    public void Btn_Start_Play()
    {
        myState = MenuState.levelSelect;
        verifyState();
    }

    public void Btn_Start_Credits() {
        myState = MenuState.credits;
        verifyState();
    }

    public void Btn_Start_Reset()
    {
        PlayerPrefs.SetInt("level0Finished",0);
        PlayerPrefs.SetInt("level1Finished", 0);
        PlayerPrefs.SetInt("level2Finished", 0);
        PlayerPrefs.SetInt("level3Finished", 0);

        int resetCount = PlayerPrefs.GetInt("resetCount", 0);
        resetCount++;
        PlayerPrefs.SetInt("resetCount", resetCount);
        verifyState();
    }

    //MENU CREDITS

    public void Btn_Credits_Back()
    {
        myState = MenuState.start;
        verifyState();
    }

    //MENU LEVEL SELECT

    public void Btn_Level_Back()
    {
        myState = MenuState.start;
        verifyState();
    }

    public void Btn_Level_1()
    {
        StartLevel(1);
    }

    public void Btn_Level_2()
    {
        StartLevel(2);
    }

    public void Btn_Level_3()
    {
        StartLevel(3);
    }

    public void Btn_Level_4()
    {
        StartLevel(4);
    }

    private void StartLevel(int level)
    {
        PlayerPrefs.SetInt("levelSelected", level);
        SceneManager.LoadScene(2);
    }

}
