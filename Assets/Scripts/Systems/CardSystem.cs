using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;
    [SerializeField] private List<Card> drawPile = new();
    [SerializeField] private List<Card> discardPile = new();
    [SerializeField] private List<Card> hand = new();

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardCardGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardGA>();
        ActionSystem.DetachPerformer<DiscardCardGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
    }

    public void Setup(List<CardData> deckData)
    {
        drawPile.Clear();
        discardPile.Clear();
        hand.Clear();
        UIManager.Instance.InitializeUI(drawPile.Count, discardPile.Count);
        foreach (CardData cardData in deckData)
        {
            Card card = new(cardData);
            drawPile.Add(card);
        }

        InitialiseQuestions();
    }
    
    private void InitialiseQuestions()
    {
        for (int i = 0; i < drawPile.Count; i++)
        {
            QuestionManager.Instance.SetQuestions(drawPile[i].cardQuestions);
        }
    }

    private IEnumerator DrawCardsPerformer(DrawCardGA drawCardGA)
    {
        Debug.Log("DrawCardsPerformer");
        int actualAmount = Mathf.Min(drawCardGA.Amount, drawPile.Count);
        int notDrawnAmount = drawCardGA.Amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }

        if (notDrawnAmount > 0)
        {
            RefillDeck();
            if (drawPile.Count != 0)
            {
                for (int i = 0; i < notDrawnAmount; i++)
                {
                    if (drawPile.Count <= 0)
                    {
                        yield return null;
                    }
                    else
                    {
                        yield return DrawCard();
                    }
                }
            }
            else
            {
                yield return null;
            }
        }
    }
    
    private IEnumerator DiscardAllCardsPerformer(DiscardCardGA discardCardGA)
    {
        Debug.Log("DiscardAllCardsPerformer");
        foreach (var card in hand)
        {
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);
        HandView.Instance.PlayCard(playCardGA.Card);
        yield return null;
    }
    
    private IEnumerator DrawCard()
    {
        Debug.Log("DrawCard");
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
        UIManager.Instance.UpdateDeckSize(drawPile.Count);
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }

    public void AddCardToDeck(Card card)
    {
        drawPile.Add(card);
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        UIManager.Instance.UpdateDiscardSize(discardPile.Count);
        Destroy(cardView.gameObject);
    }
}
