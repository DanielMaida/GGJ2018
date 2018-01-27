using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour {

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        StartCoroutine("Blink");
	}

    IEnumerator Blink() {
        while (true)
        {
            text.text = "";
            yield return new WaitForSeconds(.5f);
            text.text = ". . .";
            yield return new WaitForSeconds(.5f);
        }
    }
	
}
