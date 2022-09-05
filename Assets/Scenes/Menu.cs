using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.IO;

public class Menu : MonoBehaviour
{
    
    GameObject canva;
    CinemachineVirtualCamera cm;
    GameObject player;
    public int music;
    public bool once = false;
    public int sound;
    public GameObject option;
    public GameObject option2;
    public Text option2Text;
    public Text saveText;
    public Image option2Image;
    public Slider musicG;
    public Slider soundG;
    public float mAmount;
    public float sAmount;
    private bool isActive = false;
    public Canvas canva2;
    public chooser ch;
    public chooserStyle ch2;
    public loadingScreen loadingScreen;
    saver saver;
    private void Awake()
    {

        canva2.enabled = false;
        musicG.value = 0.25f;
        soundG.value = 0.5f;
       


        canva = GameObject.FindGameObjectWithTag("MainMenu");
        option.SetActive(false);
        option2.SetActive(false);
       
    }
    public void Start()
    {
        saver = saverSystem.loadSave();
        if (File.Exists(Application.persistentDataPath + "/save.th"))
        {
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "screenshot.png");
            Texture2D texture = new Texture2D((int)256, (int)256, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);

            option2Text.text = saver.characterName;
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100.0f);
            option2Image.color = new Color(255, 255, 255, 255);
            option2Image.sprite = sp;
        }
        else
        {
            option2Text.text = "Empty";
            option2Image.color = new Color(0, 0, 0, 0);
        }
        saver = null;
        musicG.value = options.Instance.mAmount;
        soundG.value = options.Instance.sAmount;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Play()
    {



        if (dataStatic.Instance.once)
        {

            if (saver != null)
            {
                loadingScreen.sceneString = saver.scene;

            }
            else if (GlobalControl.Instance == null)
            {
                loadingScreen.sceneString = "PlayerHouseWorld";

            }
            else
            {
                loadingScreen.sceneString = GlobalControl.Instance.lastScene;
            }
            loadingScreen.startLoading();
        }
        else
        {
            
            canva2.enabled = true;
           
        }
        
        
       
       
    }
    public void OpenLinkYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UC6fnFM0zmnemiJCL4WyGTPA");



    }
    public void Options()
    {
         
        if (isActive)
        {
            option.SetActive(false);
            isActive = false;
        }
        else
        {
            
            option.SetActive(true);
            isActive = true;
        }


    }
    public void OptionsMusic()
    {
        mAmount = musicG.value;
        options.Instance.mAmount = mAmount;
    }
    public void OptionsSound()
    {
        sAmount = soundG.value;
        options.Instance.sAmount = sAmount;
    }
    public void Save()
    {
        if (GlobalControl.Instance == null)
        {
            
        }
        else
        {
            saverSystem.Save(GlobalControl.Instance, options.Instance, dataStatic.Instance);
            byte[] bytes = File.ReadAllBytes(Application.persistentDataPath + "screenshot.png");
            Texture2D texture = new Texture2D((int)256, (int)256, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);

            option2Text.text = dataStatic.Instance.characterName;
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), 100.0f);
            option2Image.color = new Color(255, 255, 255, 255);
            option2Image.sprite = sp;
            dataStatic.Instance.once = true;
        }

    }
    public void Load()
    {
        
        int i = 0;
        saver = saverSystem.loadSave();

        if (GlobalControl.Instance != null)
        {
            GlobalControl.Instance.life = new LifeManagament(saver.life);
          
            GlobalControl.Instance.inventory = new Inventory(54);
            GlobalControl.Instance.player.transform.position = new Vector2(saver.position[0], saver.position[1]);
            GlobalControl.Instance.money = saver.money;
            GlobalControl.Instance.objindex5 = saver.objindex5;
            GlobalControl.Instance.currentobj = saver.currentobj;
        }
        else
        {
           
               
            
            
            dataStatic.Instance.position = new Vector2(saver.position[0], saver.position[1]);
            dataStatic.Instance.life = new LifeManagament(saver.life);
            dataStatic.Instance.objindex5 = saver.objindex5;
            dataStatic.Instance.currentobj = saver.currentobj;
            dataStatic.Instance.inventory = new Inventory(54);
            dataStatic.Instance.money = saver.money;
            dataStatic.Instance.savedDay = saver.time;
            dataStatic.Instance.currentday = saver.day;
           
        }
           
            dataStatic.Instance.scene = saver.scene;
            dataStatic.Instance.characterName = saver.characterName;
      
            dataStatic.Instance.objsaved = saver.objectiveCompleted;
            dataStatic.Instance.tutcomp = saver.tutcomp;
            dataStatic.Instance.lvl = saver.lvl;
            dataStatic.Instance.xp = saver.xp;
            dataStatic.Instance.nextXp = saver.nextXp;
        foreach (int id in saver.IDs)
        {

            dataStatic.Instance.DialogueInformations[id] = new DialogueInformation { ID = id, index = saver.indexes1[id], index2 = saver.indexes2[id], index3 = saver.indexes3[id] };
          

        }
        dataStatic.Instance.encounters = saver.encounters;
        for (int a = 0; a <= 7; a++)
        {
            Color color = new Color(saver.colors[a, 0], saver.colors[a, 1], saver.colors[a, 2]);
            dataStatic.Instance.colors.Add(color);
        }
        
        dataStatic.Instance.Names = saver.clothes;
        dataStatic.Instance.Indexes = saver.clothesi;
        foreach (int number in saver.itemNumber)
        {

            if (number != 0)
            {
                Item.ItemType parsed_enum = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), saver.itemTypeName[i]);
                ItemScriptableObject itemsr = Resources.Load<ItemScriptableObject>("ScriptableObjects/items/" + saver.itemName[i]);
                if (GlobalControl.Instance != null)
                {
                   
                   GlobalControl.Instance.inventory.AddInventory(new Item { category = itemsr.category, itemScriptableObject = itemsr, sprite = saver.spriteName[i], itemNumber = number, amount = saver.Amount[i], item = parsed_enum, weight = saver.weight[i], maxAmount = saver.maxAmount[i], name = saver.itemName[i], });
                   
                }
                else
                {
                    dataStatic.Instance.inventory.AddInventory(new Item { category = itemsr.category, itemScriptableObject = itemsr, sprite = saver.spriteName[i], itemNumber = number, amount = saver.Amount[i], item = parsed_enum, weight = saver.weight[i], maxAmount = saver.maxAmount[i], name = saver.itemName[i], });
                }
                i++;
            }
        }
        if (GlobalControl.Instance != null)
        {
            GlobalControl.Instance.player.GetComponent<PlayerManagament>().setInventory();
        }



        dataStatic.Instance.once = true;

        Play();
    }
    public void Options2()
    {

       
        if (isActive)
        {
            option2.SetActive(false);
            isActive = false;
        }
        else
        {
            
            option2.SetActive(true);
            isActive = true;
        }


    }
    public void Return()
    {


        ch.index = 0;
        ch2.index = 0;
        ch2.index2 = 0;
        canva2.enabled = false;

    }
    public void Multiplayer()
    {
        dataStatic.Instance.client = true;
        Load();
    }
}
