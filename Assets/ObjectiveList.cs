using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveList : MonoBehaviour
{
    public List<Objective> Objectives = new List<Objective>();

    private void Start()
    {
        GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().set(Objectives);
    }
}
