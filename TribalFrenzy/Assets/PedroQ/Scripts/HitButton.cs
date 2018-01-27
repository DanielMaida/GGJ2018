﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour {

    private Animator animator;

    public BeatManager beatManager;

    public bool inside = false;
    private SpriteRenderer spriteRender;
    public bool pressed = false;
    public bool notPressed = false;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (inside)
            {
                if (!pressed)
                {
                    //spriteRender.color = Color.green;
                    pressed = true;
                    beatManager.buttonCorrectClick();
                    animator.SetBool("pressed", true);
                    
                }
                else
                {
                    beatManager.buttonWrongClick();
                }
                
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Destroyer"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag.Equals("BeatHitter"))
        {
            inside = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("BeatHitter"))
        {
            inside = false;
            if (!pressed)
            {
                //spriteRender.color = Color.red;
                beatManager.buttonMissClick();
                notPressed = true;
            }
        }
    }






}