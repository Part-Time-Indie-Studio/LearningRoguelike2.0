using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text deckSizeText;
    [SerializeField] private TMP_Text discardSizeText;
    [SerializeField] private TMP_Text RoundsLeftText;

    private int deckSize;
    private int discardSize;
    private int roundsLeft;

    public void InitializeUI(int deck, int discard, int rounds)
    {
        this.deckSize = deck;
        this.discardSize = discard;
        this.roundsLeft = rounds;
        UpdateUI();
    }
    
    public void UpdateDeckSize(int size)
    {
        deckSize = size;
        UpdateUI();
    }

    public void UpdateDiscardSize(int size)
    {
        discardSize = size;
        UpdateUI();
    }

    public void UpdateRoundsLeft()
    {
        roundsLeft -= 1;
        UpdateUI();
    }

    private void UpdateUI()
    {
        deckSizeText.text = $"Deck:{deckSize}";
        discardSizeText.text = $"Discard:{discardSize}";
        RoundsLeftText.text = $"Rounds Left:{roundsLeft}";
    }
}
