using UnityEngine;

public class AddPointsGA : GameAction
{
    public float PointsAmount { get; set; }
    public CardDropArea CardDropArea { get; set; }
    
    public AddPointsGA(float pointsAmount, CardDropArea cardDropArea)
    {
        PointsAmount = pointsAmount;
        CardDropArea = cardDropArea;
    }
}
