using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private AllDecksData allDecksData;
    [SerializeField] private List<CardData> masterDeckData;
    [SerializeField] private List<CardData> currentDeckData;
    [SerializeField] private List<CardData> availableDeckData;
    [SerializeField] private GameObject GameCanvas;
    [SerializeField] private float numberOfCardsStart;
    
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddCardToDeckGA>(AddCardToDeckGAPerformer);
        ActionSystem.AttachPerformer<StartGameGA>(StartGameGAPerformer);
        ActionSystem.AttachPerformer<EndGameGA>(EndGameGAPerformer);
        ActionSystem.SubscribeReaction<StartGameGA>(StartGamePostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddCardToDeckGA>();
        ActionSystem.DetachPerformer<StartGameGA>();
        ActionSystem.DetachPerformer<EndGameGA>();
        ActionSystem.UnsubscribeReaction<StartGameGA>(StartGamePostReaction, ReactionTiming.POST);
    }

    private void Start()
    {
        masterDeckData = allDecksData.currentDeckData.Cards;
        
        currentDeckData.Clear();
        availableDeckData.Clear();
        
        List<CardData> tempDeck = new List<CardData>(masterDeckData);
        
        Shuffle(tempDeck);
        
        for (int i = 0; i < numberOfCardsStart && i < tempDeck.Count; i++)
        {
            currentDeckData.Add(tempDeck[i]);
        }
        
        for (int i = 5; i < tempDeck.Count; i++)
        {
            availableDeckData.Add(tempDeck[i]);
        }
        
        StartGameGA startGameGA = new();
        ActionSystem.Instance.Perform(startGameGA);
    }

    private IEnumerator AddCardToDeckGAPerformer(AddCardToDeckGA addCardToDeckGA)
    {
        AddCardToCurrentDeck(addCardToDeckGA.CardData);
        yield return null;
    }
    
    private IEnumerator StartGameGAPerformer(StartGameGA startGameGA)
    {
        GameCanvas.SetActive(true);
        CardSystem.Instance.Setup(currentDeckData);
        yield return null;
    }

    private IEnumerator EndGameGAPerformer(EndGameGA endGameGA)
    {
        RemoveDropAreaCardsGA removeDropAreaCardsGA = new();
        ActionSystem.Instance.AddReaction(removeDropAreaCardsGA);
        GameCanvas.SetActive(false);
        PickNewCardGA pickNewCardGA = new();
        ActionSystem.Instance.AddReaction(pickNewCardGA);
        yield return null;
    }

    private void StartGamePostReaction(StartGameGA startGameGA)
    {
        StartLevelGA startLevelGA = new();
        ActionSystem.Instance.AddReaction(startLevelGA);
    }
    
    public List<CardData> GetRandomCardsFromAvailableDeck(int numberOfCards)
    {
        List<CardData> pickedCards = new List<CardData>();
        List<CardData> tempAvailableDeck = new List<CardData>(availableDeckData);
        
        if (tempAvailableDeck.Count == 0)
        {
            Debug.LogWarning("Available deck is empty, cannot pick cards.");
            return pickedCards;
        }
        
        if (numberOfCards > tempAvailableDeck.Count)
        {
            numberOfCards = tempAvailableDeck.Count;
        }

        for (int i = 0; i < numberOfCards; i++)
        {
            int randomIndex = Random.Range(0, tempAvailableDeck.Count);
            pickedCards.Add(tempAvailableDeck[randomIndex]);
            tempAvailableDeck.RemoveAt(randomIndex);
        }
        
        return pickedCards;
    }
    
    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    
    public void AddCardToCurrentDeck(CardData cardToAdd)
    {
        if (availableDeckData.Contains(cardToAdd))
        {
            currentDeckData.Add(cardToAdd);
            availableDeckData.Remove(cardToAdd);
            Debug.Log($"Added {cardToAdd.cardLocalWord} to current deck. Current deck size: {currentDeckData.Count}, Available deck size: {availableDeckData.Count}.");
        }
        else
        {
            Debug.LogWarning($"Card {cardToAdd.cardLocalWord} not found in the available deck. Cannot add to current deck.");
        }
    }
}
