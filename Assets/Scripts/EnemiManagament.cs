using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Pathfinding;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
public class EnemiManagament : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public int health;
    [SerializeField] public TypeScriptable tType;
    [SerializeField] public GameObject loot;
    [SerializeField] AudioSource  audio;
    [SerializeField]  public PlayerManagament player;
    [SerializeField] public string tag;
    [SerializeField] public string tag2;
    [SerializeField] bool  isBoss;
    [SerializeField] public int amountLoot = 1;
    [SerializeField] private float XPgain;
    [SerializeField] private AIPath AiPath;
    [SerializeField] private Seeker seeker;
    [SerializeField] private AstarPath AstarPath;
    [SerializeField] private float speed = 1;
    [SerializeField] private float spreecooldown = 0.1f;
    [SerializeField] private Color color;
    [SerializeField] public GameObject link;
    [SerializeField] GameObject bomb;
    public bool linked = false;
    private LifeManagament life;
    public EventHandler hit;
    public Type type;
    public string currentScene;
   public Item item;
    private int cooldown = 0;
    private int cooldownDanmaku = 0;
    public int cooldownMax;
    public int cooldownDanmakuMax;
    public int bulletAmount;
    private Vector3 target;
    private Vector3 difference;
    public bool is_active = false;
    public GameObject[] objects;
    public Behaviour[] components;
    public CapsuleCollider2D cc2d;
    public healthsystem hs;
    public bool disabled;
    private Renderer renderer;
    private SpriteRenderer sr;
    float maxHealth;
    public GameObject PS;
    BoxCollider2D boxc;
    GameObject obstacle;
    private Path path;
    private int currentWayPoint = 0;
    private float nextWaypointDistance = 3f;
    private ScoreScreen scoreScreen;

    void Awake()
  {

        cc2d= this.GetComponent<CapsuleCollider2D>();
        sr = this.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "base";
        sr.sortingOrder = -100;
        currentScene = SceneManager.GetActiveScene().name;
       

       



        if (tag != "")
        {
            boxc = GameObject.FindGameObjectWithTag(tag).GetComponent<BoxCollider2D>();
        }
        if (tag2 != "")
        {
            obstacle = GameObject.FindGameObjectWithTag(tag2);
        }
        life = new LifeManagament(health);
        maxHealth = life.lifeAmount;
        if (hs != null)
        {
            hs.healthChange(health, maxHealth);
        }
        type = new Type { type = tType };
        GameObject GPlayer = GameObject.FindGameObjectWithTag("Player");
       player = GPlayer.GetComponent<PlayerManagament>();
        life.isPLayer = false;
         renderer = this.gameObject.GetComponent<Renderer>();
       
        hs.bar.enabled = false;
        hs.bar2.enabled = false;
        player.OnDeath += OnPlayerDeath;
        if (isBoss)
        {
            boxc.enabled = false;
        }
    }
    public void Start()
    {
        scoreScreen = GameObject.FindGameObjectWithTag("ScoreScreen").GetComponent<ScoreScreen>();
        if (!linked)
        {
            AiPath = this.GetComponent<AIPath>();
            seeker = this.GetComponent<Seeker>();
            GraphNode node = AstarPath.active.GetNearest(this.transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)), NNConstraint.Default).node;
            Vector3 v3 = (Vector3)node.position;
            seeker.StartPath(this.transform.position, v3, CalculatePath);
            Path p = seeker.StartPath(this.transform.position, v3);
            CalculatePath(p);

        }
        

    }
   
    void CalculateNewPath()
    {
        if (seeker.IsDone())
        {
            GraphNode node = AstarPath.active.GetNearest(this.transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)), NNConstraint.Default).node;
            Vector3 v3 = (Vector3)node.position;
            seeker.StartPath(this.transform.position, v3, CalculatePath);
            Path p = seeker.StartPath(this.transform.position, v3);
            CalculatePath(p);
        }
    }
    void CalculatePath(Path p)
    {
        if (!p.error)
        {

            currentWayPoint = 0;
            path = p;
        }
       
    }

        public void OnHit()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "ProjectilePlayer")
        {
            projectileManager projecile = other.gameObject.GetComponent<projectileManager>();
            if (!projecile.isBeam && !projecile.animated)
            {

                
                Destroy(other.gameObject);

            }
            life.reduceLife(projecile.getDamage()) ;
            if(hs != null)
            {
                hs.healthChange(life.lifeAmount, maxHealth);
            }
        }
    }
    private void Update()
    {
      

        

        if (life.lifeAmount <= 0)
        {
           GameObject b = Instantiate(PS);
            b.transform.position = this.transform.position;
            OnDeath.Invoke(true);
            Destroy(this.gameObject);
            Destroy(b, 5f);
            if (audio != null)
            {
                audio.Stop();
            }
            SpawnItemInWorld();
            
            if (isBoss)
            {
                boxc.enabled = true;
                Destroy(obstacle);
            }
        }
        if (!disabled)
        {
            if (is_active)
            {

                if (cooldown >= cooldownMax)
                {
                    if (projectile != null)
                    {
                        target = player.transform.position;
                        difference = target - this.transform.position;
                        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                        float distance = difference.magnitude;
                        Vector2 direction = difference / distance;
                        direction.Normalize();

                        StartCoroutine(LaunchProjectile(direction, rotationZ, bulletAmount, spreecooldown));

                        cooldown = 0;
                    }
                }
                if (cooldownDanmaku >= cooldownDanmakuMax)
                {
                    if (bomb != null)
                    {

                        StartCoroutine(LaunchBomb(bomb));

                        cooldownDanmaku = 0;
                    }
                }
            }
        }
        
    }
    private void FixedUpdate()
    {
        
        if (!disabled)
        {
            if (!linked)
            {
                if (currentWayPoint >= path.vectorPath.Count)
                {
                    CalculateNewPath();
                }
                if (path.vectorPath.Count > 0)
                {
                    Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - this.GetComponent<Rigidbody2D>().position).normalized;
                    Vector2 force = direction * UnityEngine.Random.Range(speed, speed * UnityEngine.Random.Range(1f, 3f)) * Time.deltaTime;
                    this.GetComponent<Rigidbody2D>().AddForce(force);
                    float distance = Vector2.Distance(this.GetComponent<Rigidbody2D>().position, path.vectorPath[currentWayPoint]);

                    if (distance < nextWaypointDistance)
                    {
                        currentWayPoint++;
                    }
                   

                }
               
            }
            cooldown++;
            cooldownDanmaku++;
        }
    }
    private void OnBecameInvisible()
    {
        sr.sortingLayerName = "base";
        sr.sortingOrder = -100;
        cc2d.enabled = false;

        foreach(Behaviour component in components)
        {
            component.enabled = false;
            
        }
        
        hs.bar.enabled = false;
        hs.bar2.enabled = false;
        is_active = false;
        if (audio != null)
        {
            if (!audio.isPlaying)
            {
                audio.Pause();
            }
        }
    }
    private void OnBecameVisible()
  {
        

        if (currentScene == SceneManager.GetActiveScene().name)
        {
            sr.sortingLayerName = "default";
            cc2d.enabled = true;

            foreach (Behaviour component in components)
            {
                component.enabled = true;

            }
            
            hs.bar.enabled = true;
            hs.bar2.enabled = true;
            is_active = true;
            if (audio != null)
            {
                if (!audio.isPlaying)
                {
                    audio.UnPause();
                }
            }
        }
    }
    private void randomDrop()
    {
        int randInt = UnityEngine.Random.Range(0, 1);

        switch (randInt)
        {
            default:
            case  0:  item = new Item(); break;
            case 1:  item = new Item(); break;
        }
    }
    private IEnumerator LaunchBomb(GameObject projectile)
    {
        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.position = this.GetComponent<Transform>().position;
        yield return 0;
    }
    private IEnumerator LaunchProjectile(Vector2 direction, float rotationZ, int amount, float spreeCoolDown)
    {
        

        for (int i = 0; i <= amount; i++)
        {
            this.GetComponent<AudioSource>().Play();
            GameObject b = Instantiate(projectile) as GameObject;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
            b.transform.position = this.GetComponent<Transform>().position;
          
            yield return new WaitForSeconds(spreeCoolDown);
        }
        
    }
      
    
    public void SpawnItemInWorld()
    {
        loot = Instantiate(loot) as GameObject;
        loot.transform.position = this.transform.position;
        lootManager lootm = loot.GetComponent<lootManager>();
        lootm.amount = amountLoot;
    }
    private void OnPlayerDeath(object sender, System.EventArgs e)
    {
        audio.Pause();
    }
    private void OnDestroy()
    {
        if (player.GetLife().lifeAmount < player.GetLife().maxLife)
        {

            player.GetLife().addLife(1);
            
        }
        scoreScreen.addScore(100);
        GlobalControl.Instance.UI_Exp.GetComponent<expSystem>().AddXp(XPgain);
    }
    [Serializable]
    public class ValueChangedEvent : UnityEvent<bool> { }
    [FormerlySerializedAs("onValueChange")]
    [SerializeField]
    private ValueChangedEvent OnDeath = new ValueChangedEvent();

    public ValueChangedEvent onDeath
    {
        get { return OnDeath; }
        set { OnDeath = value; }
    }
}
