using UnityEngine;

[CreateAssetMenu(menuName = "Decks/AllDecksData")]
public class AllDecksData : ScriptableObject
{
    [field: SerializeField] public DeckData currentDeckData;
    [field: SerializeField] public DeckData[] allDecksData;

    public void SetSelectedDeck(DeckData deckData)
    {
        currentDeckData = deckData;
    }
}
