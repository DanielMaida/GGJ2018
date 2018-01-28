using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public EnergyIcon energyIcon;
    public Vidas vidasBar;

    private AudioManager audioManager;
    public AudioClip carpetSound;
    public AudioClip winClip;

    public int fireZone; 

    public FireBar fireBar;
    public Animator girlAnimator;
    public Animator fogueiraAnimator;
    public Animator catAnimator;
    public GameObject fumaca;

    public int score = 0;
    public int maxVidas = 12;
    public int vidas = 12;

    public int groupCount = 0;
    public bool winGame = false;
    public bool looseGame = false;

    BeatManager beatManager;

    private int[] level1 = new int[] { 0, 1, 1, 1, 2, 2, 1, 3, 1, 2, 1, 2, 1, 3, 2, 1, 1, 1, 2, 1 };
    private int[] level2 = new int[] { 0, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 3, 3, 3, 3, 2, 3, 2, 3 };
    private int[] level3 = new int[] { 0, 3, 3, 3, 2, 2, 3, 3, 3, 3, 1, 2, 3, 3, 1, 3, 1, 3, 3, 2, 3, 2, 3, 2, 3 };
    private int[] level4 = new int[] { 0, 3, 2, 3, 3, 3, 2, 3, 2, 3, 2, 2, 3, 3, 2, 3, 1, 3, 3, 2, 3, 2, 3, 3, 2, 3 };

    private int[][] levelList = new int[4][];

    private void Awake()
    {
        beatManager = GetComponent<BeatManager>();
        audioManager = GetComponent<AudioManager>();
        energyIcon = transform.GetComponentInChildren<EnergyIcon>();

        EnergyIcon.OnEnergyStateChange += OnEnergyStateChange;
        EnergyIcon.OnFireClick += onfireClick;

        BeatManager.OnCorrectClick += OnCorrectClick;
        BeatManager.OnWrongClick += OnWrongClick;
        BeatManager.OnMissClick += OnMissClick;
        BeatManager.OnLevelCompleted += OnLevelCompleted;
        BeatManager.OnGroupCompleted += OnGroupCompleted;

        levelList[0] = level1;
        levelList[1] = level2;
        levelList[2] = level3;
        levelList[3] = level4;

        PlayerPrefs.SetInt("levelSelected", 1);
        int currentLevel = PlayerPrefs.GetInt("levelSelected", 0);

        beatManager.ConfigureLevel(levelList[currentLevel]);
        fireBar.SetLevel(currentLevel);

    }

    private void onfireClick()
    {
        catAnimator.SetTrigger("action");
    }

    private void OnEnergyStateChange(int _state)
    {
        fireZone = _state;
        if (_state == 2 && beatManager.levelConfigured && !beatManager.running)
        {
            beatManager.StartLevel();
        }

        switch (_state)
        {
            case 0:
                fogueiraAnimator.SetBool("started", false);
                break;
            case 1:
                fogueiraAnimator.SetBool("started", true);
                if (fogueiraAnimator.GetBool("boa"))
                {
                    fogueiraAnimator.SetBool("boa", false);
                }
                break;
            case 2:
                fogueiraAnimator.SetBool("boa", true);
                if (fogueiraAnimator.GetBool("fogo"))
                {
                    fogueiraAnimator.SetBool("fogo", false);
                }
                if (girlAnimator.GetBool("burnning"))
                {
                    girlAnimator.SetBool("burnning", false); 
                }
                break;
            case 3:
                Debug.Log("vamos ver");
                fogueiraAnimator.SetBool("fogo", true);
                girlAnimator.SetBool("burnning", true);
                catAnimator.SetTrigger("burn");
                break;
        }
    }

    private void OnWrongClick()
    {
        girlAnimator.SetTrigger("error");
        vidas--;
        if(vidas <= 0)
        {
            vidas = 0;
            Perdeu();
        }
        vidasBar.SetVidas(vidas);
    }

    private void OnMissClick()
    {
        vidas--;
  
        if (vidas <= 0)
        {
            vidas = 0;
            Perdeu();
        }

        vidasBar.SetVidas(vidas);
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
        audioManager.PlayCarpetSound(carpetSound);
    }

    private void Perdeu()
    {
        SceneManager.LoadScene("level_selection");
    }

    private void Ganhou()
    {
        beatManager.running = false;
        winGame = true;
        girlAnimator.SetTrigger("win");
        audioManager.PlayWinSound(winClip);
        fumaca.SetActive(true);
        Invoke("CutsceneTransition", winClip.length);
    }

    private void CutsceneTransition() {
        SceneManager.LoadScene("cutscene_1");

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
