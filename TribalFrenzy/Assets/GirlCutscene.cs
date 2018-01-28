using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlCutscene : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip carpetClip;

    public Animator catAnimator;

    public void PlayCarpetSound()
    {
        audioSource.PlayOneShot(carpetClip);
    }

    public void ScareCat() {
        catAnimator.SetTrigger("scare");
    }
}
