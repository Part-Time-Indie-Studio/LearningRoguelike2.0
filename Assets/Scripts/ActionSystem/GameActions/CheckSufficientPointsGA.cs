using UnityEngine;

public class CheckSufficientPointsGA : GameAction
{
    public int RequiredPoints { get; set; }

    public CheckSufficientPointsGA()
    {
        RequiredPoints = LevelManager.Instance.GetCurrentLevel().RequiredPoints;
    }
}
