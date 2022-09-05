using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyManagement : MonoBehaviour
{
    
    
    [SerializeField] int spawn = 0;
    [SerializeField] GameObject[] enemies;
    [SerializeField] bool[] fixedSpawns;
    [SerializeField] GameObject[] anchors;
    
    private bool once = true;
    
    
    private void Awake()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {


            if (GlobalControl.Instance.spawns.Length <= spawn)
            {
                GlobalControl.Instance.spawns[spawn] = false;
            }
            
            
          
            
               
            if (once)
            {                
                if (GlobalControl.Instance.spawns[spawn] != true)
                {




                    int i = 0;
                    foreach (GameObject enemy in enemies)
                    {
                        
                        SpawnEnemy(enemy, anchors[i], fixedSpawns[i]);
                        i++;
                    }

                    once = false;


                }
                GlobalControl.Instance.spawns[spawn] = true;
            }
        }
    }
    private void SpawnEnemy(GameObject enemy, GameObject anchor, bool fixedSpawn = false )
    {
       
        GameObject GEnemy = Instantiate(enemy) as GameObject;
        if (fixedSpawn)
        {
            GEnemy.transform.position = anchor.transform.position;
        }
        else
        {
            GEnemy.transform.position = this.transform.position;
        }
    }

  
}
