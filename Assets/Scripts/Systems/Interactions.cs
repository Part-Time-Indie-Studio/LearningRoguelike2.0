using UnityEngine;

public class Interactions : Singleton<Interactions>
{
    public bool PlayerIsDragging { get; set; } = false;

    public bool PlayerCanHover()
    {
        if (PlayerIsDragging) return false;
        return true;
    }

    public bool PlayerCanMove()
    {
        if (PlayerIsDragging) return true;
        return false;
    }
}
