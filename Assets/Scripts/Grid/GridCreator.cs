using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [OnValueChanged("Create")] [SerializeField]
    private CellShape cellShape;

    [OnValueChanged("Create")] [SerializeField]
    private GridType gridType;

    [OnValueChanged("Create")] public Dimansions currentDimension;

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


    [HideInInspector] public List<Vector3> points = new();
    
    
    public GameObject tilePrefab;
    public List<GameObject> tiles = new List<GameObject>();

    [Button()]
    public void Create()
    {
        //points.ForEach(x=>DestroyImmediate(x.GameObject()));
        Camera.main.orthographicSize = (width * xCount) + ((xCount - 1) * (offsetX / 2f)) + (width* 2f);
        for (int i = transform.childCount-1; i >=0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
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
                        if (tilePrefab!= null)
                        {
                            GameObject tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
                            tiles.Add(tempTile);
                            tempTile.name = $"Tile {x} {y}";
                        }
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
                        if (tilePrefab!= null)
                        {
                            GameObject tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
                            tiles.Add(tempTile);
                            tempTile.name = $"Tile {x} {y}";
                        }
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
                        if (tilePrefab!= null)
                        {
                            GameObject tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
                            tiles.Add(tempTile);
                            tempTile.name = $"Tile {x} {y}";
                        }
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
                        if (tilePrefab!= null)
                        {
                            GameObject tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
                            tiles.Add(tempTile);
                            tempTile.name = $"Tile {x} {y}";
                        }
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
                        if (tilePrefab!= null)
                        {
                            GameObject tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
                            tiles.Add(tempTile);
                            tempTile.name = $"Tile {x} {y}";
                        }
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
                        if (tilePrefab!= null)
                        {
                            GameObject tempTile =Instantiate(tilePrefab, tempPos, Quaternion.identity, transform);
                            tiles.Add(tempTile);
                            tempTile.name = $"Tile {x} {y}";
                            
                        }
                        //poses[((xCount-1) * y) + xCount] = tempPos;
                    }
                }
            }
        }
    }


    private void OnValidate()
    {
        Create();
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