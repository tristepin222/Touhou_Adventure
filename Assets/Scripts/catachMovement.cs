using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catachMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject b;
    public bool hasfish = false;
    // Start is called before the first frame update
    void Start()
    {
        b = GameObject.FindGameObjectWithTag("catch");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
           
            rb.mass = 0.03f;
        }
        else
        {
            rb.mass = 0.25f;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "top" && hasfish)
        {
            Destroy(b);
        }
    }
}
