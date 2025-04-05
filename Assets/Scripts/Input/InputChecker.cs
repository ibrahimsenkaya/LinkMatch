using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public BoardCreator board;
    public Tile cTile;
    
    public List<Tile> selectedTiles;
    public LineRenderer lineRenderer;
    public BoardChecker boardChecker;

    public Action OnMakeMove;
    
    bool enableToUse;

    public void Enable(bool enable = true)
    {
        enableToUse = enable;
    }

    void Update()
    {
        if (!enableToUse) return;
       
        if (Input.GetMouseButton(0))
            DetectTileUnderMouse();
        if (Input.GetMouseButtonUp(0))
        {
            
            CollectTileList();
            Reset();
        }

    }

    private void Reset()
    {
        selectedTiles.Clear();
        lineRenderer.positionCount = 0;
        if (cTile != null)
            cTile.DownLight();
    }

    private void DetectTileUnderMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (board.currentDimension == Dimansions.XY)
        {
            mousePos.z = 0f;

            Vector3 localPos = mousePos - board.transform.position;

            float totalCellWidth = board.width + board.offsetX;
            float totalCellHeight = board.height + board.offsetY;

            float halfXCount = (board.xCount - 1) / 2f;
            float halfYCount = (board.yCount - 1) / 2f;

            float x_count = localPos.x / totalCellWidth;
            float y_count = localPos.y / totalCellHeight;

            int col = Mathf.FloorToInt(x_count + halfXCount + 0.5f);
            int row = Mathf.FloorToInt(y_count + halfYCount + 0.5f);

            if (col >= 0 && col < board.xCount && row >= 0 && row < board.yCount)
            {
                int index = row * board.xCount + col;
                if (index >= 0 && index < board.tiles.Count)
                {
                    if (cTile !=null)
                        cTile.DownLight();
                    Tile tile = board.tiles[index];
                    tile.Highlight();
                    cTile = tile;
                    
                    ManageSelectedTileList(tile);
                    SetLinePoints();
                }
            }
        }
    }
    public void ManageSelectedTileList(Tile tile)
    {
        if (selectedTiles.Count ==0 )
        {
            selectedTiles.Add(tile);
            return;
        }

        if (selectedTiles.Contains(tile))
        {
            int index = selectedTiles.IndexOf(tile);
            if (index != selectedTiles.Count - 1)
            {
                selectedTiles.RemoveRange(index, selectedTiles.Count-index);
                return;
            }
        }
        var lastTile = selectedTiles[selectedTiles.Count-1];
        if (lastTile.neighbors.Contains(tile) && tile.chip.chipData.chipType == lastTile.chip.chipData.chipType)
        {
            selectedTiles.Add(tile);
        }


    }
    
    public void SetLinePoints()
    {
        if (selectedTiles.Count == 0)
        {
            lineRenderer.positionCount = 0;
            return;
        }
        lineRenderer.positionCount = selectedTiles.Count;
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            var pos = selectedTiles[i].transform.position;
            pos.z = -1f; 
            lineRenderer.SetPosition(i, pos);
        }
    }
    
    public void CollectTileList()
    {
        if (selectedTiles.Count !=0 )OnMakeMove?.Invoke();
        if (selectedTiles.Count<3) return;
        HashSet<int> coloumnIndexSet = new HashSet<int>();
        foreach (var tile in selectedTiles)
        {
            tile.Collect();
            coloumnIndexSet.Add(tile.coloumnIndex);
        }
        boardChecker.ReplaceChipsOnColoumn(coloumnIndexSet);
    }
  
}
