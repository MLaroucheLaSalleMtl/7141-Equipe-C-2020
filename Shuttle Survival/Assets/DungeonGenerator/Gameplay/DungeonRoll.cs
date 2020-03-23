using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonRoll
{
    [Range(0,1f)]
    [SerializeField] public float baseChanceOfSucess;
    [SerializeField] public List<DungeonRollModifier> rollChanceModifiers = new List<DungeonRollModifier>();
    [SerializeField] public DungeonEvent successEvent;
    [SerializeField] public DungeonEvent failureEvent;
    [SerializeField] public bool critFailPossible = false; //different outcome 10% of failing time
    [SerializeField] public DungeonEvent criticalFailureEvent;
    [SerializeField] public bool critSucessPossible = false; //different outcome 10% of success time
    [SerializeField] public DungeonEvent criticalSuccessEvent;

    public void Roll()
    {
        float chanceOfSuccess = baseChanceOfSucess;
        if(rollChanceModifiers.Count > 0)
        {
            chanceOfSuccess = Mathf.Clamp01(chanceOfSuccess + AdjustChanceOfSuccessWithModifiers());
        }

        float rand = UnityEngine.Random.Range(0.00f, 1.00f);

        DungeonRollResult dungeonRollResult = CalculateRollResult(rand, chanceOfSuccess);
        LoadDungeonEventBasedOnResult(dungeonRollResult);
    }

    private void LoadDungeonEventBasedOnResult(DungeonRollResult dungeonRollResult)
    {
        DungeonEvent nextEvent = null;
        switch (dungeonRollResult)
        {
            case DungeonRollResult.Failure:
                nextEvent = failureEvent;
                break;
            case DungeonRollResult.Success:
                nextEvent = successEvent;
                break;
            case DungeonRollResult.CriticalFailure:
                nextEvent = criticalFailureEvent;
                break;
            case DungeonRollResult.CriticalSuccess:
                nextEvent = criticalSuccessEvent;
                break;
        }
        DungeonEventPanelHandler.dungeonEventPanelHandler.SetupDungeonEventPanel(nextEvent);
    }

    private float AdjustChanceOfSuccessWithModifiers()
    {
        Debug.Log("Chance modifiers not calculated yet");
        return 0;
    }

    private DungeonRollResult CalculateRollResult(float rand, float chanceOfSuccess)
    {
        DungeonRollResult rollResult;
        if (rand < chanceOfSuccess)
        {
            if (critSucessPossible)
                rollResult = IsThisACriticalSuccess(rand, chanceOfSuccess);
            else
                rollResult = DungeonRollResult.Success;
        }
        else
        {
            if (critFailPossible)
                rollResult = IsThisACriticalFailure(rand, chanceOfSuccess);
            else
                rollResult = DungeonRollResult.Failure;
        }
        return rollResult;
    }

    private DungeonRollResult IsThisACriticalFailure(float rand, float chanceOfSuccess)
    {
        if (rand - chanceOfSuccess < (1 - chanceOfSuccess) * 0.1f)
        {
            return DungeonRollResult.CriticalFailure;
        }
        return DungeonRollResult.Failure;
    }

    private DungeonRollResult IsThisACriticalSuccess(float rand, float chanceOfSuccess)
    {
        if(rand < chanceOfSuccess * 0.1f)
        {
            return DungeonRollResult.CriticalSuccess;
        }
        return DungeonRollResult.Success;
    }
}
