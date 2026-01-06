using UnityEngine;

[CreateAssetMenu(menuName = "Level/LevelData")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public int LevelID { get; private set; }
    [field: SerializeField] public int RequiredPoints { get; private set; }
    [field: SerializeField] public int RoundsGained { get; private set; }
}
