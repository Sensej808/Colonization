using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map<GridEl>
{
    public int width { get; private set; }
    public int height { get; private set; }
    public float CellSize;

    private Vector3 start;
    private TextMesh[,] CellValue;
    private GridEl[,] map;

    public Map(int W, int H, float cellSize, Vector3 startPos, Func<Map<GridEl>, int, int, GridEl> constructor)
    {
        width = W;
        height = H;
        map = new GridEl[W, H];
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i,j] = constructor(this, i, j);
            }
        }
        CellValue = new TextMesh[W, H];
        CellSize = cellSize;
        start = startPos;

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject gameObject = new GameObject("Cell", typeof(TextMesh));
                gameObject.transform.position = new Vector3(CellSize * i + 0.5F * CellSize, CellSize * j + 0.5F * CellSize, 0) + start;
                CellValue[i, j] = gameObject.GetComponent<TextMesh>();
                CellValue[i, j].text = $"0";
                CellValue[i, j].characterSize = 0.5F;
                CellValue[i, j].anchor = TextAnchor.MiddleCenter;
            }
        }


    }

    public void GetXY(Vector3 Pos, out int X, out int Y)
    {
        X = Mathf.FloorToInt((Pos - start).x / CellSize);
        Y = Mathf.FloorToInt((Pos - start).y / CellSize);
    }
    private void SetValue(int x, int y, GridEl value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            map[x, y] = value;
            CellValue[x, y].text = value.ToString();
        }
    }

    public void SetValue(Vector3 Worldpos, GridEl value)
    {
        int x, y;
        GetXY(Worldpos, out x, out y);
        SetValue(x, y, value);
    }

    GridEl GetValue(int x, int y)
    {
        if ((x >= 0) && (y >= 0) && (x < width) && (y < height))
        {
            //Debug.Log((x >= 0) && (y >= 0) && (x < width) && (y < height));
            return map[x, y];
        }
        return default(GridEl);

    }

    public GridEl GetValue(Vector3 Worldpos)
    {
        int x, y;
        GetXY(Worldpos, out x, out y);
        return GetValue(x, y);
    }
}
