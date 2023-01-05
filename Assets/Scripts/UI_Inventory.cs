using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
   
    private Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Transform itemSlotContainer2;
    public Text FullException;
    float itemSlotCellSize;
    int x;
    int y;
    bool once = true;
    bool deletelast = false;
    RectTransform itemSlotRectTransform;
    int currentSlot;
    int x2 = 0;
    public GameObject[] gameObjects;
    HotKeySystem hks;
    UI_inventoryItemSlot inventoryItemSlot;
    public LocalizedString myString;

    string localizedText;

    private void OnEnable()
    {
        RefreshInventoryItems();
    }

    private  void Start() {
      
        x = 0;  
        x2 = 0;
        gameObjects = new GameObject[54];
        itemSlotContainer2 = GameObject.Find("itemSlotContainer2").transform;

        for (int i = 0; i < itemSlotContainer2.childCount; i++)
        {
            gameObjects[i] = itemSlotContainer2.GetChild(i).gameObject;
        }
        for (int i = 0; i < itemSlotContainer.childCount; i++)
        {
            gameObjects[i+9] = itemSlotContainer.GetChild(i).gameObject;
        }
        itemSlotTemplate.gameObject.SetActive(false);
       
            RefreshInventoryItems(true);
       if(inventory != null)
        {
            RefreshInventoryItems();
        }
       
        FullException.enabled = false;
        int i2 = 0;
        foreach (GameObject slot in gameObjects)
        {
            slot.GetComponent<UI_inventoryItemSlot>().itemi = i2;
            i2++;
        }
    }
    public void SetInventory(Inventory inventory, HotKeySystem hotKeySystem)
    {
        this.inventory = inventory;
       
        this.inventory.OnItemListChanged += Inventory_OnItemListChanged;
       hotKeySystem.OnSwap += Inventory_OnItemListChanged;
      
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e){
      RefreshInventoryItems();
    }
    public void RefreshInventoryItems(bool bypass = false){
        
        x = 0;
       y = 0;
        itemSlotCellSize = 75f;
   

        if (bypass)
        {
            for (int b = 0; b <= 53; b++)
            {
                CreateSlot(null,bypass);
                }
        }
        else
        {
            if (inventory != null)
            {
                foreach (Item item in inventory.getItemList())
                {

                    CreateSlot(item);
                }
            }
        }
           
           
        
    }
   public void selectSlot(int slot)
    {
        int i = itemSlotContainer2.transform.childCount;

        for(int a= 0; a < i; a++)
        {

            itemSlotRectTransform = itemSlotContainer2.GetChild(a).GetComponent<RectTransform>();
            inventoryItemSlot = itemSlotContainer2.GetChild(a).GetComponent<UI_inventoryItemSlot>();
            inventoryItemSlot.transform.SetSiblingIndex(inventoryItemSlot.itemi);
            Image imageContent2 = itemSlotRectTransform.Find("Image2").GetComponent<Image>();
            Image imageContent = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            Image image2 = itemSlotRectTransform.Find("Image3").GetComponent<Image>();
            if (a == slot)
            {
                
                    imageContent2.enabled = true;
                   
               
            }
            else
            {
                imageContent2.enabled = false;
               
            }
        }

        currentSlot = slot;
       
    }

    void CreateSlot(Item item, bool bypass = false)
    {

        itemSlotRectTransform = gameObjects[x].GetComponent<RectTransform>();








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

        if ( item == null)
        {
           
        }
        else if (currentSlot == x)
        {
            if (once)
            {
               
                once = false;
            }
        }

       

            x++;
    }

    private void DeleteLast()
    {
        if (x >= 5)
        {
        }
        else
        {
            itemSlotRectTransform = (RectTransform)itemSlotContainer.GetChild(x + 2);
            itemSlotRectTransform.gameObject.SetActive(false);
        }
      
       
    }
    
}
