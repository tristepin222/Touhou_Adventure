using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    // Start is called before the first frame update
    public enum ItemType{
        None,
        Spell,
        HealthPotion,
        Coin,
        Wood,
        Rock,
        Hoe,
        Daikon,
        Crop,
        Satsumaimo,
        FishingRod,
        fish,
        fireFish_small,
        fireFish_medium,
        mistyFish_small,
        mistyFish_medium,
        lamprey,
        pond_loach,
        crystal,
        iron,
        ironOre,
        Crystals_Pellets,
        ironOre_Pellets,
        Pickaxe,
        VampireAmulet,
        FarmPermit,
        Cabbage,
        Carrot,
        ChineseCabbage,
        Cucumber,
        Eggplant,
        Potato,
        Leek,
        Onion,
        Spinach,
        Taro,
        Tomato,
        Turnip,
        CarrotSeeds,
        CabbageSeeds,
        ChineseCabbageSeeds,
        CucumberSeeds,
        EggplantSeeds,
        LeekSeeds,
        OnionSeeds,
        SpinachSeeds,
        TomatoSeeds,
        TurnipSeeds,
    }

    // Update is called once per frame
    public ItemScriptableObject itemScriptableObject;
    public ItemType item;
    public int amount; 
    public  int maxAmount;
    public bool maxedOut;
    public string name;
    public int category;
    public int tier;
    public int itemNumber;
    public int weight;
    public string sprite;
    public Transform t;
    public float coinAmount;
    public string GetString(){
        switch(item)
        {
            default:
            case ItemType.Spell: return "Spell";
            case ItemType.HealthPotion : return "HealthPotion";
            case ItemType.Coin : return "coin";
            case ItemType.Wood : return "Wood";
            case ItemType.Rock : return "Rock";
            case ItemType.Hoe: return "hoe";
            case ItemType.Daikon: return "daikon";
            case ItemType.Satsumaimo: return "Satsumaimo";
            case ItemType.FishingRod: return "FishingRod";
            case ItemType.fireFish_small: return "FireFish_small";
            case ItemType.fireFish_medium: return "FireFish_medium";
            case ItemType.mistyFish_small: return "MistyFish_small";
            case ItemType.mistyFish_medium: return "MistyFish_medium";
            case ItemType.lamprey: return "Lamprey";
            case ItemType.pond_loach: return "Pond_loach";
            case ItemType.crystal: return "crystal";
            case ItemType.iron: return "iron";
            case ItemType.ironOre: return "ironOre";
            case ItemType.Crystals_Pellets: return "crystal pellets";
            case ItemType.ironOre_Pellets: return "iron ore pellets";
            case ItemType.Pickaxe: return "pickaxe";
            case ItemType.VampireAmulet: return "VampireAmulet";
            case ItemType.FarmPermit: return "FarmPermit";
            case ItemType.Cabbage: return "Cabbage";
            case ItemType.Carrot: return "Carrot";
            case ItemType.ChineseCabbage: return "ChineseCabbage";
            case ItemType.Cucumber: return "Cucumber";
            case ItemType.Eggplant: return "Eggplant";
            case ItemType.Potato: return "Potato";
            case ItemType.Leek: return "Leek";
            case ItemType.Onion: return "Onion";
            case ItemType.Spinach: return "Spinach";
            case ItemType.Taro: return "Taro";
            case ItemType.Tomato: return "Tomato";
            case ItemType.Turnip: return "Turnip";
            case ItemType.CabbageSeeds: return "CabbageSeeds";
            case ItemType.CarrotSeeds: return "CarrotSeeds";
            case ItemType.ChineseCabbageSeeds: return "ChineseCabbageSeeds";
            case ItemType.CucumberSeeds: return "CucumberSeeds";
            case ItemType.EggplantSeeds: return "EggplantSeeds";
            case ItemType.LeekSeeds: return "LeekSeeds";
            case ItemType.OnionSeeds: return "OnionSeeds";
            case ItemType.SpinachSeeds: return "SpinachSeeds";
            case ItemType.TomatoSeeds: return "TomatoSeeds";
            case ItemType.TurnipSeeds: return "TurnipSeeds";
        }

    }
    
    public ItemType GetItemType(int item)
    {
        switch (item)
        {
            default:
            case 1: return ItemType.Daikon;
            case 2: return ItemType.Coin;
        }
    }

    public int GetAmount(){
        return amount;
    }
    public int getMaxAmount(){
        return maxAmount;
    }
}
