using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBlip : MonoBehaviour {

    public void Blip() {
        GetComponent<AudioSource>().Play();
    }
}
