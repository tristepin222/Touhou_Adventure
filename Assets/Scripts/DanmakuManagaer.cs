using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanmakuManagaer : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject projectile2;
    [SerializeField] public float bulletSpeed;
    public PlayerManagament player;
    public int cooldownDanmakuMax;
    public int cooldownDanmakuMax2;
    public int bulletAmount;
    private Vector3 target;
    private Vector3 difference;
    private int cooldownDanmaku = 0;
    private int cooldownDanmaku2 = 0;
    private bool is_active = false;
    EnemiManagament enemiManagament;
    void Start()
    {
        GameObject GPlayer = GameObject.FindGameObjectWithTag("Player");
        player = GPlayer.GetComponent<PlayerManagament>();
        enemiManagament = this.GetComponent<EnemiManagament>();
    }
   
    // Update is called once per frame
    void Update()
    {
        is_active = enemiManagament.is_active;
        if (is_active)
        {
            if (projectile != null)
            {

                if (cooldownDanmaku >= cooldownDanmakuMax)
                {
                    target = player.transform.position;
                    difference = target - this.transform.position;

                    difference.Normalize();
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                    LaunchProjectile(direction, rotationZ, projectile);

                    cooldownDanmaku = 0;
                }
            }
            if (projectile2 != null)
            {
                if (cooldownDanmaku2 >= cooldownDanmakuMax2)
                {
                    target = player.transform.position;
                    difference = target - this.transform.position;

                    difference.Normalize();
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                    LaunchProjectile(direction, rotationZ, projectile2);

                    cooldownDanmaku2 = 0;
                }
            }
        }
    }
    private void LaunchProjectile(Vector2 direction, float rotationZ, GameObject projectile)
    {
        GameObject b = Instantiate(projectile) as GameObject;

        b.transform.position = this.GetComponent<Transform>().position;

        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        
        for(int i = 0; i < b.transform.childCount; i++)
        {
            Rigidbody2D c = b.transform.GetChild(i).GetComponent<Rigidbody2D>();

            c.velocity = c.transform.right*bulletSpeed;


        }
        
    }
    private void OnBecameInvisible()
    {
        is_active = false;
        
    }
    private void OnBecameVisible()
    {
        is_active = true;
       
    }
    private void FixedUpdate()
    {
        cooldownDanmaku++;
        cooldownDanmaku2++;
    }
}
