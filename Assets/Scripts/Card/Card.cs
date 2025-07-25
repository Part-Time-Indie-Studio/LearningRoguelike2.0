using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    private readonly CardData data;
    public Sprite CardImage
    {
        get => data.cardImage;
    }
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
}
