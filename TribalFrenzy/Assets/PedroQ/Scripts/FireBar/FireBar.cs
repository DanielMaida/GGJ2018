﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBar : MonoBehaviour {

    public GameObject easyBar;
    public GameObject hardBar;

    public EnergyIcon energyIcon;

    private IconConfig[] configLevel = new IconConfig[4];

    private void Awake()
    {
        configLevel[0] = new IconConfig(2.0f,0.6f,true);
        configLevel[1] = new IconConfig(2.0f, 0.6f,true);
        configLevel[2] = new IconConfig(2.0f, 0.6f,false);
        configLevel[3] = new IconConfig(2.0f, 0.6f,false);
    }

    public void SetLevel(int level)
    {
        //configura a barra
        if (configLevel[level].isEasy)
        {
            easyBar.SetActive(true);
        }
        else
        {
            hardBar.SetActive(true);
        }

        //configura o icone
        energyIcon.force = configLevel[level].clickForce;
        energyIcon.rg.gravityScale = configLevel[level].gravity;


    }


}

public class IconConfig{

    public float clickForce;
    public float gravity;
    public bool isEasy;

    public IconConfig(float _clickForce, float _gravity, bool _isEasy)
    {
        clickForce = _clickForce;
        gravity = _gravity;
        isEasy = _isEasy;
    }

}


