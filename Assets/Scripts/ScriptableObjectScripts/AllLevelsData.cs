using UnityEngine;

[CreateAssetMenu(menuName = "Level/AllLevelsData")]
public class AllLevelsData : ScriptableObject
{
    [field: SerializeField] public LevelData currentLevelData;
    [field: SerializeField] public LevelData[] allLevelsData;
    
    public void SetSelectedLevel(LevelData levelData)
    {
        currentLevelData = levelData;
    }

    public LevelData GetNextLevel()
    {
        int currentIndex = System.Array.IndexOf(allLevelsData, currentLevelData);
        if (currentIndex >= 0 && currentIndex < allLevelsData.Length - 1)
        {
            return allLevelsData[currentIndex + 1];
        }
        return null;
    }
}
