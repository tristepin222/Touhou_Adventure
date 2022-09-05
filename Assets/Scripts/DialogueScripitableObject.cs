using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/DialogueScriptableObject")]
public class DialogueScripitableObject : ScriptableObject
{

    public Sprite[] reactionsPortrait;
    public Sprite[] reactionsSprite;
    public string[] strings;
    public string[] Badstrings;
    public int[] reactionsType;
    public int[] stringsType;
    public bool[] isContinue;
    public actions[] actions;
    public bool genericSeller;
    
    
}

