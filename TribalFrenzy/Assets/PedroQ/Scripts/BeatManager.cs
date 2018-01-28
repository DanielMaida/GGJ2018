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

    public AudioClip[] bgMusics;

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
    public BeatBlock[] beatBlockListFast = new BeatBlock[4];

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

    private float currentBeatBlockTime = 2.0f;

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

        //Cria O ritmo dos blocos
        BeatBlock bloco0 = new BeatBlock(2, new float[] { });
        BeatBlock bloco1 = new BeatBlock(2, new float[] { 0f });
        BeatBlock bloco2 = new BeatBlock(2, new float[] { 0.5f, 1.5f });
        BeatBlock bloco3 = new BeatBlock(2, new float[] { 0.66f, 1.13f, 1.60f });

        //Ritmo para blocos rapidos
        BeatBlock bloco0Fast = new BeatBlock(2, new float[] { });
        BeatBlock bloco1Fast = new BeatBlock(2, new float[] { 0f });
        BeatBlock bloco2Fast = new BeatBlock(2, new float[] { 0f, 0.66f });
        BeatBlock bloco3Fast = new BeatBlock(2, new float[] { 0f, 0.33f, 0.66f});


        //normal
        beatBlockList[0] = bloco0;
        beatBlockList[1] = bloco1;
        beatBlockList[2] = bloco2;
        beatBlockList[3] = bloco3;

        //hard
        beatBlockListFast[0] = bloco0Fast;
        beatBlockListFast[1] = bloco1Fast;
        beatBlockListFast[2] = bloco2Fast;
        beatBlockListFast[3] = bloco3Fast;


    }

    private void SpawnLevelBar(int mode,float beatBlockTime)
    {
        hitButtonsContainer = new GameObject("container");
        hitButtonsContainer.transform.parent = hitBar.transform;
        hitButtonsContainer.transform.position = hitBar.transform.position;


        int count = 0;
        
        foreach( int type in beatLevelList)
        {
            if(mode == 0)
            {
                foreach (float timer in beatBlockList[type].clickAreaList)
                {

                    GameObject go = Instantiate(hitButton) as GameObject;
                    go.transform.position = hitBar.transform.position;
                    go.transform.Translate(Vector3.up * (timer + (count * beatBlockTime)) * speedMultiply);
                    go.transform.parent = hitButtonsContainer.transform;



                    HitButton hb = go.GetComponent<HitButton>();
                    hb.beatManager = this;
                }
            }
            else
            {
                foreach (float timer in beatBlockListFast[type].clickAreaList)
                {

                    GameObject go = Instantiate(hitButton) as GameObject;
                    go.transform.position = hitBar.transform.position;
                    go.transform.Translate(Vector3.up * (timer + (count * beatBlockTime)) * speedMultiply);
                    go.transform.parent = hitButtonsContainer.transform;



                    HitButton hb = go.GetComponent<HitButton>();
                    hb.beatManager = this;
                }
            }
            

            count++;

        }

    }

    // Use this for initialization
    void Start () {
        
    }

    public void ConfigureLevel(int currentLevel,int[] levelCode)
    {

        //Configuracao do level
        beatLevelList = levelCode;

        switch (currentLevel)
        {
            case 0:
                audioM.SetBGMusic(bgMusics[0]);
                speedMultiply = 1.9f;
                SpawnLevelBar(0,2.0f);
                currentBeatBlockTime = 2.0f;
                break;
            case 1:
                audioM.SetBGMusic(bgMusics[1]);
                speedMultiply = 1.9f;
                SpawnLevelBar(0,2.0f);
                currentBeatBlockTime = 2.0f;
                break;
            case 2:
                audioM.SetBGMusic(bgMusics[2]);
                speedMultiply = 2.5f;
                SpawnLevelBar(1,1.332f);
                currentBeatBlockTime = 1.332f;
                break;
            case 3:
                audioM.SetBGMusic(bgMusics[3]);
                speedMultiply = 2.5f;
                SpawnLevelBar(1,1.332f);
                currentBeatBlockTime = 1.332f;
                break;
        }

        

        

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
        currentBlock = (int)Mathf.Ceil(musicTime / currentBeatBlockTime);

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

        float blockTime = musicTime % currentBeatBlockTime;
        
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
