using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
public class interact : MonoBehaviour
{
    public actions action;
    public UnityEvent m_onKeyDown;
    public GameObject Interact;
    public reactionManager reactionManager;
    public int index;
    public bool trigger = false;
    public GameObject toshow;
    public Objective objective;
    Canvas interactC;
    bool inZone;

    // Start is called before the first frame update
    void Start()
    {

        if (GlobalControl.Instance != null)
        {
            Interact = GlobalControl.Instance.ui_interact;
          

        }
        m_onKeyDown.AddListener(onKeyDown);
    }
    
    private void onKeyDown()
    {
        if (GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().Objectives.Contains(objective))
        {
            if (GlobalControl.Instance.money > 0)
            {
                GlobalControl.Instance.money--;
                GlobalControl.Instance.moneyt.text = GlobalControl.Instance.money.ToString();
            }
            switch (action.action)
            {
                case actions.actionType.UnblockForWaitingObjective:
                    trigger = true;
                    switch (index)
                    {
                        case 0:
                            reactionManager.block1 = false;
                            break;
                        case 1:
                            reactionManager.block2 = false;
                            break;
                        case 2:
                            reactionManager.block3 = false;
                            break;
                    }
                    break;
            }
            GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().Remove(objective);
            if (trigger)
            {
                trigger = false;
                toshow.GetComponent<reactionManager>().initiated = true;
                if (toshow.activeSelf)
                {

                }
                else
                {
                    toshow.SetActive(true);

                }

                switch (index)
                {
                    case 0:
                        reactionManager.option1();
                        break;
                    case 1:
                        reactionManager.option2();
                        break;
                    case 2:
                        reactionManager.option3();
                        break;
                }

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactC = Interact.GetComponent<Canvas>();
            interactC.enabled = false;
            inZone = true;
            interactC.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inZone = false;
            
            interactC.enabled = false;
        }
    }
}
