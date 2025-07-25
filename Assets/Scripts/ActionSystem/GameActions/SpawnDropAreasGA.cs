using UnityEngine;

public class SpawnDropAreasGA : GameAction
{
    public int Amount {get; set;}

    public SpawnDropAreasGA(int amount)
    {
        Amount = amount;
    }
}
