using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class grow : MonoBehaviour
{
    private Sprite s;
    private SpriteRenderer sr;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] public int growTime;
    private int a = 0;
    private int i = 0;
    lootSystem loots;



    void Start()
    {
        loots = this.gameObject.GetComponent<lootSystem>();
        loots.canHarvest = false;
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
    }

  
    private void FixedUpdate()
    {
        a++;
        if(a >= growTime)
        {
            if (i < sprites.Count)
            {
                sr.sprite = sprites[i];
                i++;

                a = 0;

            }
            if(i == sprites.Count)
            {
                loots.canHarvest = true;
               
            }
        }
    }
}
