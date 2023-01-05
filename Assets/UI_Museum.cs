using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
public class UI_Museum : MonoBehaviour
{
    public List<MuseumItem> museumItems = new List<MuseumItem>();
    public List<GameObject> Categories = new List<GameObject>();
    public GameObject[] slots;
    public GameObject preview;
    public LocalizedString myString;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public Transform itemSlotContainer2;
    public string[] successes;
    private int x = 0;
    private int shopi = 0;
    private Item item;
    private Item preview_item;
    private string localizedText;
    private RectTransform itemSlotRectTransform;
    private int currentSlot;
    private bool conti;
    private bool bufferBool;
    private int bufferCategory;
    private SuccessManager successManager;
    // Start is called before the first frame update

    private void Start()
    {
        bufferCategory = 0;
        int i = 0;
        successManager = FindObjectOfType<SuccessManager>();
        bufferBool = true;
        foreach (MuseumItem item in museumItems)
        {


           
            item.index = i;
            i++;
            if (dataStatic.Instance.museumItems[item.index])
            {
                bufferBool = true;
                item.objectToShow.GetComponent<SpriteRenderer>().enabled = true;
            } 
            else if (!dataStatic.Instance.museumItems[item.index])
            {
                bufferBool = false;
            }
            else if (bufferCategory != item.category)
            {
                bufferBool = false;
            }
            if (successManager != null)
            {
                if (bufferBool)
                {
                    switch (bufferCategory)
                    {
                        case 0:
                            successManager.SetAchievement(successes[0]);
                            break;
                        case 1:
                            successManager.SetAchievement(successes[1]);
                            break;
                        case 2:
                            successManager.SetAchievement(successes[2]);
                            break;
                        case 3:
                            successManager.SetAchievement(successes[3]);
                            break;
                    }
                }
            }
            bufferCategory = item.category;
        }
        this.gameObject.SetActive(false);
        Initialize();
      
    }

    private void Initialize()
    {
         
            Inventory_OnItemListChanged();
            GlobalControl.Instance.inventory.OnItemListChanged += Inventory_OnitemListChanged_player;
         
        
    }

    private void OnEnable()
    {
            Initialize();
       
    }

    private void Inventory_OnItemListChanged()
    {
        x = 0;
        foreach (Item item in GlobalControl.Instance.inventory.getItemList())
        {

            updateVisual(item);
        }
    }

    public void Inventory_OnitemListChanged_player(object sender, System.EventArgs e)
    {
        x = 0;
        foreach (Item item in GlobalControl.Instance.inventory.getItemList())
        {

            updateVisual(item);
        }
    }

    public void updateVisual(int i)
    {
        shopi = i;
        Text textcontent = preview.transform.Find("Text").GetComponent<Text>();
        Text amountContent = preview.transform.Find("Amount").GetComponent<Text>();
        Image imageContent = preview.transform.Find("Image").GetComponent<Image>();
        item = GlobalControl.Instance.inventory.getItem(i);
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
    public void updateVisual(Item item)
    {
        if (item != null)
        {
            if (item.amount <= 0)
            {
                item = null;
            }
        }


            itemSlotRectTransform = slots[x].GetComponent<RectTransform>();
            itemSlotRectTransform.GetComponent<UI_inventoryItemSlot>().itemi = x;
       
     








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

    public void donate()
    {
        bufferCategory = -1;
        bufferBool = true;
        foreach (MuseumItem museumItem in museumItems)
        {
            if (preview_item != null)
            {
                if (preview_item.itemScriptableObject == museumItem.itemScriptableObject)
                {
                    GlobalControl.Instance.inventory.Decrease(preview_item, 1);

                    Inventory_OnItemListChanged();

                    museumItem.objectToShow.GetComponent<SpriteRenderer>().enabled = true;
                    GlobalControl.Instance.museumItem[museumItem.index] = true;
                    updateVisual(shopi);
                    bufferCategory = museumItem.category;
                }

                if (bufferCategory != museumItem.category) 
                {
                }
                else
                {
                    if (!GlobalControl.Instance.museumItem[museumItem.index])
                    {
                        bufferBool = false;
                    }
                   
                   
                }
             
            }
        }
        if (successManager != null)
        {
            if (bufferBool)
            {
                switch (bufferCategory)
                {
                    case 0:
                        successManager.SetAchievement(successes[0]);
                        break;
                    case 1:
                        successManager.SetAchievement(successes[1]);
                        break;
                    case 2:
                        successManager.SetAchievement(successes[2]);
                        break;
                    case 3:
                        successManager.SetAchievement(successes[3]);
                        break;
                }
            }
        }
    }
}

