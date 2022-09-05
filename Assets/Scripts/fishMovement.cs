using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class fishMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;
    SpriteRenderer sp;
    public bool catched = false;
    Collider2D colliderS;
    public GameObject pointC;
    public GameObject pointC2;
    public catachMovement fm;
    // Start is called before the first frame update
    void Start()
    {
      
        rb =this.GetComponent<Rigidbody2D>();
      
        tr = this.GetComponent<Transform>();
        sp = this.GetComponent<SpriteRenderer>();

        fm = FindObjectOfType<catachMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.flipX)
        {
            rb.velocity = new Vector2(-1f, 0f);
        }
        else
        {
            rb.velocity = new Vector2(1f, 0f);
        }

        if (catched)
        {
            this.gameObject.transform.position =pointC.transform.position;
        }
        }
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "border")
        {
            if (sp.flipX)
            {
                sp.flipX = false;
            }
            else
            {
                sp.flipX = true;
            }
        }
        if(collision.tag == "fish")
        {
            sp.flipX = !sp.flipX;
        }
        if(collision.tag == "bottom")
        {
            float rand = Random.Range(0.1f, 0.3f);
            rb.mass = rand;
        }
        if (collision.tag == "top")
        {
            float rand = Random.Range(1f, 1.3f);
            rb.mass = rand;
        }
        if(collision.tag == "catch" && catched == false && !fm.hasfish)
        {
            rb.simulated = false;
            catched = true;
            this.gameObject.transform.position = pointC.transform.position;
             colliderS = collision;
            fm.hasfish = catched;
        }
    }
}


