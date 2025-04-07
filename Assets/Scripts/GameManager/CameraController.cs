using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float extraPadding  = 2f;
    public Camera cam;

    public void FitCamera(BoardManager boardManager)
    {
        float width = boardManager.width;
        float height = boardManager.height;
        float offsetX = boardManager.offsetX;
        float offsetY = boardManager.offsetY;
        int xCount = boardManager.xCount;
        int yCount = boardManager.yCount;

        float boardWidth = (xCount - 1) * (width + offsetX);
        float boardHeight = (yCount - 1) * (height + offsetY);

        float aspect = (float)Screen.width / Screen.height;

        float halfWidth = boardWidth * 0.5f;
        float halfHeight = boardHeight * 0.5f;

        if ((halfWidth / aspect) > halfHeight)
        {
            cam.orthographicSize = (halfWidth / aspect) + extraPadding;
        }
        else
        {
            cam.orthographicSize = halfHeight + extraPadding;
        }
    }
}
