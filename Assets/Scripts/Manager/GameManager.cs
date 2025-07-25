using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<CardData> deckData;
    
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<StartGameGA>(StartGameGAPerformer);
        ActionSystem.SubscribeReaction<StartGameGA>(StartGamePostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.AttachPerformer<StartGameGA>(StartGameGAPerformer);
        ActionSystem.SubscribeReaction<StartGameGA>(StartGamePostReaction, ReactionTiming.POST);
    }
    
    private void Start()
    {
        StartGameGA startGameGA = new();
        ActionSystem.Instance.Perform(startGameGA);
    }

    private IEnumerator StartGameGAPerformer(StartGameGA startGameGA)
    {
        CardSystem.Instance.Setup(deckData);
        yield return null;
    }

    private void StartGamePostReaction(StartGameGA startGameGA)
    {
        StartLevelGA startLevelGA = new();
        ActionSystem.Instance.AddReaction(startLevelGA);
    }
}
