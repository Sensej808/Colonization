using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Map<PathNode> grid;
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
    public PathNode(Map<PathNode> map, int x, int y)
    {
        grid = map;
        this.x = x;
        this.y = y;
        is_walkable = true;
    }
}
