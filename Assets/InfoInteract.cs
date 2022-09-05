using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
public class InfoInteract : MonoBehaviour
{
    
    public GameObject b;
    GameObject b2;
    public string text;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            b2 = Instantiate(b);
            Canvas c = b2.GetComponent<Canvas>();
            c.worldCamera = Camera.main;
            //Text t = b2.GetComponentInChildren<Text>();
           // t.text = text;

            
            b2.transform.position = this.transform.position;
           
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(b2);
        }
    }
}
