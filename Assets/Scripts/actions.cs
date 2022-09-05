using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class actions 
{
    public enum actionType
    {
        None,
        WaitForEnemyDeath,
        WaitForItem,
        WaitForLevel,
        TPToScene,
        ShowObject,
        WaitForBossDeath,
        ActivateScoreScreen,
        waitForObjective,
        GiveObjective,
        blockForWaitingObjective,
        UnblockForWaitingObjective,
        WaitForMoney,
        RemoveMoney,
        PlayAnimation,
        ShowDialogue,
        Dissapear,
        freezeAll,
        Apprear
    }

    public actionType action;
    public EnemiManagament enemi;
    public Item item;
    public int level;
    public string scene;
    public GameObject showObject;
    public BossManagerFightScene boss;
    public ScoreScreen scoreScreen;
    public Objective objective;
    public int moneyAmount;
}
