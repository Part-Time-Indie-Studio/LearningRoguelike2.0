using UnityEngine;

public class AddRoundsGA : GameAction
{
    public int RoundsAmount { get; set; }
    
    public AddRoundsGA(int roundsAmount)
    {
        RoundsAmount = roundsAmount;
    }
}
