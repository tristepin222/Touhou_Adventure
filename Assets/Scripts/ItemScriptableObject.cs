using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ItemScriptableObject")]
public class ItemScriptableObject : ScriptableObject
{
    public Item.ItemType itemType;
    public string itemName;
    public Sprite sprite;
    public int category;
    public int tier;
    public int itemNumber;
    public int weight;
    public Transform crop;
    public float price;
    public int maxAmount;
    public int Shopamount;
    
}
