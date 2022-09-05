using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walksoundmanager : MonoBehaviour
{
    public AudioClip dirt;
    public AudioClip pavement;
    public AudioClip wood;
    public AudioClip water;
    public AudioClip sand;
    public AudioClip snow;
    public AudioClip metallic;
    public AudioClip rug;
    public AudioSource source;
  
    string tagbuff;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
     
       
            StartCoroutine(proccess(collision.tag));
        
        
    }
    IEnumerator proccess(string str)
    {

        if (str == "dirt") {


            if (tagbuff != str)
            {
                source.clip = dirt;
                source.Play();
               
                tagbuff = str;
            }
           
        }
        if (str == "pavement")
        {


            if (tagbuff != str)
            {
                source.clip = pavement;
                source.Play();
               
                tagbuff = str;
            }
           
            
        }
        if (str == "wood")
        {


            if (tagbuff != str)
            {
                source.clip = wood;
                source.Play();
               

                tagbuff = str;
            }
           
        }
        if (str == "sand")
        {


            if (tagbuff != str)
            {
                source.clip = sand;
                source.Play();
                
                tagbuff = str;
            }
            
            
        }
        if (str == "watery")
        {


            if (tagbuff != str)
            {
                source.clip = water;
                source.Play();
               
                tagbuff = str;
            }
            
            
        }
        if (str == "snow")
        {


            if (tagbuff != str)
            {
                source.clip = snow;
                source.Play();
                
                tagbuff = str;
            }


        }
        if (str == "metallic")
        {


            if (tagbuff != str)
            {
                source.clip = metallic;
                source.Play();

                tagbuff = str;
            }


        }
        if (str == "rug")
        {


            if (tagbuff != str)
            {
                source.clip = rug;
                source.Play();

                tagbuff = str;
            }


        }

        yield return new WaitForSeconds(0f);
     


    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
       //StartCoroutine(proccess(collision.tag));
    }
    void turnfasle(string str)
    {
    }
  
}
