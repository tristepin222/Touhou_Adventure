using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
public class bombCreator : MonoBehaviour
{
    [SerializeField] private List<RectTransform> itemslots = new List<RectTransform>();
    [SerializeField] public List<ItemScriptableObject> projectileItems = new List<ItemScriptableObject>();

    private int i;
    private void Start()
    {
        i = 0;
        foreach (RectTransform gameObject in itemslots)
        {
            Text textcontent = gameObject.Find("Text").GetComponent<Text>();
            Text amountContent = gameObject.Find("Amount").GetComponent<Text>();
            Image imageContent = gameObject.Find("Image").GetComponent<Image>();
           

            amountContent.enabled = false;
            textcontent.transform.GetComponent<dynamicLocale>().StringReference.TableEntryReference = projectileItems[i].name;
            imageContent.sprite = projectileItems[i].sprite;
            i++;
        }
    }

}
