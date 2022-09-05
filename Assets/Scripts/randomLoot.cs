using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomLoot : MonoBehaviour
{
    public GameObject[] items;
    private int rand = 0;
    private int total = 0;
    private int i;
    private GameObject item;
    private Item buffI;
    lootingTable lt;
    public bool useLootTable = false;
    private void Awake()
    {
        if (useLootTable) { 
            lt = FindObjectOfType<lootingTable>();
        if (lt != null)
        {
            items = lt.items;
        }
    }
        if (items.Length > 0)
        {

            foreach (GameObject item in items)
            {
                lootManager lm = item.GetComponent<lootManager>();
                total += lm.getWeight();

            }
            rand = Random.Range(0, total);
            for (i = 0; i < items.Length; i++)
            {
                lootManager lm = items[i].GetComponent<lootManager>();
                if (rand <= lm.getWeight())
                {
                    item = items[i];
                    return;
                }
                else
                {
                    rand -= lm.getWeight();
                }

            }
        }
      
    }

    public GameObject getItem()
    {
        return item;
    }

    private void Update()
    {
        
    }
}
