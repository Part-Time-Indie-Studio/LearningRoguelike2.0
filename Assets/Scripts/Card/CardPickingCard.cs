using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardPickingCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject LocalWord;
    [SerializeField] private GameObject TranslatedWord;
    [SerializeField] private TMP_Text cardTranslationText;
    [SerializeField] private TMP_Text cardLocalWordText;
    [SerializeField] private TMP_Text cardValueText;
    [SerializeField] private TMP_Text cardMultiplierText;

    public Card Card { get; private set; }

    public void Setup(Card card)
    {
        Card = card;
        cardLocalWordText.text = card.cardLocalWord;
        cardTranslationText.text = card.cardTranslation;
        cardValueText.text = card.Value.ToString();
        cardMultiplierText.text = card.Multiplier.ToString();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        TranslatedWord.gameObject.SetActive(false);
        LocalWord.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LocalWord.gameObject.SetActive(false);
        TranslatedWord.gameObject.SetActive(true);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AddCardToDeckGA addCardToDeckGA = new(Card.data);
        ActionSystem.Instance.Perform(addCardToDeckGA);
        Debug.Log("OnPointerClick");
    }
}
