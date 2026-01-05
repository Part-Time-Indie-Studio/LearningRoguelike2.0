using UnityEngine;

public class PlayResultAnimationGA : GameAction
{
    public CardDropArea CardDropArea { get; set; }

    public PlayResultAnimationGA(CardDropArea cardDropArea)
    {
        CardDropArea = cardDropArea;
    }
}
