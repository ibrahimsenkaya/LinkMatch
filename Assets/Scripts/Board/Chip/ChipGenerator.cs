using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChipGenerator : MonoBehaviour
{
    private BoardManager boardManager;
    public ChipDataHolder chipDataContainer;
    public Chip chipPrefab;
    public List<Chip> chips = new();
    
    public void Initialize(BoardManager boardManager)
    {
        this.boardManager = boardManager;
    }
    
    public void GenerateChips()
    {
        for (int i = transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        chips.Clear();
        
        foreach (var tile in boardManager.tiles)
        {
            var chip = Instantiate(chipPrefab, tile.transform.position, Quaternion.identity,transform);
            ChipType chipType = GetRandomChipType();
            ChipData tileData = chipDataContainer.GetTileData(chipType);
            chip.Initialize(tileData);
            tile.chip = chip;
            chips.Add(chip);
        }
    }
    
    public void RemoveChips(List<Tile> tileList)
    {
        foreach (var tile in tileList)
        {
            chips.Remove(tile.chip); 
        }
    }

    private ChipType GetRandomChipType()
    {
        int randomIndex = Random.Range(0, chipDataContainer.tileDataList.Count);
        return chipDataContainer.tileDataList[randomIndex].chipType;
    }
    public void GenerateChipWithAnim(List<Tile> tileList)
    {
        foreach (var tile in tileList)
        {
            Vector3 spawnPos = tile.transform.position;
            spawnPos.y = 50f;
            var chip = Instantiate(chipPrefab, spawnPos, Quaternion.identity,transform);
            ChipType chipType = GetRandomChipType();
            ChipData tileData = chipDataContainer.GetTileData(chipType);
            chip.Initialize(tileData);
            tile.chip = chip;
            chips.Add(chip);
            chip.transform.DOMove(tile.transform.position, 0.5f).SetEase(Ease.InSine);
        }
       
    }

}
