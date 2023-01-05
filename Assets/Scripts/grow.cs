using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class grow : MonoBehaviour
{
    public int x;
    public int y;

    private Sprite s;
    private SpriteRenderer sr;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] public int growTime;
    private int time = 0;
    private int i = 0;
    private string name;
    lootSystem loots;


   

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        loots = this.gameObject.GetComponent<lootSystem>();
        loots.canHarvest = false;
        sr = this.GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
        
        sr.sortingOrder += (int)(sr.transform.position.y * -125);
        time = dataStatic.Instance.plantsInt[x, y];
        i = dataStatic.Instance.plantsStages[x, y];
        growPlant();
    }

  
    private void FixedUpdate()
    {
        time++;
        growPlant();
    }
    private void growPlant()
    {
        if (time >= growTime)
        {
            if (i < sprites.Count)
            {
                sr.sprite = sprites[i];
                i++;

                time = 0;

            }
            if (i == sprites.Count)
            {
                loots.canHarvest = true;

            }
        }
    }
    public int Time
    {
        get { return time; }
        set { time = value; }
    }
    public int Stage
    {
        get { return i; }
        set { i = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private void OnDestroy()
    {
       dataStatic.Instance.plantsInt[x, y] = 0;
       dataStatic.Instance.plantsStages[x, y] = 0;
    }
}
