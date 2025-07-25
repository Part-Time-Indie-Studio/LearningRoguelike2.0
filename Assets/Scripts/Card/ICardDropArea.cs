using UnityEngine;

public interface ICardDropArea
{
    void OnCardDrop(CardView cardView);
    
    bool CanDropCard();
    
    void SetFilled(bool filled);
}
