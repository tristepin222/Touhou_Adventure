using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataStatic : MonoBehaviour
{
    public static dataStatic Instance;
    public LifeManagament life;
    public Vector2 position;
    public Inventory inventory;
    public string scene;
    public string[] Names;
    public int[] Indexes;
    public string characterName;
    public bool once;
    public List<Color> colors;
    public float money;
    public bool client = false;
    public int[] objsaved;
    public bool tutcomp = false;
    public int objindex5;
    public string currentobj;
    public int lvl;
    public float xp;
    public float nextXp;
    public DialogueInformation[] DialogueInformations;
    public bool[] encounters;
    public int currentday;
    public float savedDay;
    private void Awake()
    {
        DialogueInformations = new DialogueInformation[100];
        encounters = new bool[100];
        nextXp = 100;
        money = 0;
        if (Instance == null)
        {

            DontDestroyOnLoad(gameObject);
            
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
         
        }
    }
    private void Start()
    {

        once = false;
        if (colors != null)
        {
            colors = new List<Color>();
            Names = new string[8];
            Indexes = new int[8];
        }
    }
}
