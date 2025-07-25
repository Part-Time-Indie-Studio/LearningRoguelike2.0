using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public void OnClick()
    {
        EndTurnGA endTurnGA = new();
        ActionSystem.Instance.Perform(endTurnGA);
    }
}
