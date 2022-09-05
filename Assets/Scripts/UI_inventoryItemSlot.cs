using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.Netcode;
public class UI_inventoryItemSlot : MonoBehaviour, IDragHandler,IDropHandler, IEndDragHandler,IBeginDragHandler,IPointerClickHandler
{
    public CanvasGroup canva1;
    public Canvas canva;
   
    public bool isDifferent = false;
    RectTransform itemSlotRectTransform;
    Vector2 startingpos;
    public int itemi;
    private Item item;
    Inventory inventory;
    int index = 0;
    public UI_craftingSystem UI_craftingSystem;
    public UI_Shop UI_Shop;
    public RectTransform itemBomb;
    public ItemScriptableObject itemso;
    public bool onDropDisabled = false;
    public bool shop = false;
    public bool selfInv = false;
    public bool bombcreator = false;
    public RectTransform parent;
    private RectTransform b;
    
    private void Start()
    {
        inventory = GlobalControl.Instance.player.GetComponent<PlayerManagament>().inventory;
        canva1 = this.gameObject.GetComponent<CanvasGroup>();
        itemSlotRectTransform = this.GetComponent<RectTransform>();
        startingpos = itemSlotRectTransform.anchoredPosition;
        if (UI_craftingSystem != null)
        {
            inventory = UI_craftingSystem.inventory;

        }
    }
    // Update is called once per frame

    public void OnDrag(PointerEventData eventData)
    {
        if (!shop && !bombcreator)
        {
            itemSlotRectTransform.anchoredPosition += eventData.delta / canva.scaleFactor;
        }else if (bombcreator)
        {
            b.anchoredPosition += eventData.delta / canva.scaleFactor;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!shop && !bombcreator)
        {
            if (eventData.pointerDrag != null)
            {
                UI_inventoryItemSlot uI_InventoryItemSlot = eventData.pointerDrag.GetComponent<UI_inventoryItemSlot>();
                if (uI_InventoryItemSlot != null)
                {

                    if (UI_craftingSystem != null && uI_InventoryItemSlot.UI_craftingSystem == null)
                    {
                        if (!onDropDisabled)
                        {
                            item = GlobalControl.Instance.inventory.getItem(uI_InventoryItemSlot.itemi);
                            item = new Item { itemScriptableObject = item.itemScriptableObject, itemNumber = item.itemScriptableObject.itemNumber, item = item.item, name = item.itemScriptableObject.itemName, amount = item.amount, maxAmount = item.maxAmount, maxedOut = false };
                            inventory.AddInventory(item);

                            if (inventory.getItem(inventory.hasItemInt(item.name)) == null)
                            {
                                inventory.swap(inventory.hasItemInt(item.name), itemi);
                            }

                            GlobalControl.Instance.inventory.RemoveInventory(GlobalControl.Instance.inventory.getItem(uI_InventoryItemSlot.itemi));

                            UI_craftingSystem.craftingSystem.tryAddItem(item, itemi);
                            UI_craftingSystem.Inventory_OnItemListChanged();
                            GlobalControl.Instance.ui_inventory.GetComponent<UI_Inventory>().RefreshInventoryItems();
                        }
                        else
                        {
                            itemSlotRectTransform.anchoredPosition = startingpos;
                            canva1.alpha = 1f;
                            canva1.blocksRaycasts = true;
                            transform.SetSiblingIndex(itemi);
                        }
                    }
                    else if (uI_InventoryItemSlot.UI_craftingSystem != null && UI_craftingSystem == null)
                    {
                        if (uI_InventoryItemSlot.onDropDisabled)
                        {
                            if (uI_InventoryItemSlot.UI_craftingSystem.item != null)
                            {
                                uI_InventoryItemSlot.UI_craftingSystem.inventory.AddInventory(uI_InventoryItemSlot.UI_craftingSystem.item);
                                uI_InventoryItemSlot.UI_craftingSystem.inventory.swap(uI_InventoryItemSlot.UI_craftingSystem.inventory.hasItemInt(uI_InventoryItemSlot.UI_craftingSystem.item.name), 4);
                                uI_InventoryItemSlot.UI_craftingSystem.craftingSystem.consumeRecipeItems();
                                uI_InventoryItemSlot.UI_craftingSystem.Inventory_OnItemListChanged();
                            }
                        }
                        Item item = uI_InventoryItemSlot.UI_craftingSystem.inventory.getItem(uI_InventoryItemSlot.itemi);
                        item = new Item { itemScriptableObject = item.itemScriptableObject, itemNumber = item.itemScriptableObject.itemNumber, item = item.itemScriptableObject.itemType, name = item.itemScriptableObject.itemName, category = item.itemScriptableObject.category, amount = item.amount, maxAmount = item.maxAmount, maxedOut = false };
                        inventory.AddInventory(item);


                        inventory.swap(inventory.hasItemInt(item.name), itemi);

                        uI_InventoryItemSlot.UI_craftingSystem.inventory.RemoveInventory(uI_InventoryItemSlot.UI_craftingSystem.inventory.getItem(uI_InventoryItemSlot.itemi));

                        uI_InventoryItemSlot.UI_craftingSystem.craftingSystem.removeItem(uI_InventoryItemSlot.itemi);
                        uI_InventoryItemSlot.UI_craftingSystem.Inventory_OnItemListChanged();
                        GlobalControl.Instance.ui_inventory.GetComponent<UI_Inventory>().RefreshInventoryItems();
                    }
                    else
                    {
                        if (!onDropDisabled)
                        {
                            inventory.swap(itemi, uI_InventoryItemSlot.itemi);
                            if (UI_craftingSystem != null)
                            {
                                UI_craftingSystem.Inventory_OnItemListChanged();
                                UI_craftingSystem.craftingSystem.swap(itemi, uI_InventoryItemSlot.itemi);
                            }

                        }
                        else
                        {
                            itemSlotRectTransform.anchoredPosition = startingpos;
                            canva1.alpha = 1f;
                            canva1.blocksRaycasts = true;
                            transform.SetSiblingIndex(itemi);
                        }
                    }
                }
            }

            transform.SetSiblingIndex(itemi);
        }
       
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (!shop && !bombcreator)
        {
            itemSlotRectTransform.anchoredPosition = startingpos;
            canva1.alpha = 1f;
            canva1.blocksRaycasts = true;
            transform.SetSiblingIndex(itemi);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!shop && !bombcreator)
        {
            canva1.alpha = .6f;
            canva1.blocksRaycasts = false;

            transform.SetAsLastSibling();
        }
        else if (bombcreator)
        {
          b = Instantiate(itemBomb, parent) as RectTransform;
            b.GetComponent<Image>().sprite = itemso.sprite;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (shop && !bombcreator)
        {
           
        
        
            UI_Shop.updateVisual(itemi, selfInv);

        }
    }
}
