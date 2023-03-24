using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public Map grid;
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool is_walkable;
    public void CalcFcost()
    {
        fCost = gCost + hCost;
    }

    public void CalcHcost(Vector3 FinalNode)
    {
        hCost = (int)(Mathf.Abs(FinalNode.x - x) + Mathf.Abs(FinalNode.y - y));
    }

    public PathNode WhereCameFrom;
    public PathNode(Map map, int x, int y)
    {

        grid = map;
        this.x = x;
        this.y = y;

        //Debug.Log(map != null);
        //grid.ChangeColor();
        //map.CellValue[x, y].color = Color.blue;
        SetWalkable(true);
    }

    public void SetWalkable(bool value)
    {
        if (value && grid.debug)
            grid.ChangeColor(x, y, Color.green);
        else
            grid.ChangeColor(x, y, Color.red);
        is_walkable = value;
              
        
    }
}
