using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardController : MonoBehaviour
{
    private BoardManager boardManager;

    public void Initialize(BoardManager boardManager, ChipGenerator chipGenerator)
    {
        this.boardManager = boardManager;
    }
     public bool HasAnyMatchingChips(int matchCount)
    {
        foreach (var tile in boardManager.tiles)
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
    
}
