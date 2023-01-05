using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class test : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("SHOW");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("DEL");
    }
}
