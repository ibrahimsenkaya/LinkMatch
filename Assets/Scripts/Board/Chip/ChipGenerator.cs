using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class ChipGenerator : MonoBehaviour
{
    public ChipDataHolder chipDataContainer;
    
    public BoardCreator boardCreator;
    public Chip chipPrefab;
    public List<Chip> chips = new List<Chip>();
    
    [Button]
    public void GenerateTiles()
    {
        for (int i = transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        chips.Clear();
        
        foreach (var tile in boardCreator.tiles)
        {
            var chip = Instantiate(chipPrefab, tile.transform.position, Quaternion.identity,transform);
            ChipType chipType = GetRandomTileType();
            ChipData tileData = chipDataContainer.GetTileData(chipType);
            chip.Initialize(tileData);
            tile.chip = chip;
            chips.Add(chip);
        }
    }

    private ChipType GetRandomTileType()
    {
        int randomIndex = Random.Range(0, chipDataContainer.tileDataList.Count);
        return chipDataContainer.tileDataList[randomIndex].chipType;
    }


    public void GenerateChipWithAnim(Tile tile)
    {
        Vector3 spawnPos = tile.transform.position;
        spawnPos.y = 50f;
        var chip = Instantiate(chipPrefab, spawnPos, Quaternion.identity,transform);
        ChipType chipType = GetRandomTileType();
        ChipData tileData = chipDataContainer.GetTileData(chipType);
        chip.Initialize(tileData);
        tile.chip = chip;
        chips.Add(chip);
        chip.transform.DOMove(tile.transform.position, 0.5f).SetEase(Ease.InSine);
    }
   
}
