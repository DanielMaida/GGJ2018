using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBarChecker : MonoBehaviour {

    public BeatManager beatMan;
    private bool inside = false;

    private void Awake()
    {
        beatMan = transform.parent.parent.GetComponent<BeatManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!inside)
            {
                beatMan.buttonWrongClick();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ButtonTrigger"))
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ButtonTrigger"))
        {
            inside = false;
        }
    }
}
