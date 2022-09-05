using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

[System.Serializable]
public class saver 
{
    public int life;
    public string scene;
    public float[] position;
    public int[] weight;
    public int[] itemNumber;
    public string[] itemName;
    public string[] itemTypeName;
    public float[] options;
    public int[] maxAmount;
    public int[] Amount;
    public string[] spriteName;
    public string[] clothes;
    public int[] clothesi;
    public float[,] colors;
    public string characterName;
    public int[] category;
    public float money;
    public int[] objectiveCompleted;
    public bool tutcomp;
    public int objindex5;
    public string currentobj;
    public int lvl;
    public float xp;
    public float nextXp;
    public int[] IDs;
    public int[] indexes1;
    public int[] indexes2;
    public int[] indexes3;
    public bool[] encounters;
    public int day;
    public float time;
    public saver(GlobalControl data, options data2, dataStatic data3)
    {
        encounters = new bool[100];
        IDs = new int[100];
        indexes1 = new int[100];
        indexes2 = new int[100];
        indexes3 = new int[100];
        position = new float[3];
        itemNumber = new int[54];
        weight = new int[54];
        maxAmount = new int[54];
        Amount = new int[54];
        itemName = new string[54];
        category = new int[54];
        itemTypeName = new string[54];
        spriteName = new string[54];
        options = new float[2];
        clothes = new string[8];
        colors = new float[8, 3];
        clothesi = new int[8];
        objectiveCompleted = data.obtosv.ToArray();
        money = data.money;
        characterName = data3.characterName;
        life = data.life.lifeAmount;
        scene = data.lastScene;
        position[0] = data.player.transform.position.x;
        position[1] = data.player.transform.position.y;
        tutcomp = data3.tutcomp;
        Item[] items = data.inventory.getItemList();
        int i = 0;
        objindex5 = data.objindex5;
        currentobj = data.currentobj;
        lvl = data.lvl;
        xp = data.xp;
        nextXp = data.nextXp;
        foreach(Item item in items)
        {
            if (item != null)
            {
                itemNumber[i] = item.itemNumber;
                weight[i] = item.weight;
                itemName[i] = item.GetString();
                itemTypeName[i] = item.item.ToString();
                maxAmount[i] = item.maxAmount;
                Amount[i] = item.amount;
                
                if (item.sprite == null)
                {
                    spriteName[i] = item.itemScriptableObject.sprite.name;
                }
                else
                {
                    spriteName[i] = item.sprite;
                }
                i++;
            }
        }
        options[0] = data2.mAmount;
        options[1] = data2.sAmount;
        i = 0;
        foreach ( int index in data3.Indexes)
        {
            clothesi[i] = index;
            i++;
        }
        i = 0;
        foreach (string name in data3.Names)
        {
            clothes[i] = name;
            i++;
        }
        i = 0;
        foreach (Color color in data3.colors)
        {
            if (i < 8)
            {
                colors[i, 0] = color.r;
                colors[i, 1] = color.g;
                colors[i, 2] = color.b;
            }
            i++;
        }
        int index1 = 0;
        foreach(DialogueInformation DialogueInformation in data.DialogueInformations)
        {
            if (DialogueInformation != null)
            {
                IDs[index1] = DialogueInformation.ID;
                indexes1[index1] = DialogueInformation.index+1;
                indexes2[index1] = DialogueInformation.index2+1;
                indexes3[index1] = DialogueInformation.index3+1;
            }
            index1++;
        }
        encounters = data.encounters;
        time = data3.savedDay;
        day = data.currentDay;
    }
   
}
