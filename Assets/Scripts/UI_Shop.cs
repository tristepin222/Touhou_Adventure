using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;
public class UI_Shop : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject[] slotsP;
    public ItemScriptableObject[] itemsToAdd;
    public Inventory inventory;
    public ShopSystem shopSystem;
    public Text amountT;
    public Text CostT;
    public int costScale = 1;
    public GameObject preview;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Transform itemSlotContainer2;
    public Item item;
    public int selectedItem = 0;
    public LocalizedString myString;
    public actions action;
    public loadingScreen loadingScreen;
    public List<Dialogue> afterPurchase = new List<Dialogue>();
    public reactionManager dialogue;
    public bool giveSuccess;
    public string success;
    RectTransform itemSlotRectTransform;
    string localizedText;
    int amount;
    float cost;
    Item preview_item;
    int shopi;
    [SerializeField] Button Buybutton;
    [SerializeField] Button Sellbutton;
    private int currentSlot;
    private int x = 0;
    private SuccessManager SuccessManager;
    // Start is called before the first frame update
    void Start()
    {
        Buybutton.gameObject.SetActive(true);
        Sellbutton.gameObject.SetActive(false);
        
        inventory = new Inventory(slots.Length);
        foreach (ItemScriptableObject items in itemsToAdd)
        {
            item = new Item { amount = items.Shopamount, item = items.itemType, itemNumber = items.itemNumber, itemScriptableObject = items, name = items.itemName, maxAmount = items.maxAmount };
            inventory.AddInventory(item);
        }
        
        shopSystem = new ShopSystem();
        int i = 0;
        foreach(GameObject slot in slotsP)
        {
            slot.GetComponent<UI_inventoryItemSlot>().itemi = i;
            i++;
        }
         i = 0;
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<UI_inventoryItemSlot>().itemi = i;
            i++;
        }
        Inventory_OnItemListChanged();
        GlobalControl.Instance.inventory.OnItemListChanged += Inventory_OnitemListChanged_player;
        SuccessManager = FindObjectOfType<SuccessManager>();
        if (SuccessManager == null)
        {
            giveSuccess = false;
        }
    }

    
    public void Inventory_OnItemListChanged()
    {
        x = 0;
        foreach (Item item in inventory.getItemList())
        {

            updateVisual(item);
        }
    }
    public void Inventory_OnitemListChanged_player(object sender, System.EventArgs e)
    {
        x = 0;
        foreach (Item item in GlobalControl.Instance.inventory.getItemList())
        {

            updateVisual(item, true);
        }
    }
    public void updateVisual(int i, bool selfInv = false)
    {
        shopi = i;
        Text textcontent = preview.transform.Find("Text").GetComponent<Text>();
        Text amountContent = preview.transform.Find("Amount").GetComponent<Text>();
        Image imageContent = preview.transform.Find("Image").GetComponent<Image>();
        if (selfInv)
        {
            item = GlobalControl.Instance.inventory.getItem(i);
            Buybutton.gameObject.SetActive(false);
            Sellbutton.gameObject.SetActive(true);
        }
        else
        {
            item = inventory.getItem(i);
            Buybutton.gameObject.SetActive(true);
            Sellbutton.gameObject.SetActive(false);
        }
        preview_item = item;
        if (item != null)
        {
            myString.TableEntryReference = item.name;
            localizedText = myString.GetLocalizedString();
            textcontent.text = localizedText;

            amountContent.text = item.amount.ToString();
            imageContent.sprite = item.itemScriptableObject.sprite;
        }
        else
        {
            myString.TableEntryReference = "none";
            localizedText = myString.GetLocalizedString();
            textcontent.text = localizedText;

            amountContent.text = "0";
            imageContent.sprite = null;
        }
    }
    public void updateVisual(Item item, bool player = false)
    {
        if (item != null)
        {
            if (item.amount <= 0)
            {
                item = null;
            }
        }


        if (player)
        {
            itemSlotRectTransform = slotsP[x].GetComponent<RectTransform>();
            itemSlotRectTransform.GetComponent<UI_inventoryItemSlot>().itemi = x;
        }
        else
        {
            itemSlotRectTransform = slots[x].GetComponent<RectTransform>();
            itemSlotRectTransform.GetComponent<UI_inventoryItemSlot>().itemi = x;
        }








        itemSlotRectTransform.gameObject.SetActive(true);
        Text textcontent = itemSlotRectTransform.Find("Text").GetComponent<Text>();
        Text amountContent = itemSlotRectTransform.Find("Amount").GetComponent<Text>();
        Image imageContent = itemSlotRectTransform.Find("Image").GetComponent<Image>();
        Text text2 = itemSlotRectTransform.GetComponent<Text>();


        if (item == null)
        {
            itemSlotRectTransform.GetComponent<UI_inventoryItemSlot>().itemi = x;
            textcontent.enabled = false;

        }
        else
        {
            currentSlot = x;
            textcontent.enabled = true;

            text2.text = item.name;
            myString.TableEntryReference = item.name;

            localizedText = myString.GetLocalizedString();

            textcontent.text = localizedText;

        }

        if (item == null)
        {
            amountContent.enabled = false;
        }
        else
        {
            amountContent.enabled = true;

            amountContent.text = item.GetAmount().ToString();
        }

        if (item == null)
        {
            imageContent.enabled = false;
        }
        else
        {



            imageContent.enabled = true;
            if (item.itemScriptableObject != null)
            {
                imageContent.sprite = item.itemScriptableObject.sprite;
            }


            if (imageContent.sprite == null)
            {
                imageContent.sprite = Resources.Load<Sprite>("Textures/" + item.sprite);
            }
            if (imageContent.sprite == null)
            {
                string[] splitted = item.sprite.Split('_');
                Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/" + splitted[0]);
                Sprite masterSprite = null;
                foreach (Sprite sprite in sprites)
                {
                    if (sprite.name == item.sprite)
                    {
                        masterSprite = sprite;
                    }
                }
                imageContent.sprite = masterSprite;
            }

        }
        x++;
    }

    public void add(int i)
    {
        int predictAmount = amount + i;
        if (predictAmount <= item.amount)
        {
            amount += i;
            amountT.text = amount.ToString();

            cost = amount * item.itemScriptableObject.price * costScale;
            cost = Mathf.Round(cost * 100f) / 100f;
            CostT.text = cost.ToString();
        }
        else
        {
            addAll();
        }
    }
    public void remove(int i)
    {

        int predictAmount = amount - i;
        if (predictAmount > 0)
        {
            amount -= i;
            amountT.text = amount.ToString();

            cost = amount * item.itemScriptableObject.price * costScale;
            cost = Mathf.Round(cost * 100f) / 100f;
            CostT.text = cost.ToString();
        }
        else
        {
            removeAll();
        }
    }
    public void addAll()
    {
        amount = item.amount;
        amountT.text = amount.ToString();
        cost = amount * item.itemScriptableObject.price * costScale;
        cost = Mathf.Round(cost * 100f) / 100f;
        CostT.text = cost.ToString();
    }
    public void removeAll()
    {

        amount = 0;
        amountT.text = amount.ToString();
        cost = amount * item.itemScriptableObject.price * costScale;
        cost = Mathf.Round(cost * 100f) / 100f;
        CostT.text = cost.ToString();
    }
    public void sell()
    {
        if (preview_item == null || preview_item.amount == 0)
        {
            preview_item = null;
        }
        else
        {
            
            GlobalControl.Instance.money += cost;
            GlobalControl.Instance.moneyt.text = GlobalControl.Instance.money.ToString();
            GlobalControl.Instance.inventory.Decrease(preview_item, amount);
   
                Inventory_OnItemListChanged();
           
           
                updateVisual(shopi);
          
        }
        
    }
    public void buy()
    {
        int tempInt;
        tempInt = preview_item.itemNumber;
        if (preview_item == null || preview_item.amount == 0)
        {
            preview_item = null;
        }
        else
        {
            if (cost > GlobalControl.Instance.money)
            {

            }
            else
            {
                
                GlobalControl.Instance.money -= cost;
                Text textm = GlobalControl.Instance.ui_money.transform.GetComponentInChildren<Text>();
                textm.text = GlobalControl.Instance.money.ToString();
                inventory.Decrease(preview_item, amount);
                Item purchasedItem = new Item { amount = this.amount, item = preview_item.item, itemNumber = preview_item.itemNumber, itemScriptableObject = preview_item.itemScriptableObject, name = preview_item.name, maxAmount = preview_item.maxAmount };
                GlobalControl.Instance.inventory.AddInventory(purchasedItem);
                Inventory_OnItemListChanged();
            }
            if (preview_item.amount <= 0)
            {

                inventory.RemoveInventory(preview_item);
                updateVisual(shopi);
            }
        }
        GlobalControl.Instance.moneyt.text = GlobalControl.Instance.money.ToString();
        if (afterPurchase != null)
        {
            GlobalControl.Instance.canTrigger = true;
            dialogue.canClick = true;
            this.gameObject.SetActive(false);
            dialogue.gameObject.SetActive(true);
            if (tempInt == 19)
            {
              dialogue.CustomDialogue(afterPurchase[0]);
            }
            
        }
    }
    private void OnEnable()
    {
        GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = true;
        Inventory_OnitemListChanged_player( this, System.EventArgs.Empty);
        GlobalControl.Instance.inventory.OnItemListChanged += Inventory_OnitemListChanged_player;
    }
    public void leave()
    {
        this.gameObject.SetActive(false);
        GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
        GlobalControl.Instance.inventory.OnItemListChanged -= Inventory_OnitemListChanged_player;
    }
    private void OnDisable()
    {
        
        GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
        GlobalControl.Instance.inventory.OnItemListChanged -= Inventory_OnitemListChanged_player;
    }

    
}
