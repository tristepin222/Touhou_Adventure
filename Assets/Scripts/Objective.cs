using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Objective")]
public class Objective : ScriptableObject 
{
    public int ID;
    public string objname;
    public string desc;
    public bool InMap;
    public bool useString;
    public bool showArrow = true;
    public List<GameObject> Prefabs = new List<GameObject>();
    public List<string> PrefabNames = new List<string>();
    public bool IsCompleted;
    public bool checkforitem;
    public ItemScriptableObject itemToCheck;
}
