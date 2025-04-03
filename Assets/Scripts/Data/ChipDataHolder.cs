using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipDataHolder", menuName = "ScriptableObjects/ChipDataHolder", order = 1)]
public class ChipDataHolder : ScriptableObject
{
    public List<ChipData> tileDataList;
    public ChipData GetTileData(ChipType chipType)
    {
        foreach (var tileData in tileDataList)
        {
            if (tileData.chipType == chipType)
            {
                return tileData;
            }
        }

        return null;
    }
}

[Serializable]
public class ChipData
{
    public ChipType chipType;
    public Sprite sprite;
    
}