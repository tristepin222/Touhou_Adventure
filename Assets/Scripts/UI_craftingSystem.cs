using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization; 
public class UI_craftingSystem : MonoBehaviour
{
    public GameObject[] slots;
    public Inventory inventory;
    public CraftingSystem craftingSystem;
    private int x = 0;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Transform itemSlotContainer2;
    RectTransform itemSlotRectTransform;
    private int currentSlot;
    public LocalizedString myString;
    string localizedText;
    public Item item;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory(5);
        craftingSystem = new CraftingSystem();
        craftingSystem.OnGridChanged += OnItemListChanged;
        Inventory_OnItemListChanged();

    }

    public void OnItemListChanged(object sender, System.EventArgs e)
    {
        int i = 0;
        Item[] items = inventory.getItemList();
        foreach (Item item in items)
        {
            if (i < 4)
            {
                Item item2 = craftingSystem.getItem(i);
                if(item2 != null && item2.item != Item.ItemType.None && item2.amount <= 0)
                {
                     inventory.RemoveInventory(item2);
                }else 
                if(item2.item == Item.ItemType.None)
                {
                    Item item3 = inventory.getItem(i);
                    if (item3 != null)
                    {


                        inventory.RemoveInventory(item3);
                    }
                }

            }
            i++;
        }
        
        Inventory_OnItemListChanged();
    }
    public void Inventory_OnItemListChanged()
    {
        x = 0;
        foreach (Item item in inventory.getItemList())
        {

            updateVisual(item);
        }
    }
    public void updateVisual(Item item)
    {
        if (item != null)
        {
            if (item.amount <= 0)
            {
                item = null;
            }
        }
        if (x == 4)
        {
            item = craftingSystem.getOutputItem();
            if (item != null)
            {
                ItemScriptableObject items = Resources.Load<ItemScriptableObject>("ScriptableObjects/Items/" + item.GetString());

                item = new Item { itemScriptableObject = items, itemNumber = items.itemNumber, item = items.itemType, name = items.itemName, amount = 16, maxAmount = items.maxAmount, maxedOut = false, category = items.category };
                this.item = item;
            }
        }
           
       
        itemSlotRectTransform = slots[x].GetComponent<RectTransform>();








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
}
