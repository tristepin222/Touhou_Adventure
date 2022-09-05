using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
public class lootManager : MonoBehaviour
{
    [SerializeField] ItemScriptableObject itemScriptableObject;
    [SerializeField] int maxAmount;
    [SerializeField] public int amount = 1;
    public Item item;

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = itemScriptableObject.sprite;
    }
    private void CreateItem()
    {
       
        item = new Item { sprite = itemScriptableObject.sprite.name, itemScriptableObject = this.itemScriptableObject, itemNumber = this.itemScriptableObject.itemNumber, t = this.itemScriptableObject.crop, name = this.itemScriptableObject.itemName, weight = this.itemScriptableObject.weight, item = this.itemScriptableObject.itemType, amount = this.amount, maxAmount = itemScriptableObject.maxAmount, maxedOut = false};
    }
    
  
  
    public Item GetItem()
    {
        if (item.name == "")
        {
            CreateItem();
        }
        return item;
    }
    public int getWeight()
    {
        if(item.name == "")
        {
            CreateItem();
        }
        return item.weight;
    }
}
