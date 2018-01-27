using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour {

    public Sprite vidaFull;
    public Sprite vidaEmpty;

    public SpriteRenderer[] sprites = new SpriteRenderer[6];

	public void SetVidas(int vidas)
    {
        Debug.Log(vidas);
        for(int x = 0; x < sprites.Length; x++)
        {
            if(x < vidas/2)
            {
                sprites[x].sprite = vidaFull;
            }
            else
            {
                sprites[x].sprite = vidaEmpty;
            }
        }
    }

    
}
