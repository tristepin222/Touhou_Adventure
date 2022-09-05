using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
public class Inventory
{

    public event EventHandler OnItemListChanged;
    private Item[] itemList;
    private bool has_item = false;
    public bool empty;
    public int maxSlots = 32;
    private int index2 = 0;
    public bool isPlayer = false;
   

    public Inventory(int slots = 6)
    {
        itemList = new Item[slots];



    }



    public void AddInventory(Item itemName, bool bcontinue = false)
    {
        bool once = false;
        bool found = false;
        int index = 0;
        index2 = 0;
        int index3 = 0;
        Item itemtoadd = null;





        foreach (Item item in itemList)
            {
                
                if (item == null)
                {
                if (itemList.Length >= maxSlots)
                {
                    if (itemList[itemList.Length - 1] != null)
                    {
                        throw new InventoryFullException();
                    }
                }


                itemList[index] = itemName;
                break;
                }
                else
                {


                if (!bcontinue)
                {
                    if (item.itemScriptableObject == itemName.itemScriptableObject)
                    {
                        if (item.amount >= itemName.maxAmount)
                        {
                            AddInventory(itemName, true);

                        }
                        else
                        {
                            item.amount += itemName.GetAmount();
                            found = true;
                        }
                        break;
                    }
                    else
                    {
                        found = false;
                    }


                }
                }
            index++;
        }

       
        if (!found)
        {

            found = false;
            


        }





        if (OnItemListChanged != null)
        {
            if (isPlayer)
            {
                OnItemListChanged.Invoke(this, EventArgs.Empty);
            }
        }
   
    }
    private void AddInventory(Item itemName, int index)
    {
        itemList[index] = itemName;
    }

        public Item[] getItemList()
    {
        return itemList;
    }
    public Item getItem(int i)
    {
        return itemList[i];
    }

    public bool hasItem(string nameItemNeeded)
    {
        foreach(Item item in itemList)
        {
            if(item == null)
            {
                continue;
            }
            string name = item.name;
            if(name == nameItemNeeded)
            {
                return true;
            }
            
        }

        return false;
    }
    public int hasItemInt(string nameItemNeeded)
    {
        int i = 0;
        foreach (Item item in itemList)
        {
            if (item == null)
            {
                continue;
            }
            string name = item.name;
            if (name == nameItemNeeded)
            {
                return i;
            }
            
            i++;
        }

        return 0;
    }
    public override string ToString()
    {
        return base.ToString();
    }
    public void RemoveInventory(Item itemName)
    {
        bool found = false;
        Item itemInInventory = null;
        int index = 0;
        if (itemList.Length != 0)
        {
            empty = false;
            foreach(Item item in itemList)
            {
                
                if (!found)
                {
                    if (item == null)
                    {
                        index++;
                        continue;
                    }
                    else
                    {
                        
                        if (item.itemScriptableObject == itemName.itemScriptableObject)
                        {

                            item.amount -= itemName.GetAmount();
                            found = true;
                            itemInInventory = item;
                           
                            break;
                        }
                        index++;
                    }
                }
               
            }
            if (itemInInventory != null)
            {
                if (itemInInventory.amount <= 0)
                {
                    itemList[index] = null;
                    found = false;
                }
            }

        }
        else
        {
            empty = true;
            throw new InventoryEmpty();
        }
        if (OnItemListChanged != null)
        {
            OnItemListChanged.Invoke(this, EventArgs.Empty);

        }

    }

    public void Decrease(Item itemName, int amount)
    {
        if (itemName.amount <= 1)
        {
            RemoveInventory(itemName);
        }
        else
        {
            itemName.amount -= amount;
        }
        if (itemName.amount <= 0)
        {
            RemoveInventory(itemName);
        }
        if (OnItemListChanged != null)
        {
            OnItemListChanged.Invoke(this, EventArgs.Empty);

        }
    }
    public void swap(int index1, int index2)
    {
        
        Item i = itemList[index1];
        if (index1 != index2)
        {
            if (itemList[index1] != null && itemList[index2] != null)
            {

                if (itemList[index1].itemScriptableObject == itemList[index2].itemScriptableObject)
                {
                    int calculatedAmount = itemList[index1].amount + itemList[index2].amount;
                    if (calculatedAmount >= itemList[index1].maxAmount)
                    {
                        itemList[index1].amount = itemList[index1].maxAmount;
                        itemList[index2].amount = calculatedAmount - itemList[index1].maxAmount;
                    }
                    else
                    {

                        itemList[index1].amount += itemList[index2].amount;
                        itemList[index2] = null;
                    }
                }
                else
                {
                    itemList[index1] = itemList[index2];
                    itemList[index2] = i;
                }
            }
            else
            {
                itemList[index1] = itemList[index2];
                itemList[index2] = i;
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
}

