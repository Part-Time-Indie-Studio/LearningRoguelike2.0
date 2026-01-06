using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text deckSizeText;
    [SerializeField] private TMP_Text discardSizeText;
    [SerializeField] private TMP_Text RoundsLeftText;

    private int deckSize;
    private int discardSize;


    public void InitializeUI(int deck, int discard)
    {
        this.deckSize = deck;
        this.discardSize = discard;
        deckSizeText.text = $"Deck:{deckSize}";
        discardSizeText.text = $"Discard:{discardSize}";
    }
    
    public void UpdateDeckSize(int size)
    {
        deckSize = size;
        deckSizeText.text = $"Deck:{deckSize}";
    }

    public void UpdateDiscardSize(int size)
    {
        discardSize = size;
        discardSizeText.text = $"Discard:{discardSize}";
    }

    public void UpdateRoundsLeft(int rounds)
    {
        RoundsLeftText.text = $"Rounds Left:{rounds}";
    }
}
