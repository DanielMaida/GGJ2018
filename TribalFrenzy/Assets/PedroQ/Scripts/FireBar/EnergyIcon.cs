using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyIcon : MonoBehaviour {

    public delegate void ActionDelegate(bool state);
    public static event ActionDelegate OnEnergyStateChange;

    LevelManager lvlManager;

    public float force;
    public float negativeForce;

    public bool inside = false;
    private Rigidbody2D rg;

    // Use this for initialization
    void Awake()
    {
        lvlManager = transform.parent.GetComponent<LevelManager>();
        rg = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("EnergyArea"))
        {
            inside = true;
            if(OnEnergyStateChange != null)
            {
                OnEnergyStateChange(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("EnergyArea"))
        {
            inside = false;
            if (OnEnergyStateChange != null)
            {
                OnEnergyStateChange(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rg.velocity = Vector2.zero;
            rg.velocity = new Vector2(0, force);
            //transform.Translate(Vector3.up * force * Time.deltaTime);
        }

        //transform.Translate(-Vector3.up * negativeForce * Time.deltaTime);


    }
}
