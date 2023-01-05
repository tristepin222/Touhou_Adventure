using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleInteract : MonoBehaviour
{
    public Collider2D collider;
    public GameObject toshow;
    public bool inZone = false;
    public bool instantiate = false;
    public GameObject Interact;
    public bool giveSucces;
    public string success;
    Canvas interactC;
    SuccessManager successManager;

    // Start is called before the first frame update
    void Start()
    {
        if (instantiate)
        {
            toshow = Instantiate(toshow);
        }
        toshow.SetActive(false);
        successManager = FindObjectOfType<SuccessManager>();
        if(successManager == null)
        {
            giveSucces = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Interact == null)
        {
            if (GlobalControl.Instance != null)
            {
                Interact = GlobalControl.Instance.ui_interact;
                interactC = Interact.GetComponent<Canvas>();
                interactC.enabled = false;
            }
        }
        if (inZone){
            
            if (Input.GetKeyDown(KeyCode.E)) {
                if (toshow.activeSelf)
                {
                    toshow.SetActive(false);
                }
                else
                {
                    toshow.SetActive(true);
                    if (giveSucces)
                    {
                        successManager.SetAchievement(success);
                    }
                    
                }

            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interact = GlobalControl.Instance.ui_interact;
        interactC = Interact.GetComponent<Canvas>();
        if (collision.tag == "Player")
        {
            inZone = true;
            interactC.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inZone = false;
            toshow.SetActive(false);
            interactC.enabled = false;
        }
    }


}
