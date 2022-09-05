using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootingTable : MonoBehaviour
{
    public GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.Instance.UI_fishing.GetComponent<FishingSystem>().lt = items;
    }

}
