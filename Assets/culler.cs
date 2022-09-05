using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class culler : MonoBehaviour
{
    int nu = 1;
    string name = "";
    int i = 0;
    int a = 0;
    SpriteRenderer spriteRenderer;
   public SpriteRenderer[] bodyparts;
   public Animator animator;
    public List<int> sortingorderNumber = new List<int>();
    SpriteRenderer[] renderers;
    public bool culled = false;
    private void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        foreach (SpriteRenderer bodypart in bodyparts)
        {
            sortingorderNumber.Add(bodypart.sortingOrder);
        }
        if (!culled)
        {
            
            renderers = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer render in renderers)
            {
                if (render.sortingLayerName == "Default" && (render.tag != "body" && render.tag != "Player"))
                {
                    nu = (int)(render.transform.position.y * -100);
                    render.sortingOrder += nu;
                }
            }
            culled = true;
        }
    }
    public void cull()
    {
        if (!culled)
        {
            renderers = FindObjectsOfType<SpriteRenderer>();
            foreach (SpriteRenderer render in renderers)
            {
                if (render.sortingLayerName == "Default" && (render.tag != "body" && render.tag != "Player"))
                {
                    nu = (int)(render.transform.position.y * -100);
                    render.sortingOrder += nu;
                }
            }
            culled = true;
        }
    }
    void LateUpdate()
    {


        foreach (SpriteRenderer render in bodyparts)
        {
            if (render != null)
            {
                if (render.sortingLayerName == "Default")
                {

                    if (render.gameObject.tag == "body" || render.gameObject.tag == "Player")
                    {

                        switch (render.gameObject.name)
                        {
                            case "Hair":
                                nu = (int)(this.transform.position.y * -100);
                                if (animator.GetBool("IsRunningUp") == true || animator.GetBool("IsRunningLeft") == true || animator.GetBool("IsRunningRight") == true)
                                {
                                    nu += 20;
                                }
                                
                                

                                nu += sortingorderNumber[0];
                                break;
                            case "Eyes":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[1];
                                break;
                            case "Shirt":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[2];
                                break;
                            case "Pants":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[3];
                                break;
                            case "Shoes":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[4];
                                break;
                            case "EyeBrows":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[5];
                                break;
                            case "Bows":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[6];
                                break;
                            case "Mouth":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[7];
                                break;
                            case "Hands":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[8];
                                break;
                            case "Feet":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[9];
                                break;
                            case "Player":
                                nu = (int)(this.transform.position.y * -100);
                                nu += sortingorderNumber[10];
                                break;
                        }







                        render.sortingOrder = nu;
                    }
                    else
                    {



                    }



                }
            }

        }
    }
}

