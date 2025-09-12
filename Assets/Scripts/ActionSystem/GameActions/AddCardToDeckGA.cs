using UnityEngine;

public class AddCardToDeckGA : GameAction
{
    public CardData CardData;

    public AddCardToDeckGA(CardData cardData)
    {
        CardData = cardData;
    }
}
