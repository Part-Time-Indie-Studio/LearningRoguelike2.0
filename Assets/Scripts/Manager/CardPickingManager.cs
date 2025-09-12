using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPickingManager : MonoBehaviour
{
    [SerializeField] private CardPickingCard[] pickingCards;
    [SerializeField] private GameObject pickingCardArea;
    [SerializeField] private GameObject pickingCanvas;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<RemovePickingCardsGA>(RemovePickingCardsPerformer);
        ActionSystem.AttachPerformer<PickNewCardGA>(PickNewCardGAPerformer);
        ActionSystem.SubscribeReaction<AddCardToDeckGA>(PickingCardPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<RemovePickingCardsGA>();
        ActionSystem.DetachPerformer<PickNewCardGA>();
        ActionSystem.UnsubscribeReaction<AddCardToDeckGA>(PickingCardPostReaction, ReactionTiming.POST);
    }

    private IEnumerator RemovePickingCardsPerformer(RemovePickingCardsGA removePickingCardsGA)
    {
        pickingCardArea.SetActive(false);
        pickingCanvas.SetActive(false);
        StartGameGA startGameGA = new();
        ActionSystem.Instance.AddReaction(startGameGA);
        yield return null;
    }

    private void PickingCardPostReaction(AddCardToDeckGA addCardToDeckGA)
    {
        RemovePickingCardsGA removePickingCardsGA = new();
        ActionSystem.Instance.AddReaction(removePickingCardsGA);
    }
    
    private IEnumerator PickNewCardGAPerformer(PickNewCardGA pickNewCardGA)
    {
        pickingCanvas.SetActive(true);
        pickingCardArea.SetActive(true);
        int index = 0;
        List<CardData> tempAvailableDeck = new List<CardData>(GameManager.Instance.GetRandomCardsFromAvailableDeck(3));
        foreach (CardData cardData in tempAvailableDeck)
        {
            Card card = new(cardData);
            CreatePickedCard(card, index);
            index++;
        }
        yield return null;
    }

    private void CreatePickedCard(Card card, int index)
    {
        pickingCards[index].Setup(card);
    }
    
    
}
