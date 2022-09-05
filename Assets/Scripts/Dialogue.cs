using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Dialogue 
{
  

    public string DialogueLine;
    public Sprite portrait;
    public string[] Answers = new string[3];
    public string[] nextDialogueLine = new string[3];
    public bool checkforObjective;
    public bool initObjective;
    public Objective objective;
    public actions[] action;
}
