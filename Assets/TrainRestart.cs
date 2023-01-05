using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainRestart : MonoBehaviour
{
    public Transform startingPoint;
    public string scene;
    public loadingScreen loadingScreen;
    private int i = 0;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "train")
        {
            collision.transform.position = startingPoint.position;
            i++;
            if(i >= 3)
            {
                loadingScreen.sceneString = scene;
                loadingScreen.startLoading();
            }
        }

    }
}
