using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Pathfinding;
using UnityEngine.UI;
public class BossManager : MonoBehaviour
{
    public AudioClip[] audioclips;
    public AudioSource Audiosource;
    public List<point> points = new List<point>();
    [SerializeField] public List<GameObject> gameObjects = new List<GameObject>();
    public AIPath aipath;
    public AIDestinationSetter AIDS;
    public Sprite[] sprites;
    public Transform child;
    private SpriteRenderer sprite;
    float distanceToActivate = 8f;
    public Volume vol;
    float gain;
    LensDistortion ls;
    FilmGrain fg;
    int i = 1;
    int i2 = 0;
    int i3 = 0;
    bool play = false;
    public Collider2D cd;
    public Collider2D cd2;
    public Image image;
    public musicController MSC;
    // Start is called before the first frame update
    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
        this.gameObject.GetComponentInParent<AIDestinationSetter>().target = GlobalControl.Instance.player.transform;
        Audiosource.PlayOneShot(audioclips[0]);
        GlobalControl.Instance.player.GetComponent<Light2D>().enabled = true;
        GlobalControl.Instance.vc.GetComponent<Camera>().orthographicSize = 10;

         ls = (LensDistortion)vol.profile.components[2];
        fg = (FilmGrain)vol.profile.components[6];
         i2 = Random.Range(0, gameObjects.Count);
        i3 = i2;
        AIDS.target = gameObjects[i2].transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (aipath.velocity.x >= 2f) { 

            child.eulerAngles = new Vector3(0, 0, 90);
            sprite.sprite = sprites[0];
        }
        else if (aipath.velocity.y >= 2f)
        {
            child.eulerAngles = new Vector3(0, 0, 180);
            sprite.sprite = sprites[3];
        }
        else if (aipath.velocity.x <= -2f)
        {
            child.eulerAngles = new Vector3(0, 0, -90);
            sprite.sprite = sprites[1];
        }
       
        else if (aipath.velocity.y <= -2f)
        {
            child.eulerAngles = new Vector3(0, 0, 0);
            sprite.sprite = sprites[2];
        }
        float distance = Vector2.Distance(transform.position, GlobalControl.Instance.player.transform.position);

        if (distance < distanceToActivate)
        {
            if (!Audiosource.isPlaying && !play)
            {
                i = Random.Range(1, audioclips.Length);
                Audiosource.PlayOneShot(audioclips[i]);
                play = true;
            }
            vol.enabled = true;
            image.enabled = true;
             gain = distanceToActivate - distance;
          
            ls.intensity.value = gain/7f;
            fg.intensity.value = gain / 10f;
        }
        else
        {
            if (ls.intensity.value >= 0)
            {

                ls.intensity.value = gain * 7f;
                fg.intensity.value = gain * 10f;
            }
            else{
                ls.intensity.value = 0;
                fg.intensity.value = 0;
            }
            vol.enabled = false;
            play = false;
            image.enabled = false;
            ls.intensity.value = 0;
            fg.intensity.value = 0;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cd.IsTouching(collision))
        {
            if (collision.name == gameObjects[i3].name)
            {
                
                 i2 = Random.Range(0, gameObjects.Count);
               
                if(i2 == i3)
                {
                    if (i2 <= gameObjects.Count)
                    {
                        i2++;
                    }
                    else
                    {
                        i2--;
                    }
                }
                AIDS.target = gameObjects[i2].transform;
                i3 = i2;
            }
        }
        if (cd2.IsTouching(collision))
        {
            if (collision.tag == "Player")
            {

                MSC.fadein();
                AIDS.target = GlobalControl.Instance.player.transform;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            MSC.fadeout();
            i2 = Random.Range(0, gameObjects.Count);
            if (i2 <= gameObjects.Count)
            {
                i2++;
            }
            else
            {
                i2--;
            }
            AIDS.target = gameObjects[i2].transform;
            i3 = i2;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(GlobalControl.Instance.player.GetComponent<PlayerManagament>().killl(null));
            
        }
    }
}



