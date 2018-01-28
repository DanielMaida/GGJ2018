using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour {

    public delegate void ActionDelegate();

    public static event ActionDelegate OnLevelCompleted;
    public static event ActionDelegate OnGroupCompleted;

    public static event ActionDelegate OnCorrectClick;
    public static event ActionDelegate OnWrongClick;
    public static event ActionDelegate OnMissClick;

    public int numGroup = 2;

    public float speedMultiply = 1.0f;

    public GameObject hitBar;
    public GameObject hitButton;

    private GameObject hitButtonsContainer;

    public AudioClip bgMusic;

    private AudioManager audioM;

    public bool isInEnergyZone;
    public GameObject Smoke1;

    //Configuracao de tempo inicial
    public float startTime = 0.0f;
    public float currentTime = 0.0f;

    public float deltaTime = 2f;

    public bool running = false;

    //Configuracao pra level
    public BeatBlock[] beatBlockList = new BeatBlock[4];

    private int[] beatLevelList;

    private int currentBlock = 1;
    private int lastCurrentBlock = 1;

    private int completedBlocks = 0;
    public bool isInRange = false;
    public bool wasInRange = false;
    public bool missed = false;
    public bool correctClick = false;

    private float correctClickTime = 0.0f;
    private float correctClickReload = 0.2f;
    public bool levelConfigured = false;

    public void buttonCorrectClick()
    {
        if(OnCorrectClick != null)
        {
            Instantiate(Smoke1, new Vector3(-1.05f, -1.44f, 1f), Quaternion.identity);
            OnCorrectClick();
        }
    }

    public void buttonWrongClick()
    {
        if(OnWrongClick != null)
        {
            OnWrongClick();
        }
    }

    public void buttonMissClick()
    {
        if(OnMissClick != null)
        {
            OnMissClick();
        }
    }

    private void Awake()
    {

        audioM = GetComponent<AudioManager>();
        if(audioM == null)
        {
            audioM = this.gameObject.AddComponent<AudioManager>();
        }

        audioM.SetBGMusic(bgMusic);


        //Cria O ritmo dos blocos
        BeatBlock bloco0 = new BeatBlock(2, new float[] { });
        BeatBlock bloco1 = new BeatBlock(2, new float[] { 0f });
        BeatBlock bloco2 = new BeatBlock(2, new float[] { 0.5f, 1.5f });
        BeatBlock bloco3 = new BeatBlock(2, new float[] { 0.66f, 1.13f, 1.60f });

        beatBlockList[0] = bloco0;
        beatBlockList[1] = bloco1;
        beatBlockList[2] = bloco2;
        beatBlockList[3] = bloco3;


        
    }

    private void SpawnLevelBar()
    {
        hitButtonsContainer = new GameObject("container");
        hitButtonsContainer.transform.parent = hitBar.transform;
        hitButtonsContainer.transform.position = hitBar.transform.position;

        List<float> timersList = new List<float>();

        int count = 0;
        
        foreach( int type in beatLevelList)
        {
            foreach( float timer in beatBlockList[type].clickAreaList)
            {
                timersList.Add(timer + (count*2));

                GameObject go = Instantiate(hitButton) as GameObject;
                go.transform.position = hitBar.transform.position;
                go.transform.Translate(Vector3.up * (timer + (count * 2)) * speedMultiply);
                go.transform.parent = hitButtonsContainer.transform;
                /*
                if(type == 2)
                {
                    go.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                */

                HitButton hb = go.GetComponent<HitButton>();
                hb.beatManager = this;
            }

            count++;

        }

    }

    // Use this for initialization
    void Start () {
		
	}

    public void ConfigureLevel(int[] levelCode)
    {
        //Configuracao do level
        //beatLevelList = new int[] { 0, 1, 1, 1, 2, 3, 3, 3, 3, 2, 1 };
        beatLevelList = levelCode;

        SpawnLevelBar();

        levelConfigured = true;
    }

    public void StartLevel()
    {
        if (running) return;

        running = true;
        startTime = Time.time;

        audioM.PlayBGMusic(0f);
    }
	
	// Update is called once per frame
	void Update () {

        if (!running)
            return;

        if(Time.time >= correctClickTime + correctClickReload)
        {
            correctClick = false;
        }

        float musicTime = Time.time - startTime;
        if(musicTime == 0) { return; }
        currentBlock = (int)Mathf.Ceil(musicTime / 2);

        //Debug.Log("last " + lastCurrentBlock + " / current " + currentBlock);

        if(lastCurrentBlock != currentBlock)
        {
            
            completedBlocks++;
            //Colocar fumaca aqui
            //Debug.Log("Block completed" + completedBlocks % numGroup);
            if (completedBlocks % numGroup == 0) {
                if(OnGroupCompleted != null)
                {
                    OnGroupCompleted();
                }
            }
            
            lastCurrentBlock = currentBlock;
        }

        float blockTime = musicTime % 2;
        
        if (blockTime == 0) { return; }

        //Verificar se chegou no final do level
        if (currentBlock > beatLevelList.Length)
        {

            if(OnLevelCompleted != null)
            {
                OnLevelCompleted();
            }
            
            return;
        }
        
        MoveHitButtons();

    }

    private void MoveHitButtons()
    {
        hitButtonsContainer.transform.Translate(-Vector3.up * Time.deltaTime * speedMultiply);
    }

}
