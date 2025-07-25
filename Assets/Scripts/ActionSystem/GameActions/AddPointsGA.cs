using UnityEngine;

public class AddPointsGA : GameAction
{
    public float PointsAmount { get; set; }

    public AddPointsGA(float pointsAmount)
    {
        PointsAmount = pointsAmount;
    }
}
