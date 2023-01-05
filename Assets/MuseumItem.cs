using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MuseumItem : MonoBehaviour
{

    public ItemScriptableObject itemScriptableObject;
    public GameObject objectToShow;
    public int index;
    public int category = 0;
    public GameObject b;
    private GameObject b2;
    private bool on;
  
    public void SetActive()
    {
        on = true;
        objectToShow.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            b2 = Instantiate(b);
            Canvas c = b.GetComponent<Canvas>();
            c.worldCamera = Camera.main;
            //Text t = b2.GetComponentInChildren<Text>();
            // t.text = text;


            b2.transform.position = this.transform.position + new Vector3(0, 2);

            b2.GetComponentInChildren<TextMeshProUGUI>().text = "Item : " + itemScriptableObject.itemName + "\n";
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
