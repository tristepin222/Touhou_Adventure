using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
  [SerializeField]  PlayerManagament player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Inventory inventory = player.GetInventory();
        Item[] items = inventory.getItemList();
       
        foreach(Item item in items)
        {
            Debug.Log(item.name);
        }
    }
}
