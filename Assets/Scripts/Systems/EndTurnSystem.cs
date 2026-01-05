using System;
using System.Collections;
using UnityEngine;

public class EndTurnSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EndTurnGA>(EndTurnPerformer);
        ActionSystem.SubscribeReaction<EndTurnGA>(EndTurnPreReaction, ReactionTiming.PRE);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EndTurnGA>();
        ActionSystem.UnsubscribeReaction<EndTurnGA>(EndTurnPreReaction, ReactionTiming.PRE);
    }

    private IEnumerator EndTurnPerformer(EndTurnGA endTurnGA)
    {
        Debug.Log("EndTurnPerformer");
        DiscardCardGA discardCardGa = new();
        ActionSystem.Instance.AddReaction(discardCardGa);
        yield return new WaitForSeconds(0.1f);
    }
    
    private void EndTurnPreReaction(EndTurnGA endTurnGA)
    {
        Debug.Log("EndTurnPreReaction");
        CheckAnswerGA checkAnswerGa = new();
        ActionSystem.Instance.AddReaction(checkAnswerGa);
    }
}
