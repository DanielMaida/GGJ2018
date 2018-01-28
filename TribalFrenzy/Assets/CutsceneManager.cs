using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour {

    public Animator girlAnimator;
    public Animator catAnimator;

    float startTime;
    bool played_girl;
    bool played_cat;

    public void Start()
    {
        startTime = Time.fixedTime;
    }
    
    public void Update()
    {
        if ((Time.fixedTime - startTime > 2.5) && !played_cat)
        {
            played_cat = true;
            catAnimator.SetTrigger("enter");
        }
        if ((Time.fixedTime - startTime > 5) && !played_girl) {
            played_girl = true;
            girlAnimator.SetTrigger("action");
        }
    }
    
}
