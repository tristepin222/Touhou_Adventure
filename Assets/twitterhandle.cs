using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class twitterhandle : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    // Start is called before the first frame update
    public void open(string url)
    {
        Application.OpenURL(url);
    }
}
