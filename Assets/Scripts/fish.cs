using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fish : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] PlayerManagament player;
    public projectileManager prj;
    Inventory inv;
    int intItem;
    Item item;
    [SerializeField] GameObject ccatch;
    GameObject b;
    [SerializeField] float bulletSpeed;
    GameObject obj;
    private bool launched;
    int timer = 0;
    int rand = 0;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        }
        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if (player == null)
        {
            player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();

        }

        inv = player.GetInventory();
        if (inv != null)
        {
            if (player.inventory.getItemList().Length != 0)
            {


                item = player.inventory.getItem(player.selectedItem);

            }
        }
        if (item != null)
        {
            if (item.item == Item.ItemType.FishingRod)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (prj == null || !prj.sceneLoaded )
                    {
                        if (!launched)
                        {
                            b = ccatch;
                            launched = true;
                        }
                        
                    }
                    else
                    {
                        Destroy(obj);


                        launched = false;
                    }
                }
                
            }
            
        }
        if (b != null)
        {

            b.transform.position = this.transform.position;

            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            LaunchProjectile(direction, rotationZ, ccatch, isBomb: true);
            b = null;
        }
        if(obj == null)
        {
            launched = false;
        }
        
    }

   
    private void LaunchProjectile(Vector2 direction, float rotationZ, GameObject projectile, int offset = 1, bool isBomb = false)
    {

        rand =  Random.Range(1000, 10000);

        GameObject b = Instantiate(projectile) as GameObject;
        float fOffset = 0.5f;
        if (isBomb)
        {
            b.transform.position = this.GetComponent<Transform>().position;
        }
        else
        {

            b.transform.position = this.GetComponent<Transform>().position - new Vector3(0, 0.5f, 0);



        }
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.transform.Translate(Vector3.right * 1.5f);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        GlobalControl.Instance.UI_fishing.GetComponent<FishingSystem>().ccatch = b;
        obj = b;
        prj = b.GetComponent<projectileManager>();
    }
}
