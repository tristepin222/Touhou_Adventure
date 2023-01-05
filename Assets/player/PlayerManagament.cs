using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;
using Unity.Netcode;
public class PlayerManagament : MonoBehaviour   
{
    [SerializeField] public GameObject player;
    [SerializeField] public UI_Inventory uiInventory;
    [SerializeField] public GameObject uiInventoryg;
    [SerializeField] public UI_Life uiLife;
    [SerializeField] public CommandManager CommandManager;
    [SerializeField] private int amount;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject bomb;
    [SerializeField] private int cooldownMax;
    [SerializeField] private string typeName;
    [SerializeField] private  Collider2D collider2DC;
    [SerializeField] private TypeScriptable tType;
    [SerializeField] private GameObject projectilePreFab;
    [SerializeField] private int bulletAmount;
    [SerializeField] private CinemachineVirtualCamera vc;
    public event EventHandler OnDeath;
    public Inventory inventory;
    public LifeManagament life;
    public HotKeySystem hotKeySystem;
    public GameObject ui_craftingsystemg;
    public bool disabled = false;
    public Text moneyt;
    public GameObject UI_Money;
    public int selectedItem = 1;
    public GameObject ondeath;
    public AudioClip pickups;
    public AudioClip bombs;
    public AudioSource AS1;
    public bool inFightScene = false;
    public ScoreScreen ScoreScreen;
    private Type type;
    private ProjectileManagament projectileManagament;
    private Vector2 selfTransform;
    private int cooldown = 0;
    private Vector3 target;
    private KeyCode esc = KeyCode.Escape;
    private KeyCode xKey = KeyCode.X;
    private int index = 0;
    private int projectileAmount = 0;
    private PlayerMovement pm;
    private interact interact;
    private bool command;
    GameObject command2;
    private void Awake()
    {
       
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
    public Collider2D GetCollider2D()
    {
        return collider2DC;
    }
    void Start()
    {

         command2 = CommandManager.gameObject;

        gameObject.GetComponent<GlobalControl>().Set();

            inventory = GlobalControl.Instance.inventory;
           
            pm = this.GetComponent<PlayerMovement>();
            uiLife = GameObject.FindGameObjectWithTag("UI_Life").GetComponent<UI_Life>();
            uiInventory = GameObject.FindGameObjectWithTag("UI_Inventory").GetComponent<UI_Inventory>();
            uiInventoryg = GameObject.FindGameObjectWithTag("UI_Inventory");
            UI_Money = GameObject.FindGameObjectWithTag("moneyt");
            uiInventoryg.GetComponent<Canvas>().enabled = false;
            ui_craftingsystemg = GameObject.Find("UI_craftingSystem").transform.GetChild(0).gameObject;
            ui_craftingsystemg.transform.GetComponent<Canvas>().enabled = false;
            
            type = new Type { type = tType };
            

            setLife();
            inventory.isPlayer = true;

            hotKeySystem = new HotKeySystem(this, uiInventory);
            uiInventory.SetInventory(inventory, hotKeySystem);
            life.isPLayer = true;

            moneyt = UI_Money.transform.GetComponentInChildren<Text>();

            hotKeySystem.OnChange += OnItemSelectChange;

        if (FindObjectOfType<onAwake>().SetAIPlayer)
        {
            GlobalControl.Instance.ui_hotbar.GetComponentInChildren<Canvas>().enabled = false;
            GlobalControl.Instance.ui_life.GetComponent<Canvas>().enabled = false;
            GlobalControl.Instance.ui_money.GetComponentInChildren<Canvas>().enabled = false;
            GlobalControl.Instance.UI_Buttons.GetComponent<Canvas>().enabled = false;
            GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = false;
            GlobalControl.Instance.UI_Exp.GetComponent<Canvas>().enabled = false;
            GlobalControl.Instance.UI_Time.GetComponent<Canvas>().enabled = false;
        }
        GameObject gm = GameObject.Find("MainMenu");
        if (gm != null)
        {
            this.transform.position = gm.transform.position;
        }
    }
    public void setInventory()
    {
        inventory = GlobalControl.Instance.inventory;
        uiInventory.SetInventory(inventory, hotKeySystem);
        uiInventory.RefreshInventoryItems();
    }
    private void OnItemSelectChange(object sender, System.EventArgs e)
    {
        this.selectedItem = hotKeySystem.selectedItem;
        if (inventory.getItem(selectedItem) != null)
        {
            if (inventory.getItem(selectedItem).category == 6)
            {
                projectile = Resources.Load<GameObject>("prefab/projectiles/" + inventory.getItem(hotKeySystem.selectedItem).item);
                bomb = Resources.Load<GameObject>("prefab/danmaku/" + inventory.getItem(hotKeySystem.selectedItem).item);
                index = selectedItem;
                projectileAmount = inventory.getItem(selectedItem).amount;
            }
            else
            {

                projectile = null;
            }
        }else
        {
            projectile = null;
        }
    }
    void Update()
    {
        
            hotKeySystem.Update();




            if (Camera.main != null)
            {
                target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            }
            Vector3 difference = target - player.transform.position;
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            if (Input.GetKeyDown(esc))
            {
                disabled = true;
                ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "screenshot.png");
                GlobalControl.Instance.HideUI();
                SceneManager.LoadScene("MainMenu");
                
            }
        if (!command)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (uiInventoryg.GetComponent<Canvas>().enabled)
                {
                    disabled = false;
                    uiInventoryg.GetComponent<Canvas>().enabled = false;

                }
                else
                {
                    disabled = true;
                    uiInventoryg.GetComponent<Canvas>().enabled = true;

                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (uiInventoryg.GetComponent<Canvas>().enabled)
                {
                    disabled = false;
                    uiInventoryg.GetComponent<Canvas>().enabled = false;

                }
                else
                {
                    disabled = true;
                    uiInventoryg.GetComponent<Canvas>().enabled = true;

                }
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled)
                {

                    GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = false;

                }
                else
                {

                    GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = true;

                }

            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (GlobalControl.Instance.UI_Tutorial.GetComponent<Canvas>().enabled)
                {
                    disabled = false;
                    GlobalControl.Instance.UI_Tutorial.GetComponent<Canvas>().enabled = false;

                }
                else
                {
                    disabled = true;
                    GlobalControl.Instance.UI_Tutorial.GetComponent<Canvas>().enabled = true;

                }

            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (GlobalControl.Instance.ui_map.GetComponent<Canvas>().enabled)
                {
                    disabled = false;
                    GlobalControl.Instance.ui_map.GetComponent<Canvas>().enabled = false;

                }
                else
                {
                    disabled = true;
                    GlobalControl.Instance.ui_map.GetComponent<Canvas>().enabled = true;

                }

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interact != null)
                {
                    interact.m_onKeyDown.Invoke();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            
            if (command)
            {
                Destroy(command2);
                pm.restrict = false;
                command = false;
            }
            else
            {
                command2 = Instantiate(CommandManager).gameObject;
                pm.restrict = true;
                command = true;
            }
        }
        /*if (Input.GetKeyDown(KeyCode.N))
        {
            if (GlobalControl.Instance.UI_BombCreator.GetComponent<Canvas>().enabled)
            {
                disabled = false;
                GlobalControl.Instance.UI_BombCreator.GetComponent<Canvas>().enabled = false;

            }
            else
            {
                disabled = true;
                GlobalControl.Instance.UI_BombCreator.GetComponent<Canvas>().enabled = true;

            }

        }*/
        if (Input.GetMouseButton(0))
            {
                if (cooldown >= cooldownMax)
                {
                    for (int i = 0; i < bulletAmount; i++)
                    {

                        float distance = difference.magnitude;
                        Vector2 direction = difference / distance;
                        direction.Normalize();
                        if (!disabled)
                        {

                            if (projectile != null)
                            {

                                LaunchProjectile(direction, rotationZ, projectile, i);
                                if (projectile.GetComponent<projectileManager>().animated)
                                {
                                    disabled = true;
                                    GlobalControl.Instance.player.GetComponent<PlayerMovement>().enabled = false;
                                }
                                if (inventory.getItem(index) != null && inventory.getItem(index).category == 6 && !projectile.GetComponent<projectileManager>().handable)
                                {

                                    if (inventory.getItem(index).amount <= 0)
                                    {
                                        projectile = null;
                                        inventory.RemoveInventory(inventory.getItem(index));
                                    }
                                    else
                                    {
                                        inventory.Decrease(inventory.getItem(index), 1);
                                    }
                                }
                                else if (!projectile.GetComponent<projectileManager>().handable)
                                {
                                    projectile = null;
                                }
                            }
                        }
                    }
                    cooldown = 0;
                }
            }

            if (life.lifeAmount <= 0)
            {


             GameObject gameobject = GameObject.Find(GlobalControl.Instance.lastScene);
            if(gameobject == null)
            {
                loader loader = FindObjectOfType<loader>();
                if (loader != null)
                {
                    gameobject = loader.gameObject;
                }
            }
            if (gameobject == null)
            {
                gameobject = GameObject.Find("SpawPoint");
            }
            if (gameobject != null) {
                
                player.transform.position = gameobject.transform.position;

            }
            else
            {

                player.transform.position = GameObject.Find("spawnToPoint").transform.position;
            }
                setLife();
            GameObject object2 = GameObject.FindGameObjectWithTag("loadingScene");
            loadingScreen loadingScreen = object2.GetComponent<loadingScreen>();
            GameObject object3 = GameObject.FindGameObjectWithTag("ScoreScreen");
           
            
            loadingScreen.sceneString = GlobalControl.Instance.lastScene;
            if (inFightScene)
            {
                ScoreScreen = object3.GetComponent<ScoreScreen>();
                ScoreScreen.show();
            }
            else
            {
                loadingScreen.startLoading();
                OnDeath.Invoke(this, EventArgs.Empty);
            }
            }

            if (Input.GetKeyDown(xKey))
            {

                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                if (inventory.getItem(index) != null)
                {
                    projectileAmount = inventory.getItem(index).amount;
                    if (projectileAmount >= 8 && bomb != null)
                    {


                        AS1.pitch = 1f;
                        AS1.PlayOneShot(bombs, options.Instance.sAmount);
                        LaunchProjectile(direction, rotationZ, bomb, isBomb: true);
                        inventory.Decrease(inventory.getItem(index), 8);
                    }
                    if (inventory.getItem(index).amount == 0)
                    {
                        inventory.RemoveInventory(inventory.getItem(index));
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (inventory.getItem(hotKeySystem.selectedItem) != null)
                {
                    if (inventory.getItem(hotKeySystem.selectedItem).item == Item.ItemType.HealthPotion)
                    {

                        life.increaseMaxLife(1);



                        inventory.Decrease(inventory.getItem(hotKeySystem.selectedItem), 1);

                    }
                }
            }
        
    }
    private void FixedUpdate()
    {
        cooldown++;
    }

    private void LaunchProjectile(Vector2 direction, float rotationZ,  GameObject projectile, int offset = 1, bool isBomb = false)
    {


        
        GameObject b = Instantiate(projectile) as GameObject;
        float fOffset = 0.5f;
        if (isBomb)
        {
            b.transform.position = this.GetComponent<Transform>().position;
        }
        else
        {

            b.transform.position = this.GetComponent<Transform>().position - new Vector3(0, 0.5f,0);
         
                

        }
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.transform.Translate(Vector3.right *1.5f );
        if (isBomb)
        {
            for (int i = 0; i < b.transform.childCount; i++)
            {
                Rigidbody2D c = b.transform.GetChild(i).GetComponent<Rigidbody2D>();

                c.velocity = c.transform.right * b.transform.GetChild(i).GetComponent<projectileManager>().projectileScriptableObject.speed; ;


            }
        }
        else
        {
            b.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<projectileManager>().projectileScriptableObject.speed;
        }
    }
    
    public LifeManagament GetLife()
    {
        return life;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemiProjectile")
        {
            Destroy(other.gameObject);
            life.reduceLife(1);
        }
        if (other.tag == "Interact")
        {
            interact = other.GetComponent<interact>();
        }
        if (other.tag == "collectable")
        {

            AS1.pitch = UnityEngine.Random.Range(0.5f, 1.5f);
            AS1.PlayOneShot(pickups, options.Instance.sAmount);
            GameObject otherB = other.gameObject;
            lootManager loot = otherB.GetComponent<lootManager>();
            if (loot.GetItem().item == Item.ItemType.Coin)
            {
               
                GlobalControl.Instance.money += Mathf.Round(loot.GetItem().amount * 100f) / 100f;
            }
            else
            {
                inventory.AddInventory(loot.GetItem());
            }
            moneyt.text = GlobalControl.Instance.money.ToString();
            uiInventory.RefreshInventoryItems();
            Destroy(otherB);
        }
        if (other.tag == "Graze")
        {
            ScoreScreen.addScore(10);
            
        }

        }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Interact")
        {
            interact = null;
        }
    }

    public void setLife()
    {
        life = new LifeManagament(3, 3);

        uiLife.setLifeUI(life);
        GlobalControl.Instance.life = life;
       
    }

    private void kill()
    {
        GlobalControl.Instance.ui_hotbar.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        GlobalControl.Instance.ui_inventory.GetComponent<Canvas>().enabled = false;
        GlobalControl.Instance.ui_craftingsystem.gameObject.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
        life.reduceLife(10000000);
        pm.restrict = false;
       
    }

    public IEnumerator killl(AudioClip audioClip)
    {
        pm.restrict = true;
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, this.transform.position);
        }
        yield return new WaitForSeconds(1);
        kill();

    }

   
}

