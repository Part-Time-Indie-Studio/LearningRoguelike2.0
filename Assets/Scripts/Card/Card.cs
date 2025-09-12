using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public readonly CardData data;
    public string cardTranslation => data.cardTranslation;
    public string cardLocalWord => data.cardLocalWord;
    public List<string> cardQuestions => data.questions;

    public float Value
    {
        get; private set;
    }
    
    public float Multiplier
    {
        get; private set;
    }
    
    public Card(CardData cardData)
    {
        data = cardData;
        Value = cardData.cardValue;
        Multiplier = cardData.cardMultiplier;
    }

    public void IncreaseMultiplier(float amount)
    {
        Multiplier += amount;
    }

    public void ResetMultiplier()
    {
        Multiplier = data.cardMultiplier;
    }
}
