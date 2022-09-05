using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
public class HotKeySystem 
{
    public event EventHandler OnSwap;
    public event EventHandler OnChange;
    private PlayerManagament player;
    public UI_Inventory uiInv;
    public int selectedItem;
    public int[] sitems;
    public HotKeySystem(PlayerManagament player, UI_Inventory uiInv)
    {
        sitems = new int[44];
        for (int i = 0; i <= 43; i++)
        {
            sitems[i] = i;
        }
        this.player = player;
        this.uiInv = uiInv;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            uiInv.selectSlot(sitems[0]);
            selectedItem = sitems[0];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            uiInv.selectSlot(sitems[1]);
            selectedItem = sitems[1];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            uiInv.selectSlot(sitems[2]);
            selectedItem = sitems[2];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            uiInv.selectSlot(sitems[3]);
            selectedItem = sitems[3];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            uiInv.selectSlot(sitems[4]);
            selectedItem = sitems[4];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            uiInv.selectSlot(sitems[5]);
            selectedItem = sitems[5];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            uiInv.selectSlot(sitems[6]);
            selectedItem = sitems[6];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            uiInv.selectSlot(sitems[7]);
            selectedItem = sitems[7];
            OnChange?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            uiInv.selectSlot(sitems[8]);
            selectedItem = sitems[8];
            OnChange?.Invoke(this, EventArgs.Empty);
        }

    }
    public void swap(int index1, int index2)
    {
        int i = sitems[index1];
        sitems[index1] = sitems[index2];
        sitems[index2] = i;
        OnSwap?.Invoke(this, EventArgs.Empty);
    }
}
