using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giver : MonoBehaviour
{
    public Item[] items;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Item item in items)
        {
            item.item = item.itemScriptableObject.itemType;
            item.itemNumber = item.itemScriptableObject.itemNumber;
            item.name = item.itemScriptableObject.itemName;
            item.category = item.itemScriptableObject.category;
        
            GlobalControl.Instance.inventory.AddInventory(item);
        }
    }

  
}
