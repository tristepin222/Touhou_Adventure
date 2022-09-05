using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class projectileManager : MonoBehaviour
{
    [SerializeField] public ProjectileScriptableObject projectileScriptableObject;
    [SerializeField] int amount;
    [SerializeField] public bool isBeam;
    [SerializeField] public bool is0;
    [SerializeField] public float angle = -90;
    [SerializeField] public bool handable = false;
    public lootSystem ls;
    public int delay;
    private Rigidbody2D rb;
    private Vector2 bounds;
    private SpriteRenderer sr;
    int time = 0;
    [SerializeField] public  bool animated = false;
    private bool onWater = false;
    [SerializeField] int MaxTime = 400;
    public bool sceneLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        if (animated)
        {
            Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        }
        rb = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        if (is0)
        {
            this.transform.Rotate(Vector3.forward * angle);
        }
        if (!animated)
        {
            sr.sprite = projectileScriptableObject.sprite;
        }
    }

    // Update is called once per frame
   void Update()
    {
       
        
            if (time >= MaxTime)
            {

            if (!onWater)
            {
                Destroy(this.gameObject);
               
                
                time = 0;
            }
            
               

            
            }
        
        if(time>= 10)
        {
            if (is0)
            {
                this.GetComponent<Rigidbody2D>().mass = 0f;
            }
        }
        if (onWater)
        {

            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity * 0.9f;

            Quaternion quat = this.gameObject.transform.rotation;
           this.gameObject.transform.rotation = new Quaternion(quat.x, quat.y, quat.z, quat.w / 0.9f);
            if(quat == new Quaternion(0, 0, 0, quat.w))
            {
                if (!sceneLoaded)
                {

                    GlobalControl.Instance.UI_fishing.GetComponent<Canvas>().enabled = true;
                }
                sceneLoaded = true;
            }
        }
        
    }
    private void FixedUpdate()
    {
        time++;


    }

    private void OnBecameInvisible()
    {
        
            Destroy(this.gameObject);
        
    }
    public int getDamage()
    {
        return projectileScriptableObject.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        plant plant;
        if (this.tag == "catch")
        {
            if (collision.tag == "water")
            {
                onWater = true;
                
            }
        }
        if (projectileScriptableObject.projectileType == Projectile.ProjectileType.Hoe)
        {
            if (collision.TryGetComponent(out plant))
            {
                if (plant.grid.GetValue(plant.x, plant.y) == 0)
                {

                    plant.grid.SetValue(plant.x, plant.y, 1);

                }

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
     
            if (tag == "catch")
            {
                if (collision.tag == "water")
                {
                    onWater = false;

                }
            }

        
    }

    private void OnDestroy()
    {
        if (animated)
        {
            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
            GlobalControl.Instance.player.GetComponent<PlayerMovement>().enabled = true;
        }
        if (this.tag == "catch")
        {
            ls = this.gameObject.GetComponent<lootSystem>();
            if (ls != null)
            {
                ls.spawnItemInWorld();
            }
            if (sceneLoaded)
            {
                GlobalControl.Instance.UI_fishing.GetComponent<Canvas>().enabled = false;
                sceneLoaded = false;
            }
        }

    }
}
