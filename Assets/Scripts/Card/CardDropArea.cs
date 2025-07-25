using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDropArea : MonoBehaviour, ICardDropArea
{
    [SerializeField] private bool isFilled = false;
    [SerializeField] private TMP_Text questionText;
    private CardView occupyingCardView = null;
    private string questionString = "";

    private void Start()
    {
        SetupDropArea();
    }

    private void OnDestroy()
    {
        if (occupyingCardView != null)
        {
            if (CardSystem.Instance != null)
            {
                CardSystem.Instance.AddCardToDeck(occupyingCardView.Card);
                OnDestroyCard();
            }
        }
    }

    public void OnCardDrop(CardView card)
    {
        card.transform.position = transform.position;
        occupyingCardView = card;
        //card.isLocked = true;
        SetFilled(true);
    }

    public void OnCardPickUp(CardView card)
    {
        occupyingCardView = null;
        SetFilled(false);
    }

    public float IsCorrect()
    {
        if (occupyingCardView == null)
        {
            return 0;
        }

        if (occupyingCardView.Card.cardQuestions.Contains(questionString))
        {
            return occupyingCardView.Card.Value;
        }
        else
        {
            return 0;
        }
    }


    private void SetupDropArea()
    {
        questionString = QuestionManager.Instance.GetQuestion();
        questionText.text = questionString;
    }
    
    private void OnDestroyCard()
    {
        Destroy(occupyingCardView.gameObject);
        occupyingCardView = null;
        SetFilled(false);
    }

    public bool CanDropCard()
    {
        return !isFilled;
    }

    public void SetFilled(bool filled)
    {
        isFilled = filled;
    }
}
