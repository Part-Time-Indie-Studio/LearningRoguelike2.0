using System.Collections;
using UnityEngine;

public class StartTurnSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<StartTurnGA>(StartTurnPerformer);
        ActionSystem.SubscribeReaction<StartLevelGA>(StartLevelPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<StartTurnGA>();
        ActionSystem.SubscribeReaction<StartLevelGA>(StartLevelPostReaction, ReactionTiming.POST);
    }
    
    private IEnumerator StartTurnPerformer(StartTurnGA startTurnGA)
    {
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardGA);
        yield return null;
    }

    private void StartLevelPostReaction(StartLevelGA startLevelGA)
    {
        StartTurnGA startTurnGA = new();
        ActionSystem.Instance.AddReaction(startTurnGA);
    }
}
