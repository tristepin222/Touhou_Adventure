using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Pathfinding;
using UnityEngine.Audio;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private CapsuleCollider2D cc2;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private AudioMixerGroup pitchBendGroup;
    public bool restrict = false;
    public InitiateFight initiateFight;
    public bool fightCalculated = false;
    public onAwake onAwake;
    public AIPath AIPath;
    private const float MOVESPEED = 7.5f;
    private KeyCode shift = KeyCode.LeftShift;
    private Vector2 movement;
    private Vector2 sizeC;
    private Vector2 sizeC2;
    private Animator anim;
    private SpriteRenderer[] sprites;
    private SpriteRenderer sp;
    private SpriteRenderer rp;
    private culler culle;
    private bool alreadyPlaying = false;
    private bool canfin;
    private bool reseted;
    int step = 0;
    const int STEPAMOUNT = 100;
    // Update is called once per frame
    GameObject gm;
    private void Start()
    {
     

        onAwake = GameObject.FindObjectOfType<onAwake>();
        gm = GameObject.Find("MainMenu");
        if (!onAwake.SetAIPlayer)
        {
            this.GetComponent<AIPath>().enabled = false;
            this.GetComponent<AIDestinationSetter>().enabled = false;
            this.GetComponent<AIMovement>().enabled = false;
        }
        else
        {
            this.GetComponent<AIPath>().enabled = true;
            this.GetComponent<AIDestinationSetter>().enabled = true;
            this.GetComponent<AIMovement>().enabled = true;
        }
        if (IsLocalPlayer)
        {
            if (dataStatic.Instance != null)
            {
                this.transform.position = dataStatic.Instance.position;
            }
            culle = this.gameObject.GetComponent<culler>();
            rp = GameObject.Find("Hair").GetComponent<SpriteRenderer>();
            sp = this.gameObject.GetComponent<SpriteRenderer>();
            sprites = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
            anim = this.GetComponent<Animator>();
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsRunningUp", false);
            anim.SetInteger("Hair", dataStatic.Instance.Indexes[1]);
            anim.SetInteger("Mouth", dataStatic.Instance.Indexes[5]);
            anim.SetInteger("EyesBrows", dataStatic.Instance.Indexes[4]);
            anim.SetInteger("Shirt", dataStatic.Instance.Indexes[2]);
            this.gameObject.GetComponent<TouhouAdvenuturePlayer>().rb = rb;
            reseted = false;
            Debug.Log(anim.GetInteger("Hair"));
        }


        if (gm != null)
        {
            this.transform.position = gm.transform.position;
        }
        Debug.Log(this.transform.position);
        initiateFight = FindObjectOfType<InitiateFight>();
    }


    //different from update, is dependant from the system not the game update rate
    void FixedUpdate()
    {
        anim.SetFloat("VelocityX", movement.x);
        anim.SetFloat("VelocityY", movement.y);


        if (IsLocalPlayer)
        {
            //movement 
            anim.SetBool("IsRunning", true);
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0.6f;
                cc2.size = new Vector2(0.4f, 0.5f);
                anim.SetBool("IsRunning", false);
            }
            else
            {

                speed = 1.2f;
                cc2.size = new Vector2(0.4f, 0.5f);
            }
            
            if (movement.y > 0)
            {

                reseted = false;
                alreadyPlaying = true;
                culle.sortingorderNumber[0] = 23;
                step++;
                if (step >= STEPAMOUNT)
                {
                   
                    GlobalControl.Instance.steps++;
                    step = 0;
                }
            }
            if (movement.x < 0)
            {
                reseted = false;


                alreadyPlaying = true;

                step++;
                if (step >= STEPAMOUNT)
                {
                   
                    GlobalControl.Instance.steps++;
                    step = 0;
                }
            }
            if (movement.x > 0)
            {
                reseted = false;


                alreadyPlaying = true;
                step++;
                if (step >= STEPAMOUNT)
                {
                  
                    GlobalControl.Instance.steps++;
                    step = 0;
                }

            }
            if (movement.y < 0)
            {
                reseted = false;

                culle.sortingorderNumber[0] = +5;
                alreadyPlaying = true;
                step++;
                if (step >= STEPAMOUNT)
                {
                  
                    GlobalControl.Instance.steps++;
                    step = 0;
                }
            }
            if (!restrict)
            {

                //this.gameObject.GetComponent<TouhouAdvenuturePlayer>().Move();
                if (!onAwake.SetAIPlayer)
                {


                    rb.MovePosition(rb.position + movement * MOVESPEED * speed * Time.fixedDeltaTime);

                    if (initiateFight != null)
                    {
                        if (!fightCalculated)
                        {
                            fightCalculated = true;
                            StartCoroutine(initiateFight.CalculateChance());
                        }

                    }
                }
            }
            if (movement.x == 0 && movement.y == 0)
            {
                anim.speed = 1;
                if (!reseted)
                {
                    reseted = true;
                    anim.Rebind();
                    anim.Update(0f);
                    anim.Play("I", -1, 0f);
                    anim.Play("EB0I", 2, 0f);
                    anim.Play("H0I", 0, 0f);
                    anim.Play("S0I", 3, 0f);
                }
                if (culle != null)
                {
                    if (culle.sortingorderNumber != null)
                    {
                        culle.sortingorderNumber[0] = +5;
                    }
                }
                audio.Stop();
                alreadyPlaying = false;
            }
            else
            {
                if (anim.GetBool("IsRunning"))
                {
                    anim.speed = speed * 4f;
                    audio.pitch = 2f;

                    pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 2f);
                }
                else
                {
                    anim.speed = speed * 2.5f;
                    audio.pitch = 0.8f;

                    pitchBendGroup.audioMixer.SetFloat("pitchBend", 1f / 0.8f);
                }
            }
        }


    }
    void Update()
    {
        if (IsLocalPlayer)
        {
            if (!onAwake.SetAIPlayer)
            {
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                Vector2 vector2 = new Vector2(AIPath.velocity.x, AIPath.velocity.y);
                movement.x = vector2.normalized.x;
                movement.y = vector2.normalized.y;
            }

            sizeC = cc2.size;
            sizeC2 = new Vector2(0.01f, 0.014f);


            if (!restrict)
            {





            }
        }

    }
}
