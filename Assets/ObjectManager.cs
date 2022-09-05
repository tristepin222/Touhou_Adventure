using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public actions action;

    private void Start()
    {
        if (GlobalControl.Instance.HasFarm && this.tag == "FarmObstacle")
        {
            AddtoSavedObject();
        }
    }
    public void AddtoSavedObject()
    {
        
        GlobalControl.Instance.toSave.Add(this.gameObject);
    }
}
