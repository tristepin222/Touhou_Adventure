using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class plant : MonoBehaviour
{
    public bool disabled = false;
    public Sprite newSprite1;
    public Sprite newSprite;
    public List<Sprite> sprites = new List<Sprite>();
    private Sprite oldSprite;
    private bool planted = false;
    private SpriteRenderer spriter;
    private GameObject Interact;
    private bool entered = false;
    private KeyCode e = KeyCode.E;

    [SerializeField] Transform plantC;
    [SerializeField] Collider2D c;
    [SerializeField] PlayerManagament player;
    [SerializeField] Item neededItem;
    [SerializeField] public GridFarming grid;
    private bool canPlant = false;
    public string sname;
    public int x;
    public int y;
    int intItem;
   public int state;
    public bool canPlow;
    public bool plowed = false;
    GameObject g;
    Item item;
    Inventory inv;
    Vector3 pos;
    bool once = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
        grid = GameObject.Find("gridfarm").GetComponent<GridFarming>();
        neededItem = new Item { item = Item.ItemType.Hoe };
        Interact = GameObject.FindGameObjectWithTag("Interact");
        spriter = this.GetComponent<SpriteRenderer>();
        spriter.sprite = sprites[0];
        if (Interact == null)
        {
            Interact = GlobalControl.Instance.ui_interact;
        }
        planted = false;

    }
  

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            if (player == null)
            {
                if (GlobalControl.Instance != null)
                {

                    player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();
                }

            }
            inv = player.GetInventory();
            if (player.inventory.getItemList().Length != 0)
            {
                item = player.inventory.getItem(player.selectedItem);
            }
            if (item != null)
            {

                if (item.itemScriptableObject.category == 1)
                {
                    if (item.GetAmount() != 0)
                    {
                        plantC = item.t;
                        canPlant = true;
                        if (Input.GetMouseButtonDown(1))
                        {
                            
                           
                                Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                if (grid.GetValue(v3) == 1)
                                {

                                    grid.SetValue(v3, 2);
                                   
                                }
                           
                        }
                    }
                }
            }
            if (plowed)
            {
                if (x - 1 < 0 || y - 1 < 0 || x > grid.gameObjects.GetLength(0) - 2 || y > grid.gameObjects.GetLength(1) - 2)
                {
                    grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
                else
                {
                    bool plowed1 = grid.gameObjects[x + 1, y].GetComponent<plant>().plowed;
                    bool plowed2 = grid.gameObjects[x, y + 1].GetComponent<plant>().plowed;
                    bool plowed3 = grid.gameObjects[x - 1, y].GetComponent<plant>().plowed;
                    bool plowed4 = grid.gameObjects[x, y - 1].GetComponent<plant>().plowed;

                    if (plowed1)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[4];

                    }
                    if (plowed2)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[5];

                    }
                    if (plowed1 && plowed2)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[8];

                    }
                    if (plowed3)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[2];

                    }
                    if (plowed4)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[3];

                    }
                    if (plowed3 && plowed4)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[6];

                    }
                    if (plowed2 && plowed3)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[9];

                    }
                    if (plowed1 && plowed4)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[7];

                    }

                    if (!plowed3 && !plowed4 && !plowed2 && !plowed1)
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[0];

                    }
                   
                    if ((plowed3 && plowed4 && plowed2 && plowed1) || (plowed3 && plowed4 && plowed2) || (plowed1 && plowed4 && plowed2) || (plowed3 && plowed1 && plowed2) || (plowed3 && plowed1 && plowed4))
                    {
                        grid.gameObjects[x, y].GetComponent<SpriteRenderer>().sprite = sprites[10];
                    }
                }

            }
            if (g == null && planted == true)
            {
                planted = false;
                plowed = true;
                state = 1;
            }
           
        }
    }
   
    public void plantSeed(int x, int y)
    {
        string name = "";
        if (item.name.Contains("Seeds")){
            int i = item.name.Length - 1;
            name = item.name.Substring(0, item.name.Length - 5);
        }
        else
        {
            name = item.name;
        }
        g = Instantiate(Resources.Load("prefab/crops/plant/" + name), grid.gameObjects[x,y].transform) as GameObject;
        g.GetComponent<lootSystem>().isCrop = true;
        g.GetComponent<lootSystem>().plant = this;
        Item item2 = new Item { itemScriptableObject = item.itemScriptableObject, item = item.item, name = item.name, amount = 1, maxAmount = item.maxAmount, maxedOut = false }; ;
        inv.RemoveInventory(item2);
        planted = true;
        once = false;
       
    }
    public void plow(int x, int y, bool checkPlow = false)
    {
        if (!checkPlow)
        {
            plowed = true;
        }

       
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
