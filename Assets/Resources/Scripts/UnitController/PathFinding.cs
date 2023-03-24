using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Скрипт нахождения пути при перемещении
public class PathFinding 
{

    private int Stright_cost = 10;
    private int Diagonal_cost = 14;

    public static PathFinding Instance { get; private set; }
    public Map grid { get; private set; }

    private List<PathNode> OpenNodes;
    private List<PathNode> ClosedNodes;
    private List<PathNode> pathNodes;

    public static PathFinding Init(int W, int H)
    {
        if (Instance == null)
        {
            Instance = new PathFinding(W, H);
        }
            return Instance;

    }

    private PathFinding(int W, int H)
    {
            grid = new Map(W, H, 1, new Vector3(-60, -60, 0));
    }
    public List<Vector3> FindPath(Vector3 start, Vector3 fin)
    {
      
        PathNode startNode = grid.GetValue(start);
        PathNode finNode = grid.GetValue(fin);
        List<PathNode> tray = FindPath(startNode, finNode);
        List<Vector3> res = new List<Vector3>();
        if (tray !=  null)
        {
            for (int i = 0; i < tray.Count; i++)
            {
                res.Add(new Vector3(tray[i].x + grid.start.x + grid.CellSize * 0.5F, tray[i].y + grid.start.y + grid.CellSize * 0.5F, 0)  );
            }
            return res;
        }
        return null;

    }
    public List<PathNode> FindPath(PathNode start, PathNode fin)
    {
        PathNode startNode = start;
        PathNode finalNode = fin;

        //Debug.Log($"startNode = {start.x}  {start.y}");
        //Debug.Log($"finNode = {fin.x}  {fin.y}");

        OpenNodes = new List<PathNode> { startNode};
        ClosedNodes = new List<PathNode>();

        for (float i = grid.LEFT_BORDER; i < grid.RIGHT_BORDER; i++)
        {
            for (float j = grid.DOWN_BORDER; j < grid.UP_BORDER; j++)
            {
                PathNode pathNode = grid.GetValue(new Vector3(i, j, 0));
                pathNode.gCost = int.MaxValue;
                pathNode.CalcFcost();
                //Debug.Log(pathNode.fCost);
                pathNode.grid.ChangeText(pathNode.x, pathNode.y, "INF");
                pathNode.WhereCameFrom = null;

            }
        }

        startNode.gCost = 0;
        startNode.hCost = DistanceToFin(startNode, finalNode);
        startNode.CalcFcost();
        startNode.grid.ChangeText(startNode.x, startNode.y, startNode.fCost.ToString());

        while (OpenNodes.Count > 0){
            PathNode cur = LowestFcost();
            if(cur == finalNode)
            {
                return CalculatePath(finalNode);
            }
            OpenNodes.Remove(cur);
            ClosedNodes.Add(cur);

            foreach (var neigh in OpenNeighbours(cur))
            {
                //Debug.Log(OpenNeighbours(cur).Count);
                if (ClosedNodes.Contains(neigh))
                    continue;
                if (!neigh.is_walkable)
                {
                    ClosedNodes.Add(neigh);
                    continue;
                }

                int CostToNeigh = cur.gCost + DistanceToFin(cur, neigh);
                if(CostToNeigh <= neigh.gCost)
                {
                    neigh.WhereCameFrom = cur;
                    neigh.gCost = CostToNeigh;
                    neigh.hCost = DistanceToFin(neigh, finalNode);
                    neigh.CalcFcost();
                    neigh.grid.ChangeText(neigh.x, neigh.y, neigh.fCost.ToString());
                    neigh.grid.ChangeColor(neigh.x, neigh.y, Color.black);
                    if (!OpenNodes.Contains(neigh))
                        OpenNodes.Add(neigh);
                }
            }
        }

        return null;
    }

    private List<PathNode> OpenNeighbours(PathNode node)
    {
        List<PathNode> Neighs = new List<PathNode>();

        if(node.x - 1 >= 0)
        {
            Neighs.Add(grid.GetValue(node.x - 1, node.y));
            if (node.y - 1 >= 0)
            {
                Neighs.Add(grid.GetValue(node.x - 1, node.y - 1));
            }
            if (node.y + 1 < grid.height)
            {
                Neighs.Add(grid.GetValue(node.x - 1, node.y + 1));
            }
        }
        if (node.x + 1 < grid.width)
        {
            Neighs.Add(grid.GetValue(node.x + 1, node.y));
            if (node.y - 1 >= 0)
            {
                Neighs.Add(grid.GetValue(node.x + 1, node.y - 1));
            }
            if (node.y + 1 < grid.height)
            {
                Neighs.Add(grid.GetValue(node.x + 1, node.y + 1));
            }
        }
        if (node.y - 1 >= 0)
        {
            Neighs.Add(grid.GetValue(node.x, node.y - 1));
        }
        if (node.y + 1 < grid.height)
        {
            Neighs.Add(grid.GetValue(node.x, node.y + 1));
        }

        return Neighs;
    }

    private List<PathNode> CalculatePath(PathNode fin)
    {
        List<PathNode> Tray = new List<PathNode>();
        Tray.Add(fin);
        PathNode node = fin.WhereCameFrom;
        while (node != null)
        {
            Tray.Add(node);
            node = node.WhereCameFrom;
        }
        Tray.Reverse();
        return Tray;
    }
    private int DistanceToFin(PathNode start, PathNode fin)
    {
        int xDist = Mathf.Abs(start.x - fin.x);
        int yDist = Mathf.Abs(start.y - fin.y);
        //Debug.Log($"start = {start.x}  {start.y}");
        //Debug.Log($"fin = {fin.x}  {fin.y}");
        //Debug.Log($"dist = {Diagonal_cost * Mathf.Min(xDist, yDist) + Stright_cost * Mathf.Abs(xDist - yDist)}");
        return Diagonal_cost * Mathf.Min(xDist, yDist) + Stright_cost * Mathf.Abs(xDist - yDist);
    }

    private PathNode LowestFcost()
    {
        PathNode lowest = OpenNodes[0];
        for (int i = 0; i < OpenNodes.Count; i++)
        {
            if(lowest.fCost > OpenNodes[i].fCost)
            {
                lowest = OpenNodes[i];
            }
        }
        return lowest;
    }
}
