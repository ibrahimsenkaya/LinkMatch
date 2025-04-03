using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public BoardCreator board;
    public Tile cTile;
    
    public List<Tile> selectedTiles;
    public LineRenderer lineRenderer;

    void Update()
    {
        if (Input.GetMouseButton(0))
            DetectTileUnderMouse();
        if (Input.GetMouseButtonUp(0))
            Reset();

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
        // Ekran koordinatından dünya (world) koordinatına
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 2D senaryoda Z=0 düzleminde çalışacağımızı varsayıyoruz
        if (board.currentDimension == Dimansions.XY)
        {
            mousePos.z = 0f;

            // BoardCreator’ın “center” olarak kullandığı transform.position’dan offset alıyoruz
            Vector3 localPos = mousePos - board.transform.position;

            // Bir hücrenin kapladığı toplam genişlik (width + offsetX)
            float totalCellWidth = board.width + board.offsetX;
            float totalCellHeight = board.height + board.offsetY;

            // Grid orta noktası mantığı için (xCount - 1)/2, (yCount - 1)/2 kullanıyoruz
            float halfXCount = (board.xCount - 1) / 2f;
            float halfYCount = (board.yCount - 1) / 2f;

            // x_count ve y_count, BoardCreator’ın grid hesaplamasındaki “x_count, y_count” ile eşleşmeli
            float x_count = localPos.x / totalCellWidth;
            float y_count = localPos.y / totalCellHeight;

            // Hangi sütun/kolon ve satır?
            // +0.5f kaydırma, negatiften pozitife geçişte yuvarlamaları düzeltmeye yardımcı olur
            int col = Mathf.FloorToInt(x_count + halfXCount + 0.5f);
            int row = Mathf.FloorToInt(y_count + halfYCount + 0.5f);

            // Geçerli indis mi?
            if (col >= 0 && col < board.xCount && row >= 0 && row < board.yCount)
            {
                // Tile listesinde index => (row * xCount + col)
                int index = row * board.xCount + col;
                if (index >= 0 && index < board.tiles.Count)
                {
                    if (cTile !=null)
                        cTile.DownLight();
                    Tile tile = board.tiles[index];
                    Debug.Log("Tıkladığınız hücre: " + tile.name);
                    tile.Highlight();
                    cTile = tile;
                    
                    ManageSelectedTileList(tile);
                    SetLinePoints();
                    // İsterseniz buradan tile’a özel işlemler yapabilirsiniz
                    // tile.Highlight();
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
  
}
