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
            Debug.LogError("No levels configured in LevelManager!");
        }
    }

    private void InitializeLevel()
    {
        allLevels.currentLevelData = allLevels.allLevelsData[currentLevelDataIndex];
        TurnSystem.Instance.AddRoundsAmount(allLevels.currentLevelData.RoundsGained);
        PointsManager.Instance.InitializePointsManager();
    }
    
    public void ProceedToNextLevel()
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
        RemoveDropAreaCardsGA removeDropAreaCardsGA = new();
        ActionSystem.Instance.AddReaction(removeDropAreaCardsGA);
        SpawnDropAreasGA spawnDropAreasGA = new(5);
        ActionSystem.Instance.AddReaction(spawnDropAreasGA);
        PointsUIManager.Instance.SetMultiplierValue();
        
        yield return null;
    }
    
    public LevelData GetCurrentLevel()
    {
        return allLevels.currentLevelData;
    }
}
