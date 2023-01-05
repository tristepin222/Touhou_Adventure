using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using Unity.Netcode;
public class GlobalControl : MonoBehaviour
{
   
    public static GlobalControl Instance;
    public LifeManagament life;
    public Transform v;
    public Inventory inventory;
    public GameObject ui_life;
    public GameObject ui_inventory;
    public GameObject ui_interact;
    public GameObject vc;
    public GameObject player;
    public GameObject ui_hotbar;
    public GameObject ui_craftingsystem;
    public GameObject ui_money;
    public GameObject ui_map;
    public GameObject UI_Buttons;
    public GameObject UI_Objective;
    public GameObject UI_Arrow;
    public GameObject UI_Tutorial;
    public GameObject UI_fishing;
    public GameObject UI_Exp;
    public GameObject UI_BombCreator;
    public GameObject UI_Time;
    public bool[] spawns;
    public string lastScene;
    public bool[] once;
    public Vector3 v3;
    public float time;
    public float money;
    public bool dontdestroy = false;
    public ulong playerID;
    Object b;
    public List<int> obtosv = new List<int>();
    public int objindex5;
    public string currentobj;
    public Text moneyt;
    public int lvl;
    public float xp;
    public float nextXp;
    public Vector3 previousPos;
    public DialogueInformation[] DialogueInformations;
    public List<GameObject> toSave = new List<GameObject>();
    public bool HasFarm = false;
    public bool[] encounters;
    public int hour;
    public int minute;
    public int currentDay;
    public bool[] museumItem;
    public bool isloading;
    public bool canTP;
    public bool canTrigger;
    public int steps;
    public int fights;
    public bool cinematic;
    public void Set()
    {
        
        playerID = NetworkManager.Singleton.LocalClientId;
        player = NetworkManager.Singleton.ConnectedClients[playerID].PlayerObject.gameObject;
        time = 0.4f;




        if (Instance == null)
                {
                    inventory = dataStatic.Instance.inventory;
                }
                
           
            objindex5 = 0;
            vc = Instantiate(Resources.Load("Prefabs/CM vcam1")) as GameObject;
            vc.name = "CM vcam1";
            ui_interact = Instantiate(Resources.Load("Prefabs/Interact")) as GameObject;
            ui_interact.name = "Interact";
            ui_inventory = Instantiate(Resources.Load("Prefabs/UI_Inventory")) as GameObject;
            ui_inventory.name = "UI_Inventory";
            ui_hotbar = Instantiate(Resources.Load("Prefabs/UI_hotBar")) as GameObject;
            ui_hotbar.name = "UI_hotBar";
            ui_craftingsystem = Instantiate(Resources.Load("Prefabs/UI_craftingSystem")) as GameObject;
            ui_craftingsystem.name = "UI_craftingSystem";
            ui_life = Instantiate(Resources.Load("Prefabs/UI_Life")) as GameObject;
            ui_life.name = "UI_Life";
            ui_money = Instantiate(Resources.Load("Prefabs/UI_Money")) as GameObject;
            ui_money.name = "UI_Money";
            ui_map = Instantiate(Resources.Load("Prefabs/UI_Map")) as GameObject;
            ui_map.name = "UI_Map";
            UI_Buttons = Instantiate(Resources.Load("Prefabs/UI_Buttons")) as GameObject;
            UI_Buttons.name = "UI_Buttons";
            UI_Arrow = Instantiate(Resources.Load("Prefabs/UI_Arrow")) as GameObject;
            UI_Arrow.name = "UI_Arrow";
            UI_Objective = Instantiate(Resources.Load("Prefabs/UI_Objective")) as GameObject;
            UI_Objective.name = "UI_Objective";
            UI_Tutorial = Instantiate(Resources.Load("Prefabs/UI_Tutorial")) as GameObject;
            UI_Tutorial.name = "UI_Tutorial";
            UI_fishing = Instantiate(Resources.Load("Prefabs/UI_fishing")) as GameObject;
            UI_fishing.name = "UI_fishing";
            UI_Exp = Instantiate(Resources.Load("Prefabs/UI_EXP")) as GameObject;
            UI_Exp.name = "UI_Exp";
            UI_BombCreator = Instantiate(Resources.Load("Prefabs/UI_BombCreator")) as GameObject;
            UI_BombCreator.name = "UI_BombCreator";
            UI_Time = Instantiate(Resources.Load("Prefabs/UI_Time")) as GameObject;
            UI_Time.name = "UI_Time";
            bool client = true;

            dontdestroy = true;
            once = new bool[100];
            encounters = new bool[100];
            museumItem = new bool[100];
            DialogueInformations = new DialogueInformation[100];
       
            DialogueInformations = dataStatic.Instance.DialogueInformations;
       
            time = 0;
            spawns = new bool[20];
            money = 0;
            Canvas c;


         
            for(int i = 0; dataStatic.Instance.objsaved.GetLength(0) > i; i++)
                {

                obtosv.Add(dataStatic.Instance.objsaved[i]);
            }
         
                UI_Tutorial.GetComponent<Canvas>().enabled = false;
          

           
            UI_Arrow.GetComponent<Canvas>().worldCamera = vc.GetComponent<Camera>();
            UI_Arrow.GetComponent<Canvas>().sortingLayerName = "UI";
            c = ui_interact.GetComponent<Canvas>();
            UI_fishing.GetComponent<Canvas>().enabled = false;
            c.worldCamera = vc.GetComponent<Camera>();
            c.sortingLayerName = "UI";
            c.enabled = false;
            ui_map.GetComponent<Canvas>().enabled = false ;
            UI_BombCreator.GetComponent<Canvas>().enabled = false;
            c = ui_inventory.GetComponent<Canvas>();
            c.worldCamera = vc.GetComponent<Camera>();
            c.sortingLayerName = "UI";

            c = ui_life.GetComponent<Canvas>();
            c.worldCamera = vc.GetComponent<Camera>();
            c.sortingLayerName = "UI";

            CinemachineVirtualCamera cvc = vc.GetComponent<CinemachineVirtualCamera>();
            cvc.Follow = player.transform;
             moneyt = ui_money.transform.GetComponentInChildren<Text>();



      

           

            if (life == null)
            {
                PlayerManagament pm = player.GetComponent<PlayerManagament>();
                life = pm.life;
            }

            if (Instance == null)
            {
                DontDestroyOnLoad(vc);
                DontDestroyOnLoad(gameObject);
                DontDestroyOnLoad(ui_life);
                DontDestroyOnLoad(ui_inventory);
                DontDestroyOnLoad(ui_interact);
                DontDestroyOnLoad(ui_hotbar);
                DontDestroyOnLoad(ui_craftingsystem);
                DontDestroyOnLoad(ui_money);
                DontDestroyOnLoad(ui_map);
                DontDestroyOnLoad(UI_Buttons);
                DontDestroyOnLoad(UI_Objective);
                DontDestroyOnLoad(UI_Arrow);
                DontDestroyOnLoad(UI_Tutorial);
                DontDestroyOnLoad(UI_fishing);
                DontDestroyOnLoad(UI_Exp);
                DontDestroyOnLoad(UI_BombCreator);
                DontDestroyOnLoad(UI_Time);
                Instance = this;
                GameObject.Find("Bows").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[0];
                GameObject.Find("Hair").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[1];
                GameObject.Find("Hair").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/" + dataStatic.Instance.Names[1]);
                GameObject.Find("Eyes").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[3];
                GameObject.Find("EyeBrows").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/" + dataStatic.Instance.Names[4]);
                GameObject.Find("Shirt").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[2];
                GameObject.Find("Pants").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[6];
                GameObject.Find("Mouth").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[5];
                GameObject.Find("Mouth").GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load("Textures/" + dataStatic.Instance.Names[5]);
                GameObject.Find("Shoes").GetComponent<SpriteRenderer>().color = dataStatic.Instance.colors[7];

                Instance.player.GetComponent<culler>().culled = false;
                Instance.player.GetComponent<culler>().cull();
            }
            else if (Instance != this)
            { 
                Destroy(vc);
                Destroy(ui_life);
                Destroy(ui_inventory);
                Destroy(ui_interact);
                Destroy(ui_hotbar);
                Destroy(ui_craftingsystem);
                Destroy(ui_money);
                Destroy(ui_map);
                Destroy(UI_Buttons);
                Destroy(UI_Objective);
                Destroy(UI_Arrow);
                Destroy(UI_Tutorial);
                Destroy(UI_fishing);
                Destroy(UI_Exp);
                Destroy(UI_BombCreator);
                Destroy(UI_Time);
            }

        if (dataStatic.Instance.inventory == null && GlobalControl.Instance.inventory == null)
        {
            inventory = new Inventory(54);
            GlobalControl.Instance.inventory = inventory;
        }
        else
        {
            money = dataStatic.Instance.money;
            inventory = dataStatic.Instance.inventory;
            life = dataStatic.Instance.life;
            player.transform.position = dataStatic.Instance.position;
            moneyt.text = dataStatic.Instance.money.ToString();
            objindex5 = dataStatic.Instance.objindex5;
            currentobj = dataStatic.Instance.currentobj;
            if (dataStatic.Instance.museumItems != null)
            {
                museumItem = dataStatic.Instance.museumItems;
            }
            if (dataStatic.Instance.encounters != null)
            {
                encounters = dataStatic.Instance.encounters;
            }
        }
        FindObjectOfType<SuccessManager>().init();
        FindObjectOfType<onAwake>().initAwake();
        NPCMovement[] nPCMovements = FindObjectsOfType<NPCMovement>();
        UI_inventoryItemSlot[] uI_InventoryItemSlots = FindObjectsOfType<UI_inventoryItemSlot>();
        foreach (NPCMovement nPCMovement in nPCMovements)
        {
            nPCMovement.init();
        }
        foreach (UI_inventoryItemSlot uI_InventoryItemSlot in uI_InventoryItemSlots)
        {
            uI_InventoryItemSlot.init();
        }
        if (cinematic)
        {
            HideUI();
        }
    }

    public void HideUI()
    {
        GlobalControl.Instance.ui_hotbar.GetComponentInChildren<Canvas>().enabled = false;
        GlobalControl.Instance.ui_life.GetComponent<Canvas>().enabled = false;
        GlobalControl.Instance.ui_money.GetComponentInChildren<Canvas>().enabled = false;
        GlobalControl.Instance.UI_Buttons.GetComponent<Canvas>().enabled = false;
        GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = false;
        GlobalControl.Instance.UI_Exp.GetComponent<Canvas>().enabled = false;
        GlobalControl.Instance.UI_Time.GetComponent<Canvas>().enabled = false;
    }
    }


  
    

