using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        verifyState();
    }

    private void verifyState()
    {
        disableAllMenus();

        switch (myState)
        {
            case MenuState.start:
                panel_Start.SetActive(true);
                break;
            case MenuState.credits:
                panel_Credits.SetActive(true);
                break;
            case MenuState.levelSelect:
                panel_LevelSelect.SetActive(true);

                if (PlayerPrefs.GetInt("level1Finished", 0) != 0)
                {
                    btn_level2.interactable = true;
                }

                if (PlayerPrefs.GetInt("level2Finished", 0) != 0)
                {
                    btn_level3.interactable = true;
                }

                if (PlayerPrefs.GetInt("level3Finished", 0) != 0)
                {
                    btn_level4.interactable = true;
                }

                if (PlayerPrefs.GetInt("level4Finished", 0) != 0)
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
        StartLevel(0);
    }

    public void Btn_Level_2()
    {
        StartLevel(1);
    }

    public void Btn_Level_3()
    {
        StartLevel(2);
    }

    public void Btn_Level_4()
    {
        StartLevel(3);
    }

    private void StartLevel(int level)
    {

    }

}
