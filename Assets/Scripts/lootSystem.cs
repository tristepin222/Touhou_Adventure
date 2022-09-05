using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootSystem : MonoBehaviour
{

    
    [SerializeField] GameObject loot;
    [SerializeField] GameObject target;
    public AudioSource audio;
    public AudioClip audio2;
    public int amount = 1;
    private Item item;
    public bool canHarvest = true;
    catachMovement fishm;
    public bool IsPlayer = false;
    public bool isCrop = false;
    public plant plant;
    public bool disabled = false;
    private void Start()
    {
        if (this.gameObject.GetComponent<randomLoot>() != null)
        {
            randomLoot rl = this.gameObject.GetComponent<randomLoot>();
            loot = rl.getItem();
        }
        fishm = FindObjectOfType<catachMovement>();

        if (fishm == null)
        {
            fishm = FindObjectOfType<catachMovement>();
        }
        if (target != null)
        {
            if (target.tag == "Player")
            {
                target = GlobalControl.Instance.player;
            }
        
    }
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!disabled)
        {
            if (collision.tag == "ProjectilePlayer")
            {
                projectileManager pr;
                bool found;
                found = collision.TryGetComponent(out pr);
                if (found)
                {
                    if (pr.projectileScriptableObject.projectileType == Projectile.ProjectileType.Hoe || pr.projectileScriptableObject.projectileType == Projectile.ProjectileType.Pickaxe)
                    {
                        if (canHarvest)
                        {
                            spawnItemInWorld();
                            if (audio2 != null)
                            {
                                AudioSource.PlayClipAtPoint(audio2, this.gameObject.transform.position, options.Instance.sAmount);
                            }
                            if (isCrop)
                            {
                                plant.grid.SetValue(plant.x, plant.y, 1);
                            }
                            Destroy(this.gameObject);

                        }
                    }
                }

            }
        }
    }
 

    public void spawnItemInWorld()
    {
        
            bool canLoot = false;
            if (target == null)
            {
                target = this.gameObject;
            }
            if (this.tag == "catch")
            {
                
                    if (GlobalControl.Instance.UI_fishing.GetComponent<FishingSystem>().caughtFish)
                    {
                        canLoot = true;
                    }
                    else
                    {
                        canLoot = false;
                    }
               
            }
            else
            {
                canLoot = true;
            }
            if (canLoot)
            {
            GlobalControl.Instance.UI_fishing.GetComponent<FishingSystem>().caughtFish = false;
                for (int i = 0; i < amount; i++)
                {
                    if (IsPlayer)
                    {
                        target = GlobalControl.Instance.player;

                    }
                    else
                    {
                        target = this.gameObject;

                    }
                if (loot != null)
                {
                    loot = Instantiate(loot) as GameObject;
                    loot.transform.position = target.transform.position;
                }

                }
            }
        }
    

}

