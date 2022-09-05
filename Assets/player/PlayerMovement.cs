using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private CapsuleCollider2D cc2;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;
    public bool restrict = false;
    public InitiateFight initiateFight;
    public bool fightCalculated = false;
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
   
    // Update is called once per frame
    GameObject gm;
    private void Start()
    {
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
            this.gameObject.GetComponent<TouhouAdvenuturePlayer>().rb = rb;
            reseted = false;
            
        }
        gm = GameObject.Find("MainMenu");

        if (gm != null)
        {
            canfin = true;
                }
        initiateFight = FindObjectOfType<InitiateFight>();
    }


    //different from update, is dependant from the system not the game update rate
    void FixedUpdate()
    {
        if (canfin)
        {
            if(this.transform.position== new Vector3(0, 0,0))
            {
                this.transform.position = gm.transform.position;
            }
        } 
        if (IsLocalPlayer)
       {
           //movement 
            if (!audio.isPlaying)
            {
                audio.Play();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 1.5f;
                cc2.size = new Vector2(0.4f, 0.5f);

            }
            else
            {
                speed = 1f;
                cc2.size = new Vector2(0.4f, 0.5f);
            }
         anim.SetFloat("speed", speed*1.5f);
         if (movement.y > 0)
         {
             anim.SetBool("IsRunning", false);
             anim.SetBool("IsRunningLeft", false);
             anim.SetBool("IsRunningRight", false);
             anim.SetBool("IsRunningUp", true);
             reseted = false;
             alreadyPlaying = true;
             culle.sortingorderNumber[0] = 23;
         }
         if (movement.x < 0)
         {
             reseted = false;
             anim.SetBool("IsRunningUp", false);
             anim.SetBool("IsRunning", false);
             anim.SetBool("IsRunningRight", false);
             anim.SetBool("IsRunningLeft", true);

             alreadyPlaying = true;


         }
         if (movement.x > 0)
         {
             reseted = false;
             anim.SetBool("IsRunningUp", false);
             anim.SetBool("IsRunningLeft", false);
             anim.SetBool("IsRunning", false);

             anim.SetBool("IsRunningRight", true);

             alreadyPlaying = true;


         }
         if (movement.y < 0)
         {
             reseted = false;
             anim.SetBool("IsRunningUp", false);
             anim.SetBool("IsRunningLeft", false);
             anim.SetBool("IsRunningRight", false);

             anim.SetBool("IsRunning", true);

             culle.sortingorderNumber[0] = -5;
             alreadyPlaying = true;
         }
            if (!restrict)
            {
                
                //this.gameObject.GetComponent<TouhouAdvenuturePlayer>().Move();

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
            if (movement.x == 0 && movement.y == 0)
            {
                anim.SetBool("IsRunningUp", false);
                anim.SetBool("IsRunning", false);
                anim.SetBool("IsRunningRight", false);
                anim.SetBool("IsRunningLeft", false);
                if (!reseted)
                {
                    reseted = true;
                    anim.Play("I", 0, 0f);
                    anim.Play("H" + dataStatic.Instance.Indexes[1] + "I", 1, 0f);
                    anim.Play("M" + dataStatic.Instance.Indexes[5] + "I", 2, 0f);
                    anim.Play("EB" + dataStatic.Instance.Indexes[4] + "I", 3, 0f);
                }
                if (culle != null)
                {
                    culle.sortingorderNumber[0] = -5;
                }
                audio.Stop();
                alreadyPlaying = false;
            }
        }

    }
    void Update()
    {
        if (IsLocalPlayer)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            sizeC = cc2.size;
            sizeC2 = new Vector2(0.01f, 0.014f);


            if (!restrict) { 
           




            }
        }
      
    }
}
