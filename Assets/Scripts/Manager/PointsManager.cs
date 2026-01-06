using System;
using System.Collections;
using UnityEngine;

public class PointsManager : Singleton<PointsManager>
{
    private float currentPoints = 0;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddMultiplierGA>(AddMultiplierPerformer);
        ActionSystem.AttachPerformer<CheckSufficientPointsGA>(CheckSufficientPointsPerformer);
        ActionSystem.SubscribeReaction<EndTurnGA>(EndTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<CheckSufficientPointsGA>();
        ActionSystem.DetachPerformer<AddMultiplierGA>();
        ActionSystem.UnsubscribeReaction<EndTurnGA>(EndTurnPostReaction, ReactionTiming.POST);
    }

    public void InitializePointsManager()
    {
        currentPoints = 0;
        PointsUIManager.Instance.SetSliderValue(currentPoints);
        PointsUIManager.Instance.SetSliderMaxValue(LevelManager.Instance.GetCurrentLevel().RequiredPoints);
    }
    
    public void AddPoints(float points)
    {
        currentPoints += points;
        PointsUIManager.Instance.SetSliderValue(currentPoints);
    }

    private IEnumerator AddMultiplierPerformer(AddMultiplierGA addMultiplierGA)
    {
        Debug.Log("AddMultiplierPerformer");
        currentPoints *= addMultiplierGA.Multiplier;
        PointsUIManager.Instance.SetSliderValue(currentPoints);
        yield return null;
    }
    

    private IEnumerator CheckSufficientPointsPerformer(CheckSufficientPointsGA checkSufficientPointsGA)
    {
        Debug.Log("CheckSufficientPointsPerformer");
        if (currentPoints >= checkSufficientPointsGA.RequiredPoints)
        {
            EndGameGA endGameGA = new();
            ActionSystem.Instance.AddReaction(endGameGA);
            LevelManager.Instance.ProceedToNextLevel();
            yield return null;
        }
        else
        {
            if (TurnSystem.Instance.HasRunOutOfRounds())
            {
                GameLostGA gameLostGA = new();
                ActionSystem.Instance.AddReaction(gameLostGA);
                RemoveDropAreaCardsGA removeDropAreaCardsGA = new();
                ActionSystem.Instance.AddReaction(removeDropAreaCardsGA);
            }
            else
            {
                StartLevelGA startLevelGA = new();
                ActionSystem.Instance.AddReaction(startLevelGA);
            }
            yield return null;
        }
    }

    private void EndTurnPostReaction(EndTurnGA endTurnGA)
    {
        Debug.Log("EndTurnPostReaction");
        CheckSufficientPointsGA checkSufficientPointsGA = new();
        ActionSystem.Instance.AddReaction(checkSufficientPointsGA);
    }
    
    public float GetCurrentPoints()
    {
        return currentPoints;
    }

    public void SetCurrentPoints(int currentPoints)
    {
        this.currentPoints = currentPoints;
    }
}
