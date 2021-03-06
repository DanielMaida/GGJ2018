﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour {

    private Animator animator;

    public BeatManager beatManager;
    public EnergyIcon energyIcon;
    private LevelManager levelManager;

    public bool inside = false;
    private SpriteRenderer spriteRender;
    public bool pressed = false;
    public bool notPressed = false;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        levelManager = transform.root.GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        //PC
        if ((Input.GetKeyDown(KeyCode.RightArrow) ||Input.GetKeyDown(KeyCode.D)) && !levelManager.winGame)
        {
            if (inside && !levelManager.winGame)
            {
                if (!pressed)
                {
                    //spriteRender.color = Color.green;
                    //Debug.Log("OI");
                    if (levelManager.fireZone == 2)
                    {
                        pressed = true;
                        beatManager.buttonCorrectClick();
                        animator.SetBool("pressed", true);
                    }
                    else
                        return;
                    
                }
                else
                {
                    //eatManager.buttonWrongClick();
                }
                
            }
        }

        //MOBILE
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x > Screen.width / 2)
            {
                if (inside && !levelManager.winGame)
                {
                    if (!pressed)
                    {
                        //spriteRender.color = Color.green;
                        //Debug.Log("OI");
                        if (levelManager.fireZone == 2)
                        {
                            pressed = true;
                            beatManager.buttonCorrectClick();
                            animator.SetBool("pressed", true);
                        }
                        else
                            return;

                    }
                    else
                    {
                        //eatManager.buttonWrongClick();
                    }

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
