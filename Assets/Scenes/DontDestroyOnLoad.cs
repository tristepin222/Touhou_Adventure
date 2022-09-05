using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject[] gameObjects2;
   
    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    public void updateDestroy()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Dynamic");
        foreach (GameObject gameobject in gameObjects)
        {
            gameobject.tag = "Dynamic2";
            objectInfo obji = gameobject.GetComponent<objectInfo>();
            obji.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(gameobject);
        }

        gameObjects2 = GameObject.FindGameObjectsWithTag("Dynamic");


        foreach (GameObject gameobject in gameObjects2)
        {

            Destroy(gameobject);
        }

    }
    void Start()
    {
        init();
    }

    public void init()
    {
        SpriteRenderer[] sp2 = new SpriteRenderer[2];
        if (GlobalControl.Instance != null)
        {
            if (!GlobalControl.Instance.once[SceneManager.GetActiveScene().buildIndex])
            {

                updateDestroy();
                GlobalControl.Instance.once[SceneManager.GetActiveScene().buildIndex] = true;
            }
            else
            {
                gameObjects2 = GameObject.FindGameObjectsWithTag("Dynamic");


                foreach (GameObject gameobject in gameObjects2)
                {

                    Destroy(gameobject);
                }
            }

            gameObjects = GameObject.FindGameObjectsWithTag("Dynamic2");

            foreach (GameObject gameobject in gameObjects)
            {
                plant plant;
                bool found = gameobject.TryGetComponent(out plant);
                objectInfo obji = gameobject.GetComponent<objectInfo>();
                if (obji.sceneIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    SpriteRenderer sp = gameobject.GetComponent<SpriteRenderer>();
                    Collider2D cd = gameobject.GetComponent<Collider2D>();
                    if (sp != null && cd != null)
                    {
                        gameobject.GetComponent<SpriteRenderer>().enabled = false;
                        gameobject.GetComponent<Collider2D>().enabled = false;
                        if (gameobject.transform.childCount > 0)
                        {
                            for (int i = 0; i < gameobject.transform.childCount; i++)
                            {
                                sp2[i] = gameobject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                            }
                            if (sp2 != null)
                            {
                                foreach (SpriteRenderer spr in sp2)
                                {

                                    if (spr != null)
                                    {


                                        spr.enabled = false;

                                    }
                                }
                            }
                            Collider2D cd2 = gameobject.transform.GetChild(0).GetComponent<Collider2D>();
                            if (cd2 != null)
                            {
                                cd2.enabled = false;
                            }
                        }
                    }


                }
                else
                {
                    SpriteRenderer sp = gameobject.GetComponent<SpriteRenderer>();
                    Collider2D cd = gameobject.GetComponent<Collider2D>();
                    if (sp != null && cd != null)
                    {
                        gameobject.GetComponent<SpriteRenderer>().enabled = true;
                        gameobject.GetComponent<Collider2D>().enabled = true;
                        if (found)
                        {
                            if (plant.disabled)
                            {
                                gameobject.GetComponent<SpriteRenderer>().enabled = false;
                                gameobject.GetComponent<Collider2D>().enabled = false;
                            }
                        }
                        if (gameobject.transform.childCount > 0)
                        {
                            for (int i = 0; i < gameobject.transform.childCount; i++)
                            {
                                sp2[i] = gameobject.transform.GetChild(i).GetComponent<SpriteRenderer>();
                            }
                            Collider2D cd2 = gameobject.transform.GetChild(0).GetComponent<Collider2D>();
                            if (sp2 != null)
                            {
                                foreach (SpriteRenderer spr in sp2)
                                {
                                    if (spr != null)
                                    {
                                        spr.enabled = true;

                                    }
                                }
                            }
                            if (cd2 != null)
                            {
                                gameobject.transform.GetChild(0).GetComponent<Collider2D>().enabled = true;

                            }
                        }
                    }
                }
            }
        }
    }
}
