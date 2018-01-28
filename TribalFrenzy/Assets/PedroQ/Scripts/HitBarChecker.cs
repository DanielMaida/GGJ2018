using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBarChecker : MonoBehaviour {

    public BeatManager beatMan;
    private bool inside = false;

    int count = 0;

    private void Awake()
    {
        beatMan = transform.parent.parent.GetComponent<BeatManager>();
    }

    private void Update()
    {
        //PC
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(count);
            if (count <= 0)
            {
                beatMan.buttonWrongClick();
            }
        }
        //MOBILE
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x > Screen.width / 2)
            {
                if (count <= 0)
                {
                    beatMan.buttonWrongClick();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ButtonTrigger"))
        {
            inside = true;
            count++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ButtonTrigger"))
        {
            inside = false;
            count--;
            if(count < 0)
            {
                count = 0;
            }
        }
    }
}
