using System;
using System.Collections;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private AllLevelsData allLevels;
    private int currentLevelDataIndex;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<StartLevelGA>(StartLevelGAPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<StartLevelGA>();
    }

    void Start()
    {
        if (allLevels.allLevelsData.Length > 0)
        {
            currentLevelDataIndex = 0;
            InitializeLevel();
        }
        else
        {
            Debug.LogError("No levels configured in PointsManager!");
        }
    }

    private void InitializeLevel()
    {
        allLevels.currentLevelData = allLevels.allLevelsData[currentLevelDataIndex];
        PointsManager.Instance.InitializePointsManager();
    }
    
    private void ProceedToNextLevel()
    {
        if (currentLevelDataIndex < allLevels.allLevelsData.Length - 1)
        {
            currentLevelDataIndex++;
            PointsManager.Instance.SetCurrentPoints(0);
            InitializeLevel();
        }
        else
        {
            Debug.Log("All levels completed! Congratulations!");
        }
    }

    private IEnumerator StartLevelGAPerformer(StartLevelGA startLevelGA)
    {
        SpawnDropAreasGA spawnDropAreasGA = new(5);
        ActionSystem.Instance.AddReaction(spawnDropAreasGA);
        RemoveDropAreaCardsGA removeDropAreaCardsGA = new();
        ActionSystem.Instance.AddReaction(removeDropAreaCardsGA);
        yield return null;
    }
    
    public LevelData GetCurrentLevel()
    {
        return allLevels.currentLevelData;
    }
}
