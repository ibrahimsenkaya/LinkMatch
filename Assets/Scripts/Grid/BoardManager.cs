using System;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    #region BoardShapeProperties

    [OnValueChanged("Create")] [SerializeField]
    private CellShape cellShape;

    [OnValueChanged("Create")] [SerializeField]
    private GridType gridType;

    [OnValueChanged("Create")] 
    public Dimansions currentDimension;

    #endregion

    #region BoardSizeProperties

    #region Free

    // [ShowIf("gridType", GridType.Free),AllowNesting]
    [OnValueChanged("Create")] public float width, height;

    [ShowIf("gridType", GridType.Free), AllowNesting] [OnValueChanged("Create")]
    public float offsetX, offsetY;

    #endregion

    #region BoundInSnap

    [HideIf("gridType", GridType.Free), AllowNesting] [OnValueChanged("Create")]
    public float boundX, boundY;

    #endregion

    [OnValueChanged("Create")] [Range(1, 200)]
    public int xCount, yCount;

    private Vector3 center;

    #endregion

    #region Lists

    public List<Tile> tiles = new();
    public List<Coloumn> columns = new();
    [HideInInspector] public List<Vector3> points = new();

    #endregion

    public Tile tilePrefab;

    #region GenerateFunctions

        public void Create()
    {
        //points.ForEach(x=>DestroyImmediate(x.GameObject()));
      
        for (int i = transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        columns.Clear();
        for (int i = 0; i < xCount; i++)
        {
            columns.Add(new Coloumn(){
                index = i,
                tiles = new List<Tile>(),
            });
        }
        points.Clear();
        tiles.Clear();
        
        Vector3 tempPos = Vector3.zero;
        //poses = new Vector3[xCount * yCount];

        if (currentDimension == Dimansions.XY)
            center = new Vector3(transform.position.x, transform.position.y, 0);
        if (currentDimension == Dimansions.XZ)
            center = new Vector3(transform.position.x, 0, transform.position.z);

        if (cellShape == CellShape.Square)
        {
            if (gridType == GridType.Free)
            {
                for (int y = 0; y < yCount; y++)
                {
                    for (int x = 0; x < xCount; x++)
                    {
                        float x_count = x - ((xCount - 1) / 2f);
                        float y_count = y - ((yCount - 1) / 2f);
                        var xPos = (x_count * width) + (offsetX * x_count);
                        var yPos = (y_count * height) + (offsetY * y_count);
                        if (currentDimension == Dimansions.XY)
                            tempPos = center + new Vector3(xPos, yPos, 0);
                        if (currentDimension == Dimansions.XZ)
                            tempPos = center + new Vector3(xPos, 0, yPos);
                        points.Add(tempPos);
                        var tempTile =GenerateTile(tempPos, x, y);
                        columns[x].tiles.Add(tempTile);
                        
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }

            if (gridType == GridType.BoundInSnap)
            {
                width = (boundX / ((float)xCount * 1.5f));
                height = (boundY / (yCount * 1.5f));
                offsetX = width / 2f;
                offsetY = height / 2f;


                for (int y = 0; y < yCount; y++)
                {
                    for (int x = 0; x < xCount; x++)
                    {
                        float x_count = x - ((xCount - 1) / 2f);
                        float y_count = y - ((yCount - 1) / 2f);
                        var xPos = ((x_count) * width) + (offsetX * x_count);
                        var yPos = (y_count * height) + (offsetY * y_count);
                        if (currentDimension == Dimansions.XY)
                            tempPos = center + new Vector3(xPos, yPos, 0);
                        if (currentDimension == Dimansions.XZ)
                            tempPos = center + new Vector3(xPos, 0, yPos);
                        points.Add(tempPos);
                        GenerateTile(tempPos, x, y);
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }

            if (gridType == GridType.BoundOutSnap)
            {
                float startPosX = -boundX / 2;
                float startPosY = -boundY / 2;
                float stampX = ((float)boundX / (xCount - 1));
                float stampY = ((float)boundY / (yCount - 1));

                width = stampX / 2;
                height = stampY / 2;

                for (int y = 0; y < yCount; y++)
                {
                    for (int x = 0; x < xCount; x++)
                    {
                        float x_count = x - ((xCount - 1) / 2f);
                        float y_count = y - ((yCount - 1) / 2f);
                        var xPos = startPosX + (x * stampX);
                        var yPos = startPosY + (y * stampY);
                        if (currentDimension == Dimansions.XY)
                            tempPos = center + new Vector3(xPos, yPos, 0);
                        if (currentDimension == Dimansions.XZ)
                            tempPos = center + new Vector3(xPos, 0, yPos);
                        points.Add(tempPos);
                        GenerateTile(tempPos, x, y);
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }
        }
        if (cellShape == CellShape.Hexagon)
        {
            if (gridType == GridType.Free)
            {
                for (int y = 0; y < yCount; y++)
                {
                    for (int x = 0; x < xCount; x++)
                    {
                        float x_count = x - ((xCount - 1) / 2f);
                        float y_count = y - ((yCount - 1) / 2f);
                        var xPos = (x_count * width) + (offsetX * x_count);
                        var yPos = (y_count * height) + (offsetY * y_count);
                        if (currentDimension == Dimansions.XY)
                            tempPos = center + new Vector3(xPos, yPos, 0);
                        if (currentDimension == Dimansions.XZ)
                            tempPos = center + new Vector3(xPos, 0, yPos);
                        points.Add(tempPos);
                        GenerateTile(tempPos, x, y);
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }

            if (gridType == GridType.BoundInSnap)
            {
                width = (boundX / ((float)xCount * 1.5f));
                height = (boundY / (yCount * 1.5f));
                offsetX = width / 2f;
                offsetY = height / 2f;


                for (int y = 0; y < yCount; y++)
                {
                    for (int x = 0; x < xCount; x++)
                    {
                        float x_count = x - ((xCount - 1) / 2f);
                        float y_count = y - ((yCount - 1) / 2f);
                        var xPos = ((x_count) * width) + (offsetX * x_count);
                        var yPos = (y_count * height) + (offsetY * y_count);
                        if (currentDimension == Dimansions.XY)
                            tempPos = center + new Vector3(xPos, yPos, 0);
                        if (currentDimension == Dimansions.XZ)
                            tempPos = center + new Vector3(xPos, 0, yPos);
                        points.Add(tempPos);
                        GenerateTile(tempPos, x, y);
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }

            if (gridType == GridType.BoundOutSnap)
            {
                float startPosX = -boundX / 2;
                float startPosY = -boundY / 2;
                float stampX = ((float)boundX / (xCount - 1));
                float stampY = ((float)boundY / (yCount - 1));

                width = stampX / 2;
                height = stampY / 2;

                for (int y = 0; y < yCount; y++)
                {
                    for (int x = 0; x < xCount; x++)
                    {
                        float x_count = x - ((xCount - 1) / 2f);
                        float y_count = y - ((yCount - 1) / 2f);
                        var xPos = startPosX + (x * stampX);
                        var yPos = startPosY + (y * stampY);
                        if (currentDimension == Dimansions.XY)
                            tempPos = center + new Vector3(xPos, yPos, 0);
                        if (currentDimension == Dimansions.XZ)
                            tempPos = center + new Vector3(xPos, 0, yPos);
                        points.Add(tempPos);
                        GenerateTile(tempPos, x, y);
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }
        }
        
        SetTilesNeighbors();
    }


    #endregion
    
    #region TileFunctions

    public Tile GenerateTile( Vector3 tempPos, int x, int y)
    {
        if (tilePrefab!= null)
        {
            var tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
            tiles.Add(tempTile);
            tempTile.name = $"Tile {x} {y}";
            tempTile.coloumnIndex = x;
         
            return tempTile;
        }

        return null;
    }
    public HashSet<int> CollectTiles(List<Tile> tileList)
    {
        HashSet<int> coloumnIndexSet = new HashSet<int>();
        foreach (var tile in tileList)
        {
            tile.Collect();
            coloumnIndexSet.Add(tile.coloumnIndex);
        }

        return coloumnIndexSet;
    }
    public void SetTilesNeighbors()
    {
        if (tiles.Count == 0) return;

        for (int i = 0; i < tiles.Count; i++)
        {
            Tile tile = tiles[i];
            tile.neighbors.Clear();

            // Dikey ve yatay komşular
            if (i - xCount >= 0)
                tile.AddNeighbor(tiles[i - xCount]);
            if (i + xCount < tiles.Count)
                tile.AddNeighbor(tiles[i + xCount]);
            if (i - 1 >= 0 && i % xCount != 0)
                tile.AddNeighbor(tiles[i - 1]);
            if (i + 1 < tiles.Count && i % xCount != xCount - 1)
                tile.AddNeighbor(tiles[i + 1]);

            // Çapraz komşular
            // 1) Yukarı-Sol
            if (i - xCount - 1 >= 0 && (i % xCount != 0))
                tile.AddNeighbor(tiles[i - xCount - 1]);

            // 2) Yukarı-Sağ
            if (i - xCount + 1 >= 0 && (i % xCount != xCount - 1))
                tile.AddNeighbor(tiles[i - xCount + 1]);

            // 3) Aşağı-Sol
            if (i + xCount - 1 < tiles.Count && (i % xCount != 0))
                tile.AddNeighbor(tiles[i + xCount - 1]);

            // 4) Aşağı-Sağ
            if (i + xCount + 1 < tiles.Count && (i % xCount != xCount - 1))
                tile.AddNeighbor(tiles[i + xCount + 1]);
        }
    }
    
    #endregion
    
    
}

[Serializable]
public class Coloumn
{
    public int index;
    public List<Tile> tiles = new();

    public List<Tile> ReplaceChips()
    {
        int fallenCount = 0;
        for (var i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].chip ==null)
                fallenCount++;
            else
            {
                if (fallenCount!=0)
                {
                    tiles[i-fallenCount].chip = tiles[i].chip;
                    tiles[i].chip = null;
                }
            }
        }
        foreach (var tile in tiles)
        {
            if (tile.chip!=null)
                tile.chip.transform.DOMove(tile.transform.position,.2f).SetEase(Ease.InSine);
        }
        return tiles.GetRange(tiles.Count - fallenCount, fallenCount);
    }
}

public enum Dimansions
{
    XY,
    XZ,
}

public enum GridType
{
    Free,
    BoundInSnap,
    BoundOutSnap
}

public enum CellShape
{
    Square,
    Hexagon,
}