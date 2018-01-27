using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public EnergyIcon energyIcon;
    public Animator girlAnimator;

    public int score = 0;
    public int maxVidas = 3;
    public int vidas = 3;

    public int groupCount = 0;
    public bool winGame = false;
    public bool looseGame = false;

    BeatManager beatManager;

    private int[] level1 = new int[] { 0, 1, 1, 1, 2, 2, 1, 3, 1, 2, 1, 2, 1, 3, 2, 1, 1, 1, 2, 1 };
    private int[] level2 = new int[] { 0, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 3, 3, 3, 3, 2, 3, 2, 3 };
    private int[] level3 = new int[] { 0, 3, 3, 3, 2, 2, 3, 3, 3, 3, 1, 2, 3, 3, 1, 3, 1, 3, 3, 2, 3, 2, 3, 2, 3 };

    private void Awake()
    {
        beatManager = GetComponent<BeatManager>();
        energyIcon = transform.GetComponentInChildren<EnergyIcon>();

        EnergyIcon.OnEnergyStateChange += OnEnergyStateChange;

        BeatManager.OnCorrectClick += OnCorrectClick;
        BeatManager.OnWrongClick += OnWrongClick;
        BeatManager.OnMissClick += OnMissClick;
        BeatManager.OnLevelCompleted += OnLevelCompleted;
        BeatManager.OnGroupCompleted += OnGroupCompleted;
    }

    private void OnEnergyStateChange(bool _state)
    {
        if (_state && beatManager.levelConfigured && !beatManager.running)
        {
            beatManager.StartLevel();
        }
    }

    private void OnWrongClick()
    {
        vidas--;
        if(vidas <= 0)
        {
            vidas = 0;
            Perdeu();
        }
    }

    private void OnMissClick()
    {
        vidas--;
        if (vidas <= 0)
        {
            vidas = 0;
            Perdeu();
        }
    }

    private void OnLevelCompleted()
    {
        Ganhou();
    }

    private void OnGroupCompleted()
    {
        groupCount++;
    }

    private void OnCorrectClick()
    {
        Debug.Log("Correto");
        score++;

        girlAnimator.SetTrigger("correct");
    }

    private void Perdeu()
    {

    }

    private void Ganhou()
    {
        beatManager.running = false;
        winGame = true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (beatManager.levelConfigured) return;
            beatManager.ConfigureLevel(level1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (beatManager.levelConfigured) return;
            beatManager.ConfigureLevel(level2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (beatManager.levelConfigured) return;
            beatManager.ConfigureLevel(level3);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!beatManager.levelConfigured) return;

            beatManager.StartLevel();
        }
    }
}
