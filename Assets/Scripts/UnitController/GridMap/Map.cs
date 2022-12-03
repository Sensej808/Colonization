using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int width { get; private set; }
    public int height { get; private set; }
    public float CellSize;

    public float UP_BORDER;
    public float DOWN_BORDER;
    public float LEFT_BORDER;
    public float RIGHT_BORDER;
        private bool debug = true;

    public Vector3 start { get; private set; }
    public TextMesh[,] CellValue;
    private PathNode[,] map;

    public Map(int W, int H, float cellSize, Vector3 startPos)
    {
        RIGHT_BORDER = startPos.x + W;
        LEFT_BORDER = startPos.x;
        UP_BORDER = startPos.y + H;
        DOWN_BORDER = startPos.y;

        //Debug.Log("R = " + RIGHT_BORDER);
        //Debug.Log("L = " + LEFT_BORDER);
        //Debug.Log("U = " + UP_BORDER);
        //Debug.Log("D = " + DOWN_BORDER);

        width = W;
        height = H;
        CellValue = new TextMesh[W, H];
        CellSize = cellSize;
        start = startPos;
        map = new PathNode[W, H];


        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (debug)
                {
                GameObject gameObject = new GameObject("Cell", typeof(TextMesh));
                gameObject.transform.position = new Vector3(CellSize * i + 0.5F * CellSize, CellSize * j + 0.5F * CellSize, 0) + start;
                CellValue[i, j] = gameObject.GetComponent<TextMesh>();
                CellValue[i, j].text = $"0";
                CellValue[i, j].characterSize = 0.5F;
                CellValue[i, j].anchor = TextAnchor.MiddleCenter;
                CellValue[i, j].color = Color.green;

                }

            }
        }
        
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {

                //Debug.Log($"Constructing node {i} {j}, This = null {this == null}");
                map[i,j] = new PathNode(this, i, j);
            }
        }

    }

    public void ChangeColor(int x, int y, Color c)
    {
        if (x >= 0 && y >= 0 && x < width && y < height && debug)
            CellValue[x, y].color = c;
    }

   public void ChangeText(int x, int y, string s)
    {
        if (x >= 0 && y >= 0 && x < width && y < height && debug)
            CellValue[x, y].text = s;
    }

    //получаем координаты ячейки по координатам в мире
    public void GetXY(Vector3 Pos, out int X, out int Y)
    {
        
        X = Mathf.FloorToInt((Pos - start).x / CellSize);
        Y = Mathf.FloorToInt((Pos - start).y / CellSize);
    }
    
    //Ставим столбцу х строке у
    public void SetValue(int x, int y, PathNode value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            map[x, y] = value;
            CellValue[x, y].text = value.ToString();
        }
    }

    //
    public void SetValue(Vector3 Worldpos, PathNode value)
    {
        int x, y;
        GetXY(Worldpos, out x, out y);
        SetValue(x, y, value);
    }

    //берём у столбца х строки у
    public PathNode GetValue(int x, int y)
    {
        if ((x >= 0) && (y >= 0) && (x < width) && (y < height))
        {
            
            return map[x, y];
        }
        return default(PathNode);

    }

    //Получаем ячейку по координатам мира
    public PathNode GetValue(Vector3 Worldpos)
    {
        int x, y;
        GetXY(Worldpos, out x, out y);
        return GetValue(x, y);
    }
}
