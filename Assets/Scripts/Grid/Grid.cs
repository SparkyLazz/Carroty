using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Vector3 originPosition;
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[i, y] = UtilsClass.CreateWorldText(gridArray[i, y].ToString(), null, getWorldPosition(i, y) + new Vector3(cellSize, cellSize) * .5f, 5, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(getWorldPosition(i, y), getWorldPosition(i, y + 1), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(i, y), getWorldPosition(i + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(getWorldPosition(0, height), getWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(width, 0), getWorldPosition(width, height), Color.white, 100f);


    }
    private Vector3 getWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }
    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
