using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CraftingSystem 
{
    public event EventHandler OnGridChanged;
    private const int SIZE = 4;
    private Item[] items;
    private Dictionary<Item.ItemType, Item.ItemType[]> recipes;
    private Item outputItem;
    // Start is called before the first frame update
    public CraftingSystem()
    {
        items = new Item[SIZE];
        recipes = new Dictionary<Item.ItemType, Item.ItemType[]>();
        Item.ItemType[] recipe = new Item.ItemType[SIZE];
        recipe[0] = Item.ItemType.crystal; recipe[1] = Item.ItemType.None; recipe[2] = Item.ItemType.None; recipe[3] = Item.ItemType.None;
        recipes[Item.ItemType.Crystals_Pellets] = recipe;
        recipe = new Item.ItemType[SIZE];
        recipe[0] = Item.ItemType.ironOre; recipe[1] = Item.ItemType.None; recipe[2] = Item.ItemType.None; recipe[3] = Item.ItemType.None;
        recipes[Item.ItemType.ironOre_Pellets] = recipe;

    }
    public bool isEmpty(int index)
    {
        return items[index] == null;
    }

    public Item getItem(int index)
    {
        return items[index];
    }
    public void increaseItemAmount(int index)
    {
        getItem(index).amount++;
        OnGridChanged?.Invoke(this, EventArgs.Empty);
    }

    public void decreaseItemAmount(int index)
    {
        if (getItem(index) != null)
        {
            getItem(index).amount--;
            if (getItem(index).amount == 0)
            {
                removeItem(index);
            }
            OnGridChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public void removeItem(int index)
    {
        setItem(null, index);
    }
    public void setItem(Item item, int index)
    {
        if (index < items.Length)
        {
            items[index] = item;

            createOutput();
        }
    }
    public bool tryAddItem(Item item, int index)
    {
    Item item2 = getItem(index);
        if (isEmpty(index) || item2.item == Item.ItemType.None )
        {
            if (item == null)
            {
                item = new Item { item = Item.ItemType.None};
            }
            setItem(item, index);

            return true;
        }
        else
        {
             if (item.item == getItem(index).item)
            {
                increaseItemAmount(index);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void removeItem(Item item)
    {
        if (item == outputItem)
        {
            // Removed output item
            consumeRecipeItems();
            createOutput();
            OnGridChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Removed item from grid
            for (int i = 0; i < SIZE; i++)
            {
                
                    if (getItem(i) == item)
                    {
                        // Removed this one
                        removeItem(i);
                    }
               
            }
        }
    }
    public Item getOutputItem()
    {
        return outputItem;
    }

    public void consumeRecipeItems()
    {
        for (int i = 0; i < SIZE; i++)
        {
            
                decreaseItemAmount(i);
           
        }
    }

    public Item.ItemType getoutput()
    {
        foreach (Item.ItemType recipeItemType in recipes.Keys)
        {
            bool completeRecipe = true;
            Item.ItemType[] recipe = recipes[recipeItemType];
            for(int i = 0; i < SIZE; i++)
            {
                if (getItem(i) == null)
                {

                  Item item = new Item { item = Item.ItemType.None };
                    setItem(item, i);
                }
                if (isEmpty(i) || getItem(i).item != recipe[i])
                {
                  
                    completeRecipe = false;
                }
            }
            if (completeRecipe)
            {
                return recipeItemType;
            }
        }
        return Item.ItemType.None;
    }
    private void createOutput()
    {
        Item.ItemType recipeOutput = getoutput();
        if (recipeOutput == Item.ItemType.None)
        {
            outputItem = null;
        }
        else
        {
            outputItem = new Item { item = recipeOutput };
            
        }
    }
    public void swap(int index1, int index2)
    {
        Item i = items[index1];

        items[index1] = items[index2];

        items[index2] = i;
        createOutput();
      OnGridChanged?.Invoke(this, EventArgs.Empty);
    }
}
