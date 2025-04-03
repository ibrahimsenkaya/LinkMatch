using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class ChipGenerator : MonoBehaviour
{
    public ChipDataHolder chipDataContainer;
    
    public BoardCreator boardCreator;
    public Chip chipPrefab;

    public List<Chip> tiles = new List<Chip>();
    
    [Button]
    public void GenerateTiles()
    {
        for (int i = transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        tiles.Clear();
        
        foreach (var tile in boardCreator.tiles)
        {
            var chip = Instantiate(chipPrefab, tile.transform.position, Quaternion.identity,transform);
            ChipType chipType = GetRandomTileType();
            ChipData tileData = chipDataContainer.GetTileData(chipType);
            chip.Initialize(tileData);
            tile.chip = chip;
            tiles.Add(chip);
        }
    }

    private ChipType GetRandomTileType()
    {
        int randomIndex = Random.Range(0, chipDataContainer.tileDataList.Count);
        return chipDataContainer.tileDataList[randomIndex].chipType;
    }
}
