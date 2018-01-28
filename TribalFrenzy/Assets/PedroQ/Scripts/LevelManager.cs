using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public EnergyIcon energyIcon;
    public Vidas vidasBar;

    private AudioManager audioManager;
    public AudioClip[] carpetSound;
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

    public Sprite[] smokeByLevel;

    public Sprite[] backgroundByLevel;
    public SpriteRenderer background;
    /*
    private int[] level1 = new int[] { 0, 1, 1, 1, 2, 2, 1, 3, 1, 2, 1, 2, 1, 3, 2, 1, 1, 1, 2, 1 };
    private int[] level2 = new int[] { 0, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 3, 3, 3, 3, 2, 3, 2, 3 };
    private int[] level3 = new int[] { 0, 3, 3, 3, 2, 2, 3, 3, 3, 3, 1, 2, 3, 3, 1, 3, 1, 3, 3, 2, 3, 2, 3, 2, 3 };
    private int[] level4 = new int[] { 0, 3, 2, 3, 3, 3, 2, 3, 2, 3, 2, 2, 3, 3, 2, 3, 1, 3, 3, 2, 3, 2, 3, 3, 2, 3 };
    */

    private int[] level1 = new int[] { 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2 };
    private int[] level2 = new int[] { 0, 2, 2, 1, 2, 3, 2, 2, 2, 1, 2, 1, 1, 2, 3, 2, 3, 2, 1, 1, 3, 1, 2 };

    private int[] level3 = new int[] { 0, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 1, 2, 2, 1, 1, 1, 2, 1, 2, 1, 1, 1 };
    private int[] level4 = new int[] { 0, 1, 1, 2, 2, 1, 1, 2, 2, 1, 3, 1, 2, 3, 2, 3, 1, 2, 2, 1, 2, 1, 2};

    private int[][] levelList = new int[4][];

    private int currentLevel = 0;

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

        

        catAnimator = GameObject.FindGameObjectWithTag("Cat").GetComponent<Animator>();
        fogueiraAnimator = GameObject.FindGameObjectWithTag("Fogueira").GetComponent<Animator>();
        girlAnimator = GameObject.FindGameObjectWithTag("Girl").GetComponent<Animator>();

    }

    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("levelSelected", 0);

        //currentLevel = 2;

        fireBar.SetLevel(currentLevel);
        background.sprite = backgroundByLevel[currentLevel];
        beatManager.ConfigureLevel(currentLevel, levelList[currentLevel]);



        catAnimator = GameObject.FindGameObjectWithTag("Cat").GetComponent<Animator>();
        fogueiraAnimator = GameObject.FindGameObjectWithTag("Fogueira").GetComponent<Animator>();
        girlAnimator = GameObject.FindGameObjectWithTag("Girl").GetComponent<Animator>();
    }

    private void onfireClick()
    {
        catAnimator.SetTrigger("action");
    }

    private void OnEnergyStateChange(int _state)
    {

        if(fogueiraAnimator == null)
        {
            fogueiraAnimator = GameObject.FindGameObjectWithTag("Fogueira").GetComponent<Animator>();
        }

        if(catAnimator == null)
        {
            catAnimator = GameObject.FindGameObjectWithTag("Cat").GetComponent<Animator>();
        }

        if( girlAnimator == null)
        {
            girlAnimator = GameObject.FindGameObjectWithTag("Girl").GetComponent<Animator>();
        }

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
                //Debug.Log("vamos ver");
                fogueiraAnimator.SetBool("fogo", true);
                girlAnimator.SetBool("burnning", true);
                catAnimator.SetTrigger("burn");
                break;
        }
    }

    private void OnWrongClick()
    {
        Debug.Log("wrongClick");
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
        Debug.Log("missClick");
        girlAnimator.SetTrigger("error");
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
        //Debug.Log("Correto");
        
        score++;

        girlAnimator.SetTrigger("correct");

        audioManager.PlayCarpetSound(carpetSound[Random.Range(0,carpetSound.Length)]);
    }

    private void Perdeu()
    {
        PlayerPrefs.SetInt("StartMenuAt", 2);
        BeatManager.OnCorrectClick -= OnCorrectClick;
        BeatManager.OnWrongClick -= OnWrongClick;
        BeatManager.OnMissClick -= OnMissClick;
        BeatManager.OnLevelCompleted -= OnLevelCompleted;
        BeatManager.OnGroupCompleted -= OnGroupCompleted;
        EnergyIcon.OnEnergyStateChange -= OnEnergyStateChange;
        EnergyIcon.OnFireClick -= onfireClick;
        SceneManager.LoadScene(0);
    }

    private void Ganhou()
    {
        BeatManager.OnCorrectClick -= OnCorrectClick;
        BeatManager.OnWrongClick -= OnWrongClick;
        BeatManager.OnMissClick -= OnMissClick;
        BeatManager.OnLevelCompleted -= OnLevelCompleted;
        BeatManager.OnGroupCompleted -= OnGroupCompleted;
        EnergyIcon.OnEnergyStateChange -= OnEnergyStateChange;
        EnergyIcon.OnFireClick -= onfireClick;

        beatManager.running = false;
        winGame = true;
        girlAnimator.SetTrigger("win");
        audioManager.PlayWinSound(winClip);
        
        SpriteRenderer smokeRender = fumaca.GetComponent<SpriteRenderer>();
        smokeRender.sprite = smokeByLevel[currentLevel];
        fumaca.SetActive(true);

        PlayerPrefs.SetInt("level" + currentLevel + "Finished",1);

        Invoke("CutsceneTransition", winClip.length);
    }

    private void CutsceneTransition() {
        SceneManager.LoadScene("cutscene_1");

    }

	
	// Update is called once per frame
	void Update () {
       /*
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
        */
    }
}
