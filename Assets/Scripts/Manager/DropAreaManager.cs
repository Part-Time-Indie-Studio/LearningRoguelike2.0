using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAreaManager : Singleton<DropAreaManager>
{
    [SerializeField] private List<CardDropArea> spawnedDropAreas;
    [SerializeField] private GameObject dropAreaPrefab;
    [SerializeField] private float spacing = 1f;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddPointsGA>(AddPointsPerformer);
        ActionSystem.AttachPerformer<CheckAnswerGA>(CheckAnswerPerformer);
        ActionSystem.AttachPerformer<SpawnDropAreasGA>(SpawnDropAreasPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddPointsGA>();
        ActionSystem.DetachPerformer<CheckAnswerGA>();
        ActionSystem.DetachPerformer<SpawnDropAreasGA>();
    }

    public List<CardDropArea> GetDropAreas()
    {
        return spawnedDropAreas;
    }

    private IEnumerator CheckAnswerPerformer(CheckAnswerGA checkAnswerGA)
    {
        foreach (CardDropArea dropArea in spawnedDropAreas)
        {
            AddPointsGA addPointsGA = new(dropArea.IsCorrect());
            ActionSystem.Instance.AddReaction(addPointsGA);
            yield return null;
        }
    }
    
	private IEnumerator AddPointsPerformer(AddPointsGA addPointsGA)
    {
        Debug.Log("AddPointsPerformer");
        PointsManager.Instance.AddPoints(addPointsGA.PointsAmount);
        yield return new WaitForSeconds(0.7f);
    }

    private IEnumerator SpawnDropAreasPerformer(SpawnDropAreasGA spawnDropAreasGA)
    {
        SpawnDropAreas(spawnDropAreasGA.Amount);
        yield return null;
    }

    private IEnumerator RemoveDropAreaPerformer(RemoveDropAreaCardsGA removeDropAreaCardsGA)
    {
        yield return null;
    }
    
    private void SpawnDropAreas(int numberOfAreas)
    {
        ClearDropAreas();

        if (dropAreaPrefab == null)
        {
            Debug.LogError("Drop Area Prefab is not assigned in the DropAreaManager!");
            return;
        }

        if (numberOfAreas <= 0)
        {
            return; 
        }
        
        float areaWidth = dropAreaPrefab.GetComponent<RectTransform>().rect.width;
        
        float totalWidth = (numberOfAreas * areaWidth) + ((numberOfAreas - 1) * spacing);
        
        float startX = -totalWidth / 2f + areaWidth / 2f;

        for (int i = 0; i < numberOfAreas; i++)
        {
            float xPos = startX + i * (areaWidth + spacing);
            Vector3 spawnPosition = new Vector3(xPos, 0, 0);
            
            GameObject dropAreaInstance = Instantiate(dropAreaPrefab, transform);
            
            dropAreaInstance.transform.localPosition = spawnPosition;
            dropAreaInstance.name = $"Drop Area Slot {i + 1}";
            
            spawnedDropAreas.Add(dropAreaInstance.GetComponent<CardDropArea>());
        }
    }
    
    private void ClearDropAreas()
    {
        foreach (CardDropArea area in spawnedDropAreas)
        {
            Destroy(area.gameObject);
        }

        spawnedDropAreas.Clear();
    }
}
