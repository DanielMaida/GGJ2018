using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public AudioSource audioS;
    public AudioClip BackgroundMusic;

    void Awake()
    {
        audioS = GetComponent<AudioSource>();
    }

    public void SetBGMusic(AudioClip _clip)
    {
        BackgroundMusic = _clip;
        audioS.clip = BackgroundMusic;
    }

    public void PlayBGMusic(float delay)
    {
        audioS.PlayDelayed(delay);
        Debug.Log("oi");
    }

}
