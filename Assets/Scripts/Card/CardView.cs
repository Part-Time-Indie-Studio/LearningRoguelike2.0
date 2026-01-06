using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private TMP_Text cardLocalWordText;
    [SerializeField] private TMP_Text cardTranslationText;
    [SerializeField] private TMP_Text cardValueText;
    [SerializeField] private TMP_Text cardMultiplierText;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private SortingGroup sortingGroup;
    
    private BoxCollider2D col;
    private Vector3 startDragPosition;
    private Quaternion startDragRotation;
    private CardDropArea occupyingCardDropArea;

    public bool isInSlot { get; set; }
    public Card Card { get; private set; }

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        sortingGroup = GetComponent<SortingGroup>();
        //isLocked = false;
    }
    
    public void Setup(Card card)
    {
        Card = card;
        cardLocalWordText.text = card.cardLocalWord;
        cardTranslationText.text = card.cardTranslation;
        cardValueText.text = card.Value.ToString();
        cardMultiplierText.text = card.Multiplier.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (ActionSystem.Instance.IsPerforming) return;
        //if (isLocked) return;
        if (isInSlot)
        {
            occupyingCardDropArea.OnCardPickUp(this);
        }
        sortingGroup.sortingOrder = 99;
        Interactions.Instance.PlayerIsDragging = true;
        startDragPosition = transform.position;
        startDragRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.position = GetMousePositionInWorldSpace();
    }
    
    public void OnDrag(PointerEventData data)
    {
        //if (isLocked) return;
        if (ActionSystem.Instance.IsPerforming) return;
        if (Interactions.Instance.PlayerCanMove())
        {
            transform.position = GetMousePositionInWorldSpace();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (isLocked) return;
        if (ActionSystem.Instance.IsPerforming) return;
        sortingGroup.sortingOrder = 90;
        Interactions.Instance.PlayerIsDragging = false;
        
        CardDropArea closestDropArea = FindClosestDropArea();

        if (closestDropArea != null && closestDropArea.CanDropCard())
        {
            PlayCardGA playCardGA = new(Card);
            ActionSystem.Instance.Perform(playCardGA);
            occupyingCardDropArea = closestDropArea;
            occupyingCardDropArea.OnCardDrop(this);
            isInSlot = true;
        }
        else
        {
            transform.position = startDragPosition;
            if (occupyingCardDropArea != null)
            {
                occupyingCardDropArea.OnCardDrop(this);
            }
        }
    }
    
    private CardDropArea FindClosestDropArea()
    {
        var dropAreas = DropAreaManager.Instance.GetDropAreas();

        if (dropAreas == null || dropAreas.Count == 0)
        {
            return null;
        }
        
        float closestDistance = float.MaxValue;
        CardDropArea closestDropArea = null;

        foreach (var dropArea in dropAreas)
        {
            float distance = Vector3.Distance(transform.position, dropArea.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDropArea = dropArea;
            }
        }
        
        float maxDistanceThreshold = 2f; // Adjust as needed
        if (closestDistance > maxDistanceThreshold)
        {
            return null; // No drop area is close enough
        }

        return closestDropArea;
    }
    public Vector3 GetMousePositionInWorldSpace()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0f;
        return p;
    }
}
