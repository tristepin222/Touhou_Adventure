using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/AnswerScriptableObject")]
public class AnswerScriptableObject : ScriptableObject
{
   public string[] answers;
   public int[] answersType;
}
