using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class plow : MonoBehaviour
{
    public GridFarming grid;
    private bool found;
    // Start is called before the first frame update
    void Start()
    {

        GameObject gameObject = GameObject.Find("grid");
        if(gameObject != null)
        {
            grid = gameObject.GetComponent<GridFarming>();
        }
    }


    private void Update()
    {

        if (found)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GlobalControl.Instance.inventory.getItem(GlobalControl.Instance.player.GetComponent<PlayerManagament>().selectedItem).item == Item.ItemType.Hoe)
                {
                    Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    grid.SetValue(v3, 1);
                }
            }
        }
        

    }

}
