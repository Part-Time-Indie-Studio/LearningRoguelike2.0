using UnityEngine;

public class AddMultiplierGA : GameAction
{
    public float Multiplier {get; set;}

    public AddMultiplierGA(float multiplier)
    {
        Multiplier = multiplier;
    }
}
