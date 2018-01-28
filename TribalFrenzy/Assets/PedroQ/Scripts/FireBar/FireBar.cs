using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBar : MonoBehaviour {

    public GameObject easyBar;
    public GameObject hardBar;

    public EnergyIcon energyIcon;

    private IconConfig[] configLevel = new IconConfig[4];

    private void Awake()
    {
        configLevel[0] = new IconConfig(6.0f,0.4f,true, 5.5f);
        configLevel[1] = new IconConfig(4.0f, 0.5f,true, 5.0f);
        configLevel[2] = new IconConfig(3.0f, 0.55f,false, 4.0f);
        configLevel[3] = new IconConfig(3.0f, 0.6f,false, 3.5f);
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
        energyIcon.rg.drag = configLevel[level].linearDrag;

    }


}

public class IconConfig{

    public float clickForce;
    public float gravity;
    public bool isEasy;
    public float linearDrag;

    public IconConfig(float _clickForce, float _gravity, bool _isEasy, float _linearDrag)
    {
        clickForce = _clickForce;
        gravity = _gravity;
        isEasy = _isEasy;
        linearDrag = _linearDrag;
    }

}


