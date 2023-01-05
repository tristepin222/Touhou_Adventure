using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
public class NPCMovement : MonoBehaviour
{
    public bool disabled;
    public PointManager pointManager;
    public AIDestinationSetter aIDestinationSetter;
    public AIPath AIPath;
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public GameObject target;
    public List<GameObject> effects = new List<GameObject>();
    public GameObject dialogue;
    public bool loop;
    public int id;
    public TimeTable timeTable;
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    public List<Collider2D> colliders = new List<Collider2D>();
    public List<SpriteRenderer> otherSprites = new List<SpriteRenderer>();
    public List<Collider2D> otherColliders = new List<Collider2D>();
    public bool spawn;
    private bool AlreadyPlaying;
    private int i = 0;
    private day currentTimeTable;
    private int check;
    // Start is called before the first frame update
   
   public void startMovement()
    {
        rigidbody2D.constraints = RigidbodyConstraints2D.None;
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        aIDestinationSetter.target = pointManager.points[0].transform;
    }
    public void Start()
    {
        if (GlobalControl.Instance != null)
        {
           

                init();
           

        }
    }
   public void init()
    {
        if (!disabled)
        {

            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

            if (timeTable.timeTable.Count != 0)
            {
                currentTimeTable = timeTable.timeTable[GlobalControl.Instance.currentDay];
                int a = 0;
                foreach (hours hour in currentTimeTable.hours)
                {
                    pointManager = hour.pointlist;
                    if (GlobalControl.Instance.hour >= hour.hour)
                    {

                        foreach (SpriteRenderer sp in sprites)
                        {
                            sp.enabled = true;
                        }
                        foreach (Collider2D collider in colliders)
                        {
                            collider.enabled = true;
                        }
                        foreach (SpriteRenderer sp in otherSprites)
                        {
                            sp.enabled = true;
                        }
                        foreach (Collider2D collider in otherColliders)
                        {
                            collider.enabled = true;
                        }
                        if (hour.leaving)
                        {
                            hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.Dissapear;
                        }
                        if (hour.action.action == actions.actionType.Dissapear)
                        {
                            hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.Dissapear;
                        }
                        if (hour.action.action == actions.actionType.freezeAll)
                        {
                            hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.freezeAll;
                        }
                        if (hour.action.action == actions.actionType.Apprear)
                        {
                            hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.Apprear;
                        }
                        hour.completed = true;
                        startMovement();
                    }
                    if (GlobalControl.Instance.hour >= hour.hour + 1)
                    {
                        if (hour.endPoint != null)
                        {
                            this.transform.position = hour.endPoint.position;
                        }
                        if (hour.leaving)
                        {
                            foreach (SpriteRenderer sp in sprites)
                            {
                                sp.enabled = false;
                            }
                            foreach (Collider2D collider in colliders)
                            {
                                collider.enabled = false;
                            }
                            foreach (SpriteRenderer sp in otherSprites)
                            {
                                sp.enabled = false;
                            }
                            foreach (Collider2D collider in otherColliders)
                            {
                                collider.enabled = false;
                            }
                        }
                        else
                        {
                            foreach (SpriteRenderer sp in sprites)
                            {
                                sp.enabled = true;
                            }
                            foreach (Collider2D collider in colliders)
                            {
                                collider.enabled = true;
                            }
                            foreach (SpriteRenderer sp in otherSprites)
                            {
                                sp.enabled = true;
                            }
                            foreach (Collider2D collider in otherColliders)
                            {
                                collider.enabled = true;
                            }
                        }
                    }
                    else if (GlobalControl.Instance.hour < hour.hour)
                    {
                        if (hour.startpoint != null)
                        {
                            this.transform.position = hour.startpoint.position;
                        }
                        if (hour.joining)
                        {
                            foreach (SpriteRenderer sp in sprites)
                            {
                                sp.enabled = false;
                            }
                            foreach (Collider2D collider in colliders)
                            {
                                collider.enabled = false;
                            }
                            foreach (SpriteRenderer sp in otherSprites)
                            {
                                if (otherSprites != null)
                                {
                                    sp.enabled = false;
                                }
                            }
                            foreach (Collider2D collider in otherColliders)
                            {
                                collider.enabled = false;
                            }
                        }
                    }

                    a++;
                }
            }
        }
    }
    void Update()
    {
        if (!disabled)
        {
            animator.SetFloat("VelocityX", AIPath.velocity.x);
            animator.SetFloat("VelocityY", AIPath.velocity.y);

            if (GlobalControl.Instance != null)
            {

                if (timeTable.timeTable.Count != 0)
                {

                    currentTimeTable = timeTable.timeTable[GlobalControl.Instance.currentDay];
                    int a = 0;
                    foreach (hours hour in currentTimeTable.hours)
                    {
                        pointManager = hour.pointlist;
                        if (GlobalControl.Instance.hour >= hour.hour)
                        {

                            if (!hour.completed)
                            {
                                if (spawn)
                                {

                                    this.gameObject.transform.position = hour.startpoint.position;
                                }
                                if (hour.joining)
                                {
                                    foreach (SpriteRenderer sp in sprites)
                                    {
                                        sp.enabled = true;
                                    }
                                    foreach (Collider2D collider in colliders)
                                    {
                                        collider.enabled = true;
                                    }

                                }
                                if (hour.leaving)
                                {
                                    hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.Dissapear;
                                }
                                if (hour.action.action == actions.actionType.Dissapear)
                                {
                                    hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.Dissapear;
                                }
                                if (hour.action.action == actions.actionType.freezeAll)
                                {
                                    hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.freezeAll;
                                }
                                if (hour.action.action == actions.actionType.Apprear)
                                {
                                    hour.pointlist.points[hour.pointlist.points.Count - 1].GetComponent<point>().action[0].action = actions.actionType.Apprear;
                                }

                                hour.completed = true;
                                startMovement();
                            }
                        }

                        a++;
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "point")
        {
            if (collision.GetComponent<point>().i == id)
            {
              
                
                i++;
                point point = collision.GetComponent<point>();
                foreach (actions action in point.action)
                {
                    switch (action.action)
                    {
                        case actions.actionType.PlayAnimation:
                            StartCoroutine(PlayAnim());
                            break;
                        case actions.actionType.ShowDialogue:
                            dialogue.SetActive(true);
                            break;
                            case actions.actionType.Dissapear:

                            action.action = actions.actionType.None;
                            foreach (SpriteRenderer sp in sprites)
                            {
                                sp.enabled = false;
                            }
                            foreach (Collider2D collider in colliders)
                            {
                                collider.enabled = false;
                            }
                            foreach (SpriteRenderer sp in otherSprites)
                            {
                                sp.enabled = false;
                            }
                            foreach (Collider2D collider in otherColliders)
                            {
                                collider.enabled = false;
                            }

                            break;

                        case actions.actionType.freezeAll:
                            action.action = actions.actionType.None;
                            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                            break;
                        case actions.actionType.Apprear:
                            foreach (SpriteRenderer sp in otherSprites)
                            {
                                sp.enabled = true;
                            }
                            foreach (Collider2D collider in otherColliders)
                            {
                                collider.enabled = true;
                            }
                            break;
                        case actions.actionType.None:
                            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
                            break;
                    }
                }
                if (i <= pointManager.points.Count-1)
                {
                    aIDestinationSetter.target = pointManager.points[i].transform;
                }
                else if (loop)
                {
                    i = 0;
                    aIDestinationSetter.target = pointManager.points[i].transform;
                }
                
            }
        }

    }
    private IEnumerator PlayAnim()
    {
        if (!AlreadyPlaying)
        {
            AlreadyPlaying = true;
            animator.Play("KJUMP");
            yield return new WaitForSeconds(1f);
         GameObject b  =  Instantiate(effects[0], this.transform);
            yield return new WaitForSeconds(1f);
            GameObject a =  Instantiate(effects[1], target.transform);
            yield return new WaitForSeconds(2f);
            Destroy(b);
            Destroy(a);
            Destroy(target);
        }
        animator.Play("", -1, 0f);
    }
}
