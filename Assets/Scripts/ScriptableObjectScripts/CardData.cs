using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Card")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public int cardID;
    [field: SerializeField] public float cardValue;
    [field: SerializeField] public float cardMultiplier;
    [field: SerializeField] public string cardLocalWord;
    [field: SerializeField] public string cardTranslation;
    
    [SerializeField] public List<string> questions;
}