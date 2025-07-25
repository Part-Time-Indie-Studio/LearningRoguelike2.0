using System;
using UnityEngine;

public class DrawCardsButton : MonoBehaviour
{
    public void OnClick()
    {
        BetweenTurnGA betweenTurnGA = new ();
        ActionSystem.Instance.Perform(betweenTurnGA);
    }
}
