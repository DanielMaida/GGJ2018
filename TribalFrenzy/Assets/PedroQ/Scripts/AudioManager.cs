using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public AudioSource audioS;
    public AudioClip BackgroundMusic;

    void Awake()
    {
        audioS = null;
        audioS = this.gameObject.AddComponent<AudioSource>();
        audioS = GetComponent<AudioSource>();
    }

    public void SetBGMusic(AudioClip _clip)
    {
        BackgroundMusic = _clip;
        audioS.clip = BackgroundMusic;

        Debug.Log(audioS);
    }

    public void PlayBGMusic(float delay)
    {
        if(audioS == null)
        {
            audioS = GetComponent<AudioSource>();
        }
        audioS.PlayDelayed(delay);
        //Debug.Log("oi");
    }

    public void PlayCarpetSound(AudioClip _clip) {
        audioS.PlayOneShot(_clip, 2f);
    }

    public void PlayWinSound(AudioClip _clip)
    {
        audioS.PlayOneShot(_clip);
    }

}
