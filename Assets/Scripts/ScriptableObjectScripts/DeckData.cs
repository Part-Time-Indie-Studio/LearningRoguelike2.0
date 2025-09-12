using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Decks/DeckData")]
public class DeckData : ScriptableObject
{
    [field: SerializeField] public string DeckName { get; set; }
    [field: SerializeField] public List<CardData> Cards { get; set; }
}
