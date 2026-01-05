using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDropArea : MonoBehaviour, ICardDropArea
{
    [SerializeField] private bool isFilled = false;
    [SerializeField] private TMP_Text questionText;
    private CardView occupyingCardView = null;
    private string questionString = "";
    [SerializeField] private Animator animator;

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

    public bool IsCorrect()
    {
        if (occupyingCardView == null)
        {
            return false;
        }
        if (occupyingCardView.Card.cardQuestions.Contains(questionString))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public float GetPoints()
    {
        if (occupyingCardView == null)
        {
            return 0;
        }

        if (occupyingCardView.Card.cardQuestions.Contains(questionString))
        {
            float points = occupyingCardView.Card.Value * occupyingCardView.Card.Multiplier;
            occupyingCardView.Card.IncreaseMultiplier(0.1f);
            return points;
        }
        else
        {
            occupyingCardView.Card.ResetMultiplier();
            return 0;
        }
    }

    public void PlayResultAnimation()
    {
        if (GetPoints() > 0)
        {
            animator.SetTrigger("true");
        }
        else
        {
            animator.SetTrigger("false");
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
