using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardChecker : MonoBehaviour
{
     public BoardCreator boardCreator;
     public bool HasAnyMatchingChips(int matchCount)
    {
        foreach (var tile in boardCreator.tiles)
        {
            int tempMatchCount = 0;
            if (tile.neighbors.Count > 0)
            {
                foreach (var neighbor in tile.neighbors)
                {
                    if (tile.chip.chipData.chipType == neighbor.chip.chipData.chipType)
                    {
                        tempMatchCount++;
                        if (tempMatchCount >= matchCount)
                            return true;
                    }
                }
            }
        }

        return false;
    }
     
     [Button]
     public void ReplaceChipsOnColoumn(HashSet<int> coloumnIndexSet)
     {
         foreach (var coloumnIndex in coloumnIndexSet)
         {
             Debug.Log("Col Index:" + coloumnIndex);
             boardCreator.columns[coloumnIndex].ReplaceChips();
         }
     }
    
}
