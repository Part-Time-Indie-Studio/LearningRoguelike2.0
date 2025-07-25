using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text deckSizeText;
    [SerializeField] private TMP_Text discardSizeText;

    private int deckSize;
    private int discardSize;

    public void InitializeUI(int deck, int discard)
    {
        this.deckSize = deck;
        this.discardSize = discard;
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

    private void UpdateUI()
    {
        deckSizeText.text = $"Deck:{deckSize}";
        discardSizeText.text = $"Discard:{discardSize}";
    }
}
