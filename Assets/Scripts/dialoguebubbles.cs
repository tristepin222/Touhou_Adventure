using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dialoguebubbles : MonoBehaviour
{
    public string[] strings;
    public Text text;
    public int x = 0;
    public GameObject panel;
    void Start()
    {
        this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        this.gameObject.GetComponent<simplierlocal>().myString.TableEntryReference = strings[x];
        this.gameObject.GetComponent<simplierlocal>().RefreshString();
      
        this.gameObject.GetComponent<simplierlocal>().twe.startCoroutine(this.gameObject.GetComponent<simplierlocal>().myString.GetLocalizedString());
        x++;
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalControl.Instance != null)
        {
            this.transform.position = GlobalControl.Instance.player.transform.position + new Vector3(0, 2, 0);
            if (!panel.activeSelf)
            {
                panel.SetActive(true);
            }
        }
    }
    public void next()
    {
        if (x < strings.Length)
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponent<simplierlocal>().myString.TableEntryReference = strings[x];
            this.gameObject.GetComponent<simplierlocal>().RefreshString();
            x++;
           
        }
    }
}
