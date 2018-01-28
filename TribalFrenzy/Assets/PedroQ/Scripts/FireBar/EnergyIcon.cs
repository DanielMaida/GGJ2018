using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyIcon : MonoBehaviour {

    public delegate void ActionDelegate(int state);
    public static event ActionDelegate OnEnergyStateChange;

    public delegate void newActionDelegate();
    public static event newActionDelegate OnFireClick;

    //LevelManager lvlManager;

    public float force;
    public float negativeForce;

    public bool inside = false;
    public Rigidbody2D rg;

    // Use this for initialization
    void Awake()
    {
        //lvlManager = transform.parent.GetComponent<LevelManager>();
        rg = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("NoArea"))
        {
            inside = true;
            if(OnEnergyStateChange != null)
            {
                OnEnergyStateChange(0);
            }
        }

        if (collision.gameObject.tag.Equals("LowArea"))
        {
            inside = true;
            if (OnEnergyStateChange != null)
            {
                OnEnergyStateChange(1);
            }
        }

        if (collision.gameObject.tag.Equals("EnergyArea"))
        {
            inside = true;
            if (OnEnergyStateChange != null)
            {
                OnEnergyStateChange(2);
            }
        }

        if (collision.gameObject.tag.Equals("FireArea"))
        {
            inside = true;
            if (OnEnergyStateChange != null)
            {
                OnEnergyStateChange(3);
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
        //PC
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) )
        {
            rg.velocity = Vector2.zero;
            rg.velocity = new Vector2(0, force);

            if(OnFireClick != null)
            {
                OnFireClick();
            }
        }

        //MOBILE
        foreach(Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                rg.velocity = Vector2.zero;
                rg.velocity = new Vector2(0, force);

                if (OnFireClick != null)
                {
                    OnFireClick();
                }
            }
        }

    }
}
