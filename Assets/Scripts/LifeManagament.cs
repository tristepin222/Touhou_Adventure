using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LifeManagament 
{
    public event EventHandler LifeChanged;
    public int lifeAmount;
    public bool isPLayer;
    public int maxLife;
    public LifeManagament (int amount, int maxAmount = 0){
lifeAmount = amount;
        maxLife = maxAmount;
    }
    public void addLife(int amount){
    lifeAmount += amount;
        if (LifeChanged != null)
        {

            LifeChanged.Invoke(this, EventArgs.Empty);
        }
    }
    public void increaseMaxLife(int amount)
    {
        maxLife += amount;
        if (LifeChanged != null)
        {

            LifeChanged.Invoke(this, EventArgs.Empty);
        }
    }

    public void reduceLife(int amount){
    lifeAmount -= amount;
        if (LifeChanged != null)
        {
            LifeChanged.Invoke(this, EventArgs.Empty);
        }
    }

    public override string ToString()
    {
        return lifeAmount.ToString();
    }
}
