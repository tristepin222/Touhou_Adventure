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

public class BossManagerFightScene : MonoBehaviour
{

    public GameObject[] danmakus;
    public GameObject[] subspawns;
    public GameObject[] bombs;
    public Animator animator;
    public ScoreScreen scoreScreen;
    public SpriteRenderer sr;
    public string currentScene;
    public int maxHealth;
    public int health;
    public healthsystem hs;
    public AudioSource audio;
    public int stage;
    public int cooldownMax;
    public int cooldownDanmakuMax;
    public int bulletAmount;
    public bool is_active = false;
    public GameObject PS;
    public bool disabled;
    public int lives;
    private Vector3 target;
    private Vector3 difference;
    private int cooldown = 0;
    private int cooldownDanmaku = 0;
    private PlayerManagament player;
    private LifeManagament life;
    private Path path;
    private int currentWayPoint = 0;
    private float nextWaypointDistance = 3f;
    private bool onStage;
    [SerializeField] public int amountLoot = 1;
    [SerializeField] public GameObject loot;
    [SerializeField] private float XPgain;
    [SerializeField] private AIPath AiPath;
    [SerializeField] private Seeker seeker;
    [SerializeField] private float speed = 1;
    [SerializeField] private float spreecooldown = 0.1f;
    [SerializeField] private Color color;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        
        currentScene = SceneManager.GetActiveScene().name;
        scoreScreen = GameObject.FindGameObjectWithTag("ScoreScreen").GetComponent<ScoreScreen>();
        life = new LifeManagament(health);
        maxHealth = life.lifeAmount;
        if (hs != null)
        {
            hs.healthChange(health, maxHealth);
        }
      
        GameObject GPlayer = GameObject.FindGameObjectWithTag("Player");
        player = GPlayer.GetComponent<PlayerManagament>();
        life.isPLayer = false;
      

        player.OnDeath += OnPlayerDeath;
        AiPath = this.GetComponent<AIPath>();
        seeker = this.GetComponent<Seeker>();
        GraphNode node = AstarPath.active.GetNearest(this.transform.position + new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)), NNConstraint.Default).node;
        Vector3 v3 = (Vector3)node.position;
        seeker.StartPath(this.transform.position, v3, CalculatePath);
        Path p = seeker.StartPath(this.transform.position, v3);
        CalculatePath(p);
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
    // Update is called once per frame
    void Update()
    {


        if (life.lifeAmount <= 0)
        {

            GameObject b = Instantiate(PS);
            b.transform.position = this.transform.position;
            onStage = false;
            if (stage >= lives)
            {
                OnDeath.Invoke(true);
                Destroy(this.gameObject);
                Destroy(b, 5f);
                if (audio != null)
                {
                    audio.Stop();
                }
                SpawnItemInWorld();
            }
            else
            {
                stage++;
            }
        }
        if (!disabled)
        {
            

                if (cooldown >= cooldownMax)
                {
                    target = player.transform.position;
                    difference = target - this.transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

                    float distance = difference.magnitude;
                    Vector2 direction = difference / distance;
                    direction.Normalize();
                int random = UnityEngine.Random.Range(0, 1);
               
                switch (random)
                {
                    case 0:
                        animator.Play("LaunchProjectileLeft", -1, 0f);
                        break;
                    case 1:
                        animator.Play("LauchProjectileRight", -1, 0f);
                        break;
                }
               
                    StartCoroutine(LaunchProjectile(direction, rotationZ, bulletAmount, spreecooldown, danmakus[stage]));
                if (!onStage)
                {
                    onStage = true;
                    StartCoroutine(SpawnEnemy(subspawns[stage]));
                }
                    cooldown = 0;
                }
                if (cooldownDanmaku >= cooldownDanmakuMax)
                {
                    

                    StartCoroutine(LaunchBomb(bombs[stage]));

                    cooldownDanmaku = 0;
                }
            
        }
    }
    private void FixedUpdate()
    {
        if (!disabled)
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
                cooldown++;
                cooldownDanmaku++;

            }
        }
    }
    private IEnumerator LaunchBomb(GameObject projectile)
    {
        GameObject b = Instantiate(projectile) as GameObject;
        b.transform.position = this.GetComponent<Transform>().position;
        yield return 0;
    }

    private IEnumerator LaunchProjectile(Vector2 direction, float rotationZ, int amount, float spreeCoolDown, GameObject projectile)
    {


        for (int i = 0; i <= amount; i++)
        {
            this.GetComponent<AudioSource>().Play();
            GameObject b = Instantiate(projectile) as GameObject;
            b.transform.position = this.GetComponent<Transform>().position;
            b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
       
            yield return new WaitForSeconds(spreeCoolDown);
        }
       
    }
    private IEnumerator SpawnEnemy( GameObject enemy)
    {
        
        GameObject b = Instantiate(enemy) as GameObject;
        b.transform.position = this.transform.position;
       foreach(EnemiManagament enemiManagament in b.GetComponentsInChildren<EnemiManagament>())
        {
            enemiManagament.linked = true;
            enemiManagament.link = this.gameObject;
        }
      
        yield return 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ProjectilePlayer")
        {
            projectileManager projecile = other.gameObject.GetComponent<projectileManager>();
            if (!projecile.isBeam && !projecile.animated)
            {


                Destroy(other.gameObject);

            }
            life.reduceLife(projecile.getDamage());
            if (hs != null)
            {
                hs.healthChange(life.lifeAmount, maxHealth);
            }
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
