using System;
using System.Collections;
using UnityEngine;

public class TurnSystem : Singleton<TurnSystem>
{
    private int currentRound = 0;
    
    public void AddRoundsAmount(int amount)
    {
        currentRound += amount;
        UIManager.Instance.UpdateRoundsLeft(currentRound);
    }

    public void ReduceRoundsAmount()
    {
        currentRound -= 1;
        UIManager.Instance.UpdateRoundsLeft(currentRound);
    }
    
    public bool HasRunOutOfRounds()
    {
        return currentRound <= 0;
    }
}
