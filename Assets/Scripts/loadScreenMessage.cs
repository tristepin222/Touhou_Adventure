using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LoadScreenMessage")]

public class loadScreenMessage : ScriptableObject
{
   [SerializeField] public GameObject gameObject;
}
