using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BetweenTurnSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<BetweenTurnGA>(BetweenTurnPerformer);
        ActionSystem.SubscribeReaction<BetweenTurnGA>(BetweenTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<BetweenTurnGA>(BetweenTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<BetweenTurnGA>();
        ActionSystem.UnsubscribeReaction<BetweenTurnGA>(BetweenTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<BetweenTurnGA>(BetweenTurnPostReaction, ReactionTiming.POST);
    }

    private IEnumerator BetweenTurnPerformer(BetweenTurnGA betweenTurnGA)
    {
        yield return new WaitForSeconds(0.2f);
    }
    
    private void BetweenTurnPreReaction(BetweenTurnGA betweenTurnGA)
    {
        DiscardCardGA discardCardGA = new();
        ActionSystem.Instance.AddReaction(discardCardGA);
    }

    private void BetweenTurnPostReaction(BetweenTurnGA betweenTurnGA)
    {
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardGA);
    }
}
