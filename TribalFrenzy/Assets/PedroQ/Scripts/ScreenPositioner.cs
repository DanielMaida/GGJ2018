using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPositioner : MonoBehaviour {

    public Vector3 screenRelative;

	// Use this for initialization
	void Awake () {
        //transform.position = Camera.main.ViewportToWorldPoint(new Vector3(.9f, .15f, 1));
        transform.position = Camera.main.ViewportToWorldPoint(screenRelative);
    }

}
