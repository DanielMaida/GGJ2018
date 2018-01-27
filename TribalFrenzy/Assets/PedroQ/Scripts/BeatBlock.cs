using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBlock {

    public float duration = 2.0f;
    public float[] clickAreaList;

	// Use this for initialization
	public BeatBlock (float _duration,float[] _clickArealist) {
        duration = _duration;
        clickAreaList = _clickArealist;
	}
	
    
}
